import { useEffect, useId, useMemo, useState } from 'react'
import { sociosService } from '../../services/sociosService'
import type {
  ChangePasswordRequestDto,
  DiaDto,
  EstadoSocio,
  GeneroSocio,
  PerfilIAUpdateDto,
  PlanSocio,
  UsuarioDto,
  UsuarioUpdateDto,
} from '../../types/socio'
import { getSociosErrorMessage } from '../../utils/apiError'
import { formatDateCell } from '../../utils/date'

const EDIT_PERMISSION = 'EDITAR_USUARIO_SOCIO'
const CHANGE_PASSWORD_PERMISSION = 'CAMBIAR_CONTRASENA_SOCIO'
const DELETE_PERMISSION = 'ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE'

const TABS = [
  { id: 'personal', label: 'Info. Personal', icon: 'P' },
  { id: 'facturacion', label: 'Facturacion', icon: '$' },
  { id: 'perfilIA', label: 'Perfil IA', icon: 'IA' },
  { id: 'seguridad', label: 'Seguridad', icon: 'K' },
] as const

type TabId = (typeof TABS)[number]['id']

interface SocioFormState {
  nombre: string
  apellido: string
  email: string
  telefono: string
  direccion: string
  ciudad: string
  tipoDocumento: string
  nroDocumento: string
  genero: GeneroSocio | ''
  fechaNacimiento: string
  obraSocial: string
  fechaNotificacion: string
  respuestaNotificacion: boolean | null
  pregunta: string
  respuesta: string
  perfilIA: {
    objetivoPrincipal: string
    nivelExperiencia: string
    ejerciciosPreferidos: string
    ejerciciosAEvitar: string
    disponibilidadHoraria: string
    motivacionPersonal: string
  }
}

interface RenovacionState {
  active: boolean
  plan: PlanSocio
  monto: string
}

interface PasswordState extends ChangePasswordRequestDto {
  visible: boolean
}

interface ConsultarSocioModalProps {
  idUsuario: number
  userPermissions: readonly string[]
  onClose: () => void
  onDeleted: () => void
  onUpdated: () => void
}

function hasPermission(userPermissions: readonly string[], permission: string) {
  return userPermissions.includes(permission)
}

function toText(value: string | null | undefined) {
  return value ?? ''
}

function toNullable(value: string) {
  const cleanValue = value.trim()
  return cleanValue ? cleanValue : null
}

function toDateInput(value: string | null | undefined) {
  if (!value) {
    return ''
  }

  return value.split('T')[0] ?? ''
}

function getSocioName(usuario: UsuarioDto | null) {
  const socio = usuario?.personaSocio

  if (!socio) {
    return 'Socio'
  }

  return `${socio.nombre} ${socio.apellido}`.trim() || usuario.username
}

function getLatestCuota(usuario: UsuarioDto | null) {
  const cuotas = usuario?.personaSocio?.cuotas ?? []

  return [...cuotas].sort((a, b) => {
    const first = new Date(a.fechaFinPeriodo).getTime()
    const second = new Date(b.fechaFinPeriodo).getTime()
    return second - first
  })[0]
}

function getInitialFormState(usuario: UsuarioDto): SocioFormState {
  const socio = usuario.personaSocio

  return {
    nombre: toText(socio?.nombre),
    apellido: toText(socio?.apellido),
    email: toText(socio?.email),
    telefono: toText(socio?.telefono),
    direccion: toText(socio?.direccion),
    ciudad: toText(socio?.ciudad),
    tipoDocumento: toText(socio?.tipoDocumento),
    nroDocumento: toText(socio?.nroDocumento),
    genero: socio?.genero ?? '',
    fechaNacimiento: toDateInput(socio?.fechaNacimiento),
    obraSocial: toText(socio?.obraSocial),
    fechaNotificacion: toDateInput(socio?.fechaNotificacion),
    respuestaNotificacion: socio?.respuestaNotificacion ?? null,
    pregunta: toText(socio?.pregunta),
    respuesta: toText(socio?.respuesta),
    perfilIA: {
      objetivoPrincipal: toText(socio?.perfilIA?.objetivoPrincipal),
      nivelExperiencia: toText(socio?.perfilIA?.nivelExperiencia),
      ejerciciosPreferidos: toText(socio?.perfilIA?.ejerciciosPreferidos),
      ejerciciosAEvitar: toText(socio?.perfilIA?.ejerciciosAEvitar),
      disponibilidadHoraria: toText(socio?.perfilIA?.disponibilidadHoraria),
      motivacionPersonal: toText(socio?.perfilIA?.motivacionPersonal),
    },
  }
}

function getInitialDias(usuario: UsuarioDto) {
  return (
    usuario.personaSocio?.rutinas
      ?.filter((rutina) => rutina.activo)
      .map((rutina) => rutina.idDia) ?? []
  )
}

function getPerfilPayload(perfilIA: SocioFormState['perfilIA']): PerfilIAUpdateDto {
  return {
    objetivoPrincipal: toNullable(perfilIA.objetivoPrincipal),
    nivelExperiencia: toNullable(perfilIA.nivelExperiencia),
    ejerciciosPreferidos: toNullable(perfilIA.ejerciciosPreferidos),
    ejerciciosAEvitar: toNullable(perfilIA.ejerciciosAEvitar),
    disponibilidadHoraria: toNullable(perfilIA.disponibilidadHoraria),
    motivacionPersonal: toNullable(perfilIA.motivacionPersonal),
  }
}

function areSameNumberLists(first: number[], second: number[]) {
  if (first.length !== second.length) {
    return false
  }

  const normalizedFirst = [...first].sort((a, b) => a - b)
  const normalizedSecond = [...second].sort((a, b) => a - b)

  return normalizedFirst.every((value, index) => value === normalizedSecond[index])
}

function buildUpdatePayload(
  usuario: UsuarioDto,
  formState: SocioFormState,
  selectedDias: number[],
  renovacion: RenovacionState,
): UsuarioUpdateDto {
  return {
    username: usuario.username,
    tipoPersona: 'Socio',
    personaResponsable: null,
    personaSocio: {
      nombre: formState.nombre.trim(),
      apellido: formState.apellido.trim(),
      email: formState.email.trim(),
      telefono: toNullable(formState.telefono),
      direccion: toNullable(formState.direccion),
      ciudad: toNullable(formState.ciudad),
      tipoDocumento: formState.tipoDocumento.trim(),
      nroDocumento: formState.nroDocumento.trim(),
      genero: formState.genero || null,
      fechaNacimiento: formState.fechaNacimiento || null,
      obraSocial: toNullable(formState.obraSocial),
      fechaNotificacion: formState.fechaNotificacion || null,
      respuestaNotificacion: formState.respuestaNotificacion,
      pregunta: toNullable(formState.pregunta),
      respuesta: toNullable(formState.respuesta),
      diasActivosIds: selectedDias,
      perfilIA: getPerfilPayload(formState.perfilIA),
      cuota: {
        renueva: renovacion.active,
        plan: renovacion.active ? renovacion.plan : null,
        monto: renovacion.active ? Number(renovacion.monto) : null,
      },
    },
    idGrupos: usuario.grupos.map((grupo) => grupo.idGrupo),
  }
}

function getStatusClass(estadoSocio: EstadoSocio | undefined) {
  const normalized = estadoSocio?.toLocaleLowerCase('es-AR')

  if (normalized === 'eliminado') {
    return 'consultar-status consultar-status--danger'
  }

  if (normalized === 'suspendido') {
    return 'consultar-status consultar-status--warning'
  }

  if (normalized === 'actualizado') {
    return 'consultar-status consultar-status--info'
  }

  return 'consultar-status consultar-status--success'
}

export function ConsultarSocioModal({
  idUsuario,
  userPermissions,
  onClose,
  onDeleted,
  onUpdated,
}: ConsultarSocioModalProps) {
  const [usuario, setUsuario] = useState<UsuarioDto | null>(null)
  const [dias, setDias] = useState<DiaDto[]>([])
  const [formState, setFormState] = useState<SocioFormState | null>(null)
  const [selectedDias, setSelectedDias] = useState<number[]>([])
  const [initialFormState, setInitialFormState] = useState<SocioFormState | null>(null)
  const [initialSelectedDias, setInitialSelectedDias] = useState<number[]>([])
  const [editingFields, setEditingFields] = useState<Record<string, boolean>>({})
  const [activeTab, setActiveTab] = useState<TabId>('personal')
  const [renovacion, setRenovacion] = useState<RenovacionState>({
    active: false,
    plan: 'Mensual',
    monto: '',
  })
  const [passwordState, setPasswordState] = useState<PasswordState>({
    visible: false,
    currentPassword: '',
    newPassword: '',
  })
  const [isPasswordAutofillGuardEnabled, setIsPasswordAutofillGuardEnabled] = useState(true)
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [isDeleting, setIsDeleting] = useState(false)
  const [isChangingPassword, setIsChangingPassword] = useState(false)
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')

  const canEdit = hasPermission(userPermissions, EDIT_PERMISSION)
  const canChangePassword = hasPermission(userPermissions, CHANGE_PASSWORD_PERMISSION)
  const canDelete = hasPermission(userPermissions, DELETE_PERMISSION)
  const socio = usuario?.personaSocio ?? null
  const passwordFormKey = useId()
  const latestCuota = useMemo(() => getLatestCuota(usuario), [usuario])
  const socioName = getSocioName(usuario)
  const isDirty = useMemo(() => {
    if (!formState || !initialFormState) {
      return false
    }

    const formChanged = JSON.stringify(formState) !== JSON.stringify(initialFormState)
    const diasChanged = !areSameNumberLists(selectedDias, initialSelectedDias)
    const renovacionChanged = renovacion.active || renovacion.monto.trim() !== ''

    return formChanged || diasChanged || renovacionChanged
  }, [formState, initialFormState, selectedDias, initialSelectedDias, renovacion])

  useEffect(() => {
    let isActive = true

    async function loadSocio() {
      setIsLoading(true)
      setError('')
      setSuccess('')

      try {
        const [diasResponse, usuarioResponse] = await Promise.all([
          sociosService.getDias(),
          sociosService.getSocioById(idUsuario),
        ])

        if (!isActive) {
          return
        }

        const nextFormState = getInitialFormState(usuarioResponse)
        const nextSelectedDias = getInitialDias(usuarioResponse)

        setDias(diasResponse)
        setUsuario(usuarioResponse)
        setFormState(nextFormState)
        setInitialFormState(nextFormState)
        setSelectedDias(nextSelectedDias)
        setInitialSelectedDias(nextSelectedDias)
        setEditingFields({})
        setRenovacion({ active: false, plan: 'Mensual', monto: '' })
      } catch (requestError) {
        if (isActive) {
          setError(getSociosErrorMessage(requestError))
        }
      } finally {
        if (isActive) {
          setIsLoading(false)
        }
      }
    }

    void loadSocio()

    return () => {
      isActive = false
    }
  }, [idUsuario])

  function requestClose() {
    if (isDirty && !window.confirm('Hay cambios sin confirmar. Deseas cerrar la consulta?')) {
      return
    }

    onClose()
  }

  function setField<K extends keyof SocioFormState>(field: K, value: SocioFormState[K]) {
    if (!formState) {
      return
    }

    setFormState({ ...formState, [field]: value })
  }

  function setPerfilField<K extends keyof SocioFormState['perfilIA']>(
    field: K,
    value: SocioFormState['perfilIA'][K],
  ) {
    if (!formState) {
      return
    }

    setFormState({
      ...formState,
      perfilIA: {
        ...formState.perfilIA,
        [field]: value,
      },
    })
  }

  function toggleEditing(field: string) {
    setEditingFields((current) => ({ ...current, [field]: !current[field] }))
  }

  function toggleDia(idDia: number) {
    setSelectedDias((current) =>
      current.includes(idDia) ? current.filter((dia) => dia !== idDia) : [...current, idDia],
    )
  }

  function resetRenovacion() {
    setRenovacion({ active: false, plan: 'Mensual', monto: '' })
  }

  function validateBeforeSave() {
    if (!formState) {
      return 'No se cargaron los datos del socio.'
    }

    if (
      !formState.nombre.trim() ||
      !formState.apellido.trim() ||
      !formState.email.trim() ||
      !formState.tipoDocumento.trim() ||
      !formState.nroDocumento.trim()
    ) {
      return 'Completa nombre, apellido, email, tipo y numero de documento.'
    }

    if (selectedDias.length === 0) {
      return 'Selecciona al menos un dia de asistencia.'
    }

    if (renovacion.active && (!renovacion.plan || Number(renovacion.monto) <= 0)) {
      return 'Para renovar la cuota indica plan y monto mayor a cero.'
    }

    return ''
  }

  async function handleSave() {
    if (!usuario || !formState || !canEdit) {
      return
    }

    const validationError = validateBeforeSave()

    if (validationError) {
      setError(validationError)
      setSuccess('')
      return
    }

    setIsSaving(true)
    setError('')
    setSuccess('')

    try {
      const updatedSocio = await sociosService.updateSocio(
        idUsuario,
        buildUpdatePayload(usuario, formState, selectedDias, renovacion),
      )
      const nextFormState = getInitialFormState(updatedSocio)
      const nextSelectedDias = getInitialDias(updatedSocio)

      setUsuario(updatedSocio)
      setFormState(nextFormState)
      setInitialFormState(nextFormState)
      setSelectedDias(nextSelectedDias)
      setInitialSelectedDias(nextSelectedDias)
      setEditingFields({})
      resetRenovacion()
      setSuccess('Cambios confirmados correctamente.')
      onUpdated()
    } catch (requestError) {
      setError(getSociosErrorMessage(requestError))
    } finally {
      setIsSaving(false)
    }
  }

  async function handleDelete() {
    if (!canDelete || socio?.estadoSocio !== 'Eliminado') {
      return
    }

    const confirmed = window.confirm(
      `Vas a borrar definitivamente a ${socioName}. Esta accion no se puede deshacer. Continuar?`,
    )

    if (!confirmed) {
      return
    }

    setIsDeleting(true)
    setError('')
    setSuccess('')

    try {
      await sociosService.deleteSocioDefinitivamente(idUsuario)
      setSuccess('Socio borrado definitivamente.')
      onDeleted()
    } catch (requestError) {
      setError(getSociosErrorMessage(requestError))
    } finally {
      setIsDeleting(false)
    }
  }

  async function handlePasswordChange() {
    if (!canChangePassword) {
      return
    }

    if (!passwordState.currentPassword || passwordState.newPassword.length < 8) {
      setError('Completa la contrasena actual y una nueva contrasena de al menos 8 caracteres.')
      setSuccess('')
      return
    }

    setIsChangingPassword(true)
    setError('')
    setSuccess('')

    try {
      const response = await sociosService.changeSocioPassword({
        currentPassword: passwordState.currentPassword,
        newPassword: passwordState.newPassword,
      })

      setIsPasswordAutofillGuardEnabled(true)
      setPasswordState({ visible: false, currentPassword: '', newPassword: '' })
      setSuccess(response || 'Contrasena cambiada correctamente.')
    } catch (requestError) {
      setError(getSociosErrorMessage(requestError))
    } finally {
      setIsChangingPassword(false)
    }
  }

  function renderField(
    field: keyof SocioFormState,
    label: string,
    options?: { type?: string; choices?: string[] },
  ) {
    if (!formState) {
      return null
    }

    const isEditing = Boolean(editingFields[field])
    const value = String(formState[field] ?? '')
    const inputType = options?.type ?? 'text'

    return (
      <label className="consultar-field">
        <span>{label}</span>
        <span className="consultar-field__control">
          {options?.choices ? (
            <select
              className="consultar-input"
              value={value}
              disabled={!isEditing}
              onChange={(event) => setField(field, event.target.value as SocioFormState[typeof field])}
            >
              <option value="">Sin especificar</option>
              {options.choices.map((choice) => (
                <option key={choice} value={choice}>
                  {choice}
                </option>
              ))}
            </select>
          ) : (
            <input
              className="consultar-input"
              type={inputType}
              value={value}
              disabled={!isEditing}
              onChange={(event) => setField(field, event.target.value as SocioFormState[typeof field])}
            />
          )}
          {canEdit ? (
            <button
              className="consultar-edit"
              type="button"
              aria-label={`Editar ${label}`}
              onClick={() => toggleEditing(String(field))}
            >
              Editar
            </button>
          ) : null}
        </span>
      </label>
    )
  }

  function renderPerfilField(field: keyof SocioFormState['perfilIA'], label: string) {
    if (!formState) {
      return null
    }

    const isEditing = Boolean(editingFields[`perfilIA.${field}`])

    return (
      <label className="consultar-field">
        <span>{label}</span>
        <span className="consultar-field__control">
          <input
            className="consultar-input"
            type="text"
            value={formState.perfilIA[field]}
            disabled={!isEditing}
            onChange={(event) => setPerfilField(field, event.target.value)}
          />
          {canEdit ? (
            <button
              className="consultar-edit"
              type="button"
              aria-label={`Editar ${label}`}
              onClick={() => toggleEditing(`perfilIA.${field}`)}
            >
              Editar
            </button>
          ) : null}
        </span>
      </label>
    )
  }

  return (
    <div className="consultar-backdrop" role="presentation">
      <section
        className="consultar-modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="consultar-socio-title"
      >
        <header className="consultar-header">
          <div>
            <h2 id="consultar-socio-title">Consultar Socio: {socioName}</h2>
            <div className="consultar-meta">
              <span className={getStatusClass(socio?.estadoSocio)}>{socio?.estadoSocio ?? '-'}</span>
              <span>ID: {idUsuario}</span>
            </div>
          </div>
          <button className="consultar-close" type="button" aria-label="Cerrar" onClick={requestClose}>
            x
          </button>
        </header>

        <nav className="consultar-tabs" aria-label="Secciones de consulta">
          {TABS.map((tab) => (
            <button
              key={tab.id}
              className={activeTab === tab.id ? 'consultar-tab consultar-tab--active' : 'consultar-tab'}
              type="button"
              onClick={() => setActiveTab(tab.id)}
            >
              <span>{tab.icon}</span>
              {tab.label}
            </button>
          ))}
        </nav>

        <div className="consultar-body">
          {isLoading ? <p className="inicio-status">Cargando datos del socio...</p> : null}
          {error ? <p className="form-alert form-alert--error">{error}</p> : null}
          {success ? <p className="form-alert form-alert--success">{success}</p> : null}

          {!isLoading && formState && usuario ? (
            <>
              {activeTab === 'personal' ? (
                <div className="consultar-section">
                  <h3>Datos Personales</h3>
                  <div className="consultar-grid">
                    {renderField('nombre', 'Nombre')}
                    {renderField('apellido', 'Apellido')}
                    {renderField('email', 'Email', { type: 'email' })}
                    {renderField('telefono', 'Telefono')}
                    {renderField('fechaNacimiento', 'Fecha de Nacimiento', { type: 'date' })}
                    {renderField('genero', 'Genero', {
                      choices: ['Masculino', 'Femenino', 'Otro'],
                    })}
                    {renderField('tipoDocumento', 'Tipo Documento')}
                    {renderField('nroDocumento', 'Nro Documento')}
                    {renderField('ciudad', 'Ciudad')}
                    {renderField('direccion', 'Direccion')}
                    {renderField('obraSocial', 'Obra Social')}
                  </div>

                  <div className="consultar-days">
                    <h3>Dias de Asistencia</h3>
                    <div className="consultar-day-list">
                      {dias.map((dia) => (
                        <label
                          key={dia.idDia}
                          className={
                            selectedDias.includes(dia.idDia)
                              ? 'consultar-day consultar-day--active'
                              : 'consultar-day'
                          }
                        >
                          <input
                            type="checkbox"
                            checked={selectedDias.includes(dia.idDia)}
                            disabled={!canEdit}
                            onChange={() => toggleDia(dia.idDia)}
                          />
                          <span>{dia.nombreDia}</span>
                        </label>
                      ))}
                    </div>
                  </div>
                </div>
              ) : null}

              {activeTab === 'facturacion' ? (
                <div className="consultar-section consultar-section--billing">
                  <div className="consultar-billing-card">
                    <h3>Informacion de Cuota Vigente</h3>
                    <div className="consultar-billing-grid">
                      <div>
                        <span>Plan Actual</span>
                        <strong>{latestCuota?.plan ?? '-'}</strong>
                      </div>
                      <div>
                        <span>Estado</span>
                        <strong>{socio?.estadoSocio ?? '-'}</strong>
                      </div>
                      <div>
                        <span>Vencimiento</span>
                        <strong
                          className={
                            socio?.estadoSocio === 'Suspendido' || socio?.estadoSocio === 'Eliminado'
                              ? 'consultar-danger-text'
                              : ''
                          }
                        >
                          {formatDateCell(latestCuota?.fechaFinPeriodo ?? null)}
                        </strong>
                      </div>
                    </div>
                  </div>

                  {renovacion.active ? (
                    <div className="consultar-renewal">
                      <label className="field-group">
                        <span className="field-label">Plan</span>
                        <select
                          className="field-input"
                          value={renovacion.plan}
                          onChange={(event) => {
                            setRenovacion({ ...renovacion, plan: event.target.value })
                          }}
                        >
                          <option value="Mensual">Mensual</option>
                          <option value="Anual">Anual</option>
                        </select>
                      </label>
                      <label className="field-group">
                        <span className="field-label">Monto</span>
                        <input
                          className="field-input"
                          type="number"
                          min="0"
                          value={renovacion.monto}
                          onChange={(event) => {
                            setRenovacion({ ...renovacion, monto: event.target.value })
                          }}
                        />
                      </label>
                      <button
                        className="ghost-button consultar-renewal__cancel"
                        type="button"
                        onClick={resetRenovacion}
                      >
                        Cancelar renovacion
                      </button>
                    </div>
                  ) : null}

                  <div className="consultar-billing-actions">
                    {canEdit && !renovacion.active ? (
                      <button
                        className="submit-button consultar-button--green"
                        type="button"
                        onClick={() => {
                          setRenovacion({ active: true, plan: 'Mensual', monto: '' })
                        }}
                      >
                        Renovar Cuota
                      </button>
                    ) : null}

                    {socio?.estadoSocio === 'Eliminado' && canDelete ? (
                      <button
                        className="submit-button consultar-button--danger"
                        type="button"
                        disabled={isDeleting}
                        onClick={handleDelete}
                      >
                        {isDeleting ? 'Borrando...' : 'Borrar Definitivamente'}
                      </button>
                    ) : null}
                  </div>
                </div>
              ) : null}

              {activeTab === 'perfilIA' ? (
                <div className="consultar-section">
                  <h3>Configuracion Perfil IA</h3>
                  <div className="consultar-grid">
                    {renderPerfilField('objetivoPrincipal', 'Objetivo Principal')}
                    {renderPerfilField('nivelExperiencia', 'Nivel de Experiencia')}
                    {renderPerfilField('ejerciciosPreferidos', 'Ejercicios Preferidos')}
                    {renderPerfilField('ejerciciosAEvitar', 'Ejercicios a Evitar')}
                    {renderPerfilField('disponibilidadHoraria', 'Disponibilidad Horaria')}
                    {renderPerfilField('motivacionPersonal', 'Motivacion Personal')}
                  </div>
                </div>
              ) : null}

              {activeTab === 'seguridad' ? (
                <div className="consultar-section consultar-section--security">
                  <h3>Seguridad de la Cuenta</h3>
                  {!passwordState.visible ? (
                    canChangePassword ? (
                      <button
                        className="ghost-button"
                        type="button"
                        onClick={() => {
                          setIsPasswordAutofillGuardEnabled(true)
                          setPasswordState({ ...passwordState, visible: true })
                        }}
                      >
                        Cambiar Contrasena
                      </button>
                    ) : (
                      <p className="inicio-status">No tienes permiso para cambiar la contrasena.</p>
                    )
                  ) : (
                    <div className="consultar-password-box">
                      <div aria-hidden="true">
                        <input
                          type="text"
                          name={`${passwordFormKey}-decoy-user`}
                          autoComplete="username"
                          tabIndex={-1}
                          aria-hidden="true"
                          className="autofill-decoy"
                        />
                        <input
                          type="password"
                          name={`${passwordFormKey}-decoy-password`}
                          autoComplete="current-password"
                          tabIndex={-1}
                          aria-hidden="true"
                          className="autofill-decoy"
                        />
                      </div>
                      <label className="field-group">
                        <span className="field-label">Contrasena Actual</span>
                        <input
                          className="field-input"
                          type="password"
                          name={`${passwordFormKey}-current-password`}
                          autoComplete="new-password"
                          readOnly={isPasswordAutofillGuardEnabled}
                          value={passwordState.currentPassword}
                          onMouseDown={() => setIsPasswordAutofillGuardEnabled(false)}
                          onFocus={() => setIsPasswordAutofillGuardEnabled(false)}
                          onChange={(event) =>
                            setPasswordState({
                              ...passwordState,
                              currentPassword: event.target.value,
                            })
                          }
                        />
                      </label>
                      <label className="field-group">
                        <span className="field-label">Nueva Contrasena</span>
                        <input
                          className="field-input"
                          type="password"
                          name={`${passwordFormKey}-next-password`}
                          autoComplete="new-password"
                          readOnly={isPasswordAutofillGuardEnabled}
                          value={passwordState.newPassword}
                          onMouseDown={() => setIsPasswordAutofillGuardEnabled(false)}
                          onFocus={() => setIsPasswordAutofillGuardEnabled(false)}
                          onChange={(event) =>
                            setPasswordState({
                              ...passwordState,
                              newPassword: event.target.value,
                            })
                          }
                        />
                      </label>
                      <div className="consultar-password-actions">
                        <button
                          className="submit-button"
                          type="button"
                          disabled={isChangingPassword}
                          onClick={handlePasswordChange}
                        >
                          {isChangingPassword ? 'Confirmando...' : 'Confirmar Cambio'}
                        </button>
                        <button
                          className="ghost-button"
                          type="button"
                          onClick={() =>
                            {
                              setIsPasswordAutofillGuardEnabled(true)
                              setPasswordState({
                                visible: false,
                                currentPassword: '',
                                newPassword: '',
                              })
                            }
                          }
                        >
                          Cancelar
                        </button>
                      </div>
                    </div>
                  )}
                </div>
              ) : null}
            </>
          ) : null}
        </div>

        <footer className="consultar-footer">
          <button className="ghost-button consultar-footer__close" type="button" onClick={requestClose}>
            Cerrar
          </button>
          {canEdit ? (
            <button
              className="submit-button consultar-footer__save"
              type="button"
              disabled={isSaving || isLoading || !isDirty}
              onClick={handleSave}
            >
              {isSaving ? 'Confirmando...' : 'Confirmar Cambios'}
            </button>
          ) : null}
        </footer>
      </section>
    </div>
  )
}

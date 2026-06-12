import { useEffect, useId, useState } from 'react'
import type { FormEvent } from 'react'
import { sociosService } from '../services/sociosService'
import type { DiaDto, GeneroSocio, PlanSocio, UsuarioInsertDto } from '../types/socio'
import { getSociosErrorMessage } from '../utils/apiError'

const SOCIO_GROUP_ID = 3

const TABS = [
  { id: 'personal', label: 'Datos personales' },
  { id: 'cuota', label: 'Cuota inicial' },
  { id: 'perfilIA', label: 'Perfil IA' },
] as const

type TabId = (typeof TABS)[number]['id']

interface AgregarSocioModalProps {
  onClose: () => void
  onCreated: () => void
}

interface AgregarSocioFormState {
  username: string
  password: string
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
  plan: PlanSocio
  monto: string
  perfilIA: {
    objetivoPrincipal: string
    nivelExperiencia: string
    ejerciciosPreferidos: string
    ejerciciosAEvitar: string
    disponibilidadHoraria: string
    motivacionPersonal: string
  }
}

const initialFormState: AgregarSocioFormState = {
  username: '',
  password: '',
  nombre: '',
  apellido: '',
  email: '',
  telefono: '',
  direccion: '',
  ciudad: '',
  tipoDocumento: '',
  nroDocumento: '',
  genero: '',
  fechaNacimiento: '',
  obraSocial: '',
  fechaNotificacion: '',
  respuestaNotificacion: null,
  pregunta: '',
  respuesta: '',
  plan: 'Mensual',
  monto: '',
  perfilIA: {
    objetivoPrincipal: '',
    nivelExperiencia: '',
    ejerciciosPreferidos: '',
    ejerciciosAEvitar: '',
    disponibilidadHoraria: '',
    motivacionPersonal: '',
  },
}

function toNullable(value: string) {
  const cleanValue = value.trim()
  return cleanValue ? cleanValue : null
}

function buildRegisterPayload(
  formState: AgregarSocioFormState,
  selectedDias: number[],
): UsuarioInsertDto {
  return {
    username: formState.username.trim(),
    password: formState.password,
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
      cuota: {
        plan: formState.plan,
        monto: Number(formState.monto),
      },
      perfilIA: {
        objetivoPrincipal: toNullable(formState.perfilIA.objetivoPrincipal),
        nivelExperiencia: toNullable(formState.perfilIA.nivelExperiencia),
        ejerciciosPreferidos: toNullable(formState.perfilIA.ejerciciosPreferidos),
        ejerciciosAEvitar: toNullable(formState.perfilIA.ejerciciosAEvitar),
        disponibilidadHoraria: toNullable(formState.perfilIA.disponibilidadHoraria),
        motivacionPersonal: toNullable(formState.perfilIA.motivacionPersonal),
      },
    },
    idGrupos: [SOCIO_GROUP_ID],
  }
}

export function AgregarSocioModal({ onClose, onCreated }: AgregarSocioModalProps) {
  const [activeTab, setActiveTab] = useState<TabId>('personal')
  const [dias, setDias] = useState<DiaDto[]>([])
  const [selectedDias, setSelectedDias] = useState<number[]>([])
  const [formState, setFormState] = useState<AgregarSocioFormState>(initialFormState)
  const [isCredentialAutofillGuardEnabled, setIsCredentialAutofillGuardEnabled] = useState(true)
  const [isLoadingDias, setIsLoadingDias] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')

  const formKey = useId()

  useEffect(() => {
    let isActive = true

    async function loadDias() {
      setIsLoadingDias(true)
      setError('')

      try {
        const diasResponse = await sociosService.getDias()

        if (isActive) {
          setDias(diasResponse)
        }
      } catch (requestError) {
        if (isActive) {
          setError(getSociosErrorMessage(requestError))
        }
      } finally {
        if (isActive) {
          setIsLoadingDias(false)
        }
      }
    }

    void loadDias()

    return () => {
      isActive = false
    }
  }, [])

  function setField<K extends keyof AgregarSocioFormState>(
    field: K,
    value: AgregarSocioFormState[K],
  ) {
    setFormState((current) => ({ ...current, [field]: value }))
  }

  function setPerfilField<K extends keyof AgregarSocioFormState['perfilIA']>(
    field: K,
    value: AgregarSocioFormState['perfilIA'][K],
  ) {
    setFormState((current) => ({
      ...current,
      perfilIA: {
        ...current.perfilIA,
        [field]: value,
      },
    }))
  }

  function toggleDia(idDia: number) {
    setSelectedDias((current) =>
      current.includes(idDia) ? current.filter((dia) => dia !== idDia) : [...current, idDia],
    )
  }

  function requestBack() {
    onClose()
  }

  function validateBeforeSave() {
    if (
      !formState.username.trim() ||
      !formState.password ||
      !formState.nombre.trim() ||
      !formState.apellido.trim() ||
      !formState.email.trim() ||
      !formState.tipoDocumento.trim() ||
      !formState.nroDocumento.trim()
    ) {
      return 'Completa usuario, contraseña, nombre, apellido, email y documento.'
    }

    if (formState.password.length < 8) {
      return 'La contraseña debe tener al menos 8 caracteres.'
    }

    if (selectedDias.length === 0) {
      return 'Selecciona al menos un dia de asistencia.'
    }

    if (!formState.plan || Number(formState.monto) <= 0) {
      return 'Indica el plan y un monto de cuota mayor a cero.'
    }

    return ''
  }

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()

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
      await sociosService.registerSocio(buildRegisterPayload(formState, selectedDias))
      setSuccess('Socio creado correctamente.')
      onCreated()
    } catch (requestError) {
      setError(getSociosErrorMessage(requestError))
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="consultar-backdrop" role="presentation">
      <section
        className="consultar-modal agregar-socio-modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="agregar-socio-title"
      >
        <header className="consultar-header">
          <div>
            <span className="section-kicker">Socios / Alta</span>
            <h2 id="agregar-socio-title">Agregar socio</h2>
            <div className="consultar-meta">
              <span className="consultar-status consultar-status--success">Nuevo socio</span>
              <span>Grupo Socio</span>
            </div>
          </div>
          <button
            className="consultar-close"
            type="button"
            aria-label="Cerrar"
            onClick={requestBack}
            disabled={isSaving}
          >
            x
          </button>
        </header>

        <form
          className="agregar-socio-workspace"
          autoComplete="off"
          onSubmit={(event) => void handleSubmit(event)}
        >
          <input
            type="text"
            name={`${formKey}-decoy-user`}
            autoComplete="username"
            tabIndex={-1}
            aria-hidden="true"
            className="autofill-decoy"
          />
          <input
            type="password"
            name={`${formKey}-decoy-password`}
            autoComplete="new-password"
            tabIndex={-1}
            aria-hidden="true"
            className="autofill-decoy"
          />
          <div className="consultar-tabs agregar-socio-tabs" aria-label="Secciones de alta">
            {TABS.map((tab) => (
              <button
                key={tab.id}
                className={activeTab === tab.id ? 'consultar-tab consultar-tab--active' : 'consultar-tab'}
                type="button"
                onClick={() => setActiveTab(tab.id)}
              >
                {tab.label}
              </button>
            ))}
          </div>

          <div className="agregar-socio-body">
            {error ? <p className="form-alert form-alert--error">{error}</p> : null}
            {success ? <p className="form-alert form-alert--success">{success}</p> : null}

            {activeTab === 'personal' ? (
              <section className="consultar-section">
                <h2>Datos personales y acceso</h2>
                <div className="consultar-grid">
                  <label className="consultar-field">
                    <span>Usuario</span>
                    <input
                      className="consultar-input"
                      type="text"
                      name={`${formKey}-member-user`}
                      autoComplete="new-password"
                      readOnly={isCredentialAutofillGuardEnabled}
                      value={formState.username}
                      onMouseDown={() => setIsCredentialAutofillGuardEnabled(false)}
                      onFocus={() => setIsCredentialAutofillGuardEnabled(false)}
                      onChange={(event) => setField('username', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Contraseña</span>
                    <input
                      className="consultar-input"
                      type="password"
                      name={`${formKey}-member-password`}
                      autoComplete="new-password"
                      readOnly={isCredentialAutofillGuardEnabled}
                      value={formState.password}
                      onMouseDown={() => setIsCredentialAutofillGuardEnabled(false)}
                      onFocus={() => setIsCredentialAutofillGuardEnabled(false)}
                      onChange={(event) => setField('password', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Nombre</span>
                    <input
                      className="consultar-input"
                      type="text"
                      autoComplete="off"
                      value={formState.nombre}
                      onChange={(event) => setField('nombre', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Apellido</span>
                    <input
                      className="consultar-input"
                      type="text"
                      autoComplete="off"
                      value={formState.apellido}
                      onChange={(event) => setField('apellido', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Email</span>
                    <input
                      className="consultar-input"
                      type="email"
                      autoComplete="off"
                      value={formState.email}
                      onChange={(event) => setField('email', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Telefono</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.telefono}
                      onChange={(event) => setField('telefono', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Fecha de nacimiento</span>
                    <input
                      className="consultar-input"
                      type="date"
                      value={formState.fechaNacimiento}
                      onChange={(event) => setField('fechaNacimiento', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Genero</span>
                    <select
                      className="consultar-input"
                      value={formState.genero}
                      onChange={(event) =>
                        setField('genero', event.target.value as AgregarSocioFormState['genero'])
                      }
                    >
                      <option value="">Sin especificar</option>
                      <option value="Masculino">Masculino</option>
                      <option value="Femenino">Femenino</option>
                      <option value="Otro">Otro</option>
                      <option value="NoEspecifica">No especifica</option>
                    </select>
                  </label>
                  <label className="consultar-field">
                    <span>Tipo Documento</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.tipoDocumento}
                      onChange={(event) => setField('tipoDocumento', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Nro Documento</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.nroDocumento}
                      onChange={(event) => setField('nroDocumento', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Ciudad</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.ciudad}
                      onChange={(event) => setField('ciudad', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Direccion</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.direccion}
                      onChange={(event) => setField('direccion', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Obra Social</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.obraSocial}
                      onChange={(event) => setField('obraSocial', event.target.value)}
                    />
                  </label>
                </div>

                <div className="consultar-days">
                  <h2>Dias de asistencia</h2>
                  {isLoadingDias ? <p className="inicio-status">Cargando dias disponibles...</p> : null}
                  {!isLoadingDias ? (
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
                            onChange={() => toggleDia(dia.idDia)}
                          />
                          <span>{dia.nombreDia}</span>
                        </label>
                      ))}
                    </div>
                  ) : null}
                </div>
              </section>
            ) : null}

            {activeTab === 'cuota' ? (
              <section className="consultar-section agregar-socio-section--narrow">
                <h2>Cuota inicial</h2>
                <div className="agregar-socio-billing">
                  <label className="field-group">
                    <span className="field-label">Plan</span>
                    <select
                      className="field-input"
                      value={formState.plan}
                      onChange={(event) =>
                        setField('plan', event.target.value as AgregarSocioFormState['plan'])
                      }
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
                      step="0.01"
                      value={formState.monto}
                      onChange={(event) => setField('monto', event.target.value)}
                    />
                  </label>
                </div>
              </section>
            ) : null}

            {activeTab === 'perfilIA' ? (
              <section className="consultar-section">
                <h2>Perfil IA</h2>
                <div className="consultar-grid">
                  <label className="consultar-field">
                    <span>Objetivo Principal</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.perfilIA.objetivoPrincipal}
                      onChange={(event) => setPerfilField('objetivoPrincipal', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Nivel de Experiencia</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.perfilIA.nivelExperiencia}
                      onChange={(event) => setPerfilField('nivelExperiencia', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Ejercicios Preferidos</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.perfilIA.ejerciciosPreferidos}
                      onChange={(event) => setPerfilField('ejerciciosPreferidos', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Ejercicios a Evitar</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.perfilIA.ejerciciosAEvitar}
                      onChange={(event) => setPerfilField('ejerciciosAEvitar', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Disponibilidad Horaria</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.perfilIA.disponibilidadHoraria}
                      onChange={(event) => setPerfilField('disponibilidadHoraria', event.target.value)}
                    />
                  </label>
                  <label className="consultar-field">
                    <span>Motivacion Personal</span>
                    <input
                      className="consultar-input"
                      type="text"
                      value={formState.perfilIA.motivacionPersonal}
                      onChange={(event) => setPerfilField('motivacionPersonal', event.target.value)}
                    />
                  </label>
                </div>
              </section>
            ) : null}
          </div>

          <footer className="consultar-footer agregar-socio-footer">
            <button className="ghost-button consultar-footer__close" type="button" onClick={requestBack}>
              Cancelar
            </button>
            <button
              className="submit-button consultar-footer__save"
              type="submit"
              disabled={isSaving || isLoadingDias}
            >
              {isSaving ? 'Guardando...' : 'Guardar'}
            </button>
          </footer>
        </form>
      </section>
    </div>
  )
}

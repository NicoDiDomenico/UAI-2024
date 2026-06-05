import { useCallback, useEffect, useMemo, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { ConsultarSocioModal } from '../components/socios/ConsultarSocioModal'
import { useAuth } from '../hooks/useAuth'
import { AppLayout } from '../layouts/AppLayout'
import { sociosService } from '../services/sociosService'
import type { SocioGridItem } from '../types/socio'
import { formatDateCell, normalizeDateForSearch } from '../utils/date'
import { getSociosErrorMessage } from '../utils/apiError'

const SEARCH_OPTIONS = [
  { value: 'nombreCompleto', label: 'Socio' },
  { value: 'estadoSocio', label: 'Estado' },
  { value: 'fechaFinPeriodo', label: 'Fecha vencimiento cuota' },
] as const

const CONSULTAR_PERMISSIONS = [
  'EDITAR_USUARIO_SOCIO',
  'CAMBIAR_CONTRASENA_SOCIO',
  'ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE',
] as const

const TURNOS_PERMISSIONS = ['AGREGAR_TURNO', 'CANCELAR_TURNO'] as const

const ACTION_PERMISSIONS = {
  agregar: 'CREAR_USUARIO_SOCIO',
  eliminar: 'ELIMINAR_USUARIO_SOCIO',
} as const

type SearchField = (typeof SEARCH_OPTIONS)[number]['value']

function matchesPermission(userPermissions: readonly string[], requiredPermissions: readonly string[]) {
  return requiredPermissions.some((permission) => userPermissions.includes(permission))
}

function getSearchableValue(socio: SocioGridItem, searchField: SearchField) {
  if (searchField === 'fechaFinPeriodo') {
    return normalizeDateForSearch(socio.fechaFinPeriodo)
  }

  return String(socio[searchField] ?? '')
    .trim()
    .toLocaleLowerCase('es-AR')
}

function getSocioDisplayName(socio: SocioGridItem) {
  return socio.nombreCompleto?.trim() || socio.username
}

export function SociosPage() {
  const navigate = useNavigate()
  const { idUsuario } = useParams()
  const { session } = useAuth()
  const userPermissions = session?.permisos ?? []
  const [socios, setSocios] = useState<SocioGridItem[]>([])
  const [selectedSocioId, setSelectedSocioId] = useState<number | null>(null)
  const [showDeleted, setShowDeleted] = useState(false)
  const [searchField, setSearchField] = useState<SearchField>('nombreCompleto')
  const [searchValue, setSearchValue] = useState('')
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState('')

  const canAgregar = userPermissions.includes(ACTION_PERMISSIONS.agregar)
  const canConsultar = matchesPermission(userPermissions, CONSULTAR_PERMISSIONS)
  const canEliminar = userPermissions.includes(ACTION_PERMISSIONS.eliminar)
  const canTurnos = matchesPermission(userPermissions, TURNOS_PERMISSIONS)
  const consultarSocioId = idUsuario ? Number(idUsuario) : null

  const loadSocios = useCallback(
    async (options?: { keepLoading?: boolean }) => {
      if (options?.keepLoading !== false) {
        setIsLoading(true)
      }
      setError('')

      try {
        await sociosService.actualizarCuotasVencidas()
        await sociosService.procesarEliminacionesPendientes()
        const response = await sociosService.getSociosGrid()
        setSocios(response)
      } catch (requestError) {
        setError(getSociosErrorMessage(requestError))
      } finally {
        setIsLoading(false)
      }
    },
    [],
  )

  useEffect(() => {
    let isActive = true

    async function loadInitialSocios() {
      if (isActive) {
        await loadSocios()
      }
    }

    void loadInitialSocios()

    return () => {
      isActive = false
    }
  }, [loadSocios])

  useEffect(() => {
    if (!consultarSocioId || Number.isNaN(consultarSocioId)) {
      return
    }

    setSelectedSocioId(consultarSocioId)
  }, [consultarSocioId])

  const visibleSocios = useMemo(() => {
    const normalizedSearch = searchValue.trim().toLocaleLowerCase('es-AR')

    return socios
      .filter((socio) => showDeleted || socio.estadoSocio !== 'Eliminado')
      .filter((socio) => {
        if (!normalizedSearch) {
          return true
        }

        return getSearchableValue(socio, searchField).includes(normalizedSearch)
      })
  }, [searchField, searchValue, showDeleted, socios])

  const selectedSocio = useMemo(
    () => socios.find((socio) => socio.idUsuario === selectedSocioId) ?? null,
    [selectedSocioId, socios],
  )

  useEffect(() => {
    if (!selectedSocioId) {
      return
    }

    const stillVisible = visibleSocios.some((socio) => socio.idUsuario === selectedSocioId)

    if (!stillVisible) {
      setSelectedSocioId(null)
    }
  }, [selectedSocioId, visibleSocios])

  function goToSelected(pathBuilder: (idUsuario: number) => string) {
    if (!selectedSocio) {
      return
    }

    navigate(pathBuilder(selectedSocio.idUsuario))
  }

  return (
    <AppLayout>
      <main className="socios-page">
        <section className="socios-intro">
          <div>
            <span className="section-kicker">Socios / Operacion</span>
            <h1 className="dashboard-title">Ver socios</h1>
            <p className="dashboard-copy">
              Consulta el estado de cada socio, filtra la grilla y entra a las acciones que
              tienes habilitadas.
            </p>
          </div>
          <Link className="ghost-button socios-backlink" to="/dashboard">
            Volver a Inicio
          </Link>
        </section>

        <section className="socios-workspace" aria-labelledby="socios-grid-title">
          <div className="socios-toolbar">
            <div>
              <span className="section-kicker">Listado operativo</span>
              <h2 id="socios-grid-title">Lista de socios</h2>
            </div>

            <div className="socios-filters" aria-label="Filtros de busqueda">
              <label className="field-group socios-filter-field">
                <span className="field-label">Buscar por</span>
                <select
                  className="field-input"
                  value={searchField}
                  onChange={(event) => setSearchField(event.target.value as SearchField)}
                >
                  {SEARCH_OPTIONS.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </select>
              </label>

              <label className="field-group socios-filter-field socios-filter-field--search">
                <span className="field-label">Valor</span>
                <input
                  className="field-input"
                  type="text"
                  value={searchValue}
                  onChange={(event) => setSearchValue(event.target.value)}
                  placeholder="Filtrar resultados"
                />
              </label>
            </div>
          </div>

          <label className="socios-toggle">
            <input
              type="checkbox"
              checked={showDeleted}
              onChange={(event) => setShowDeleted(event.target.checked)}
            />
            <span>Mostrar socios eliminados</span>
          </label>

          {isLoading ? <p className="inicio-status">Actualizando estados y cargando socios...</p> : null}
          {error ? <p className="inicio-status inicio-status--error">{error}</p> : null}

          {!isLoading && !error ? (
            <div className="socios-table-wrap">
              <table className="socios-table">
                <thead>
                  <tr>
                    <th aria-label="Seleccion" />
                    <th>Socio</th>
                    <th>Estado</th>
                    <th>Fecha vencimiento cuota</th>
                  </tr>
                </thead>
                <tbody>
                  {visibleSocios.length === 0 ? (
                    <tr>
                      <td className="socios-empty" colSpan={4}>
                        No encontramos socios para los filtros actuales.
                      </td>
                    </tr>
                  ) : (
                    visibleSocios.map((socio) => {
                      const isSelected = socio.idUsuario === selectedSocioId
                      const isAlertState =
                        socio.estadoSocio === 'Eliminado' || socio.estadoSocio === 'Suspendido'

                      return (
                        <tr
                          key={socio.idUsuario}
                          className={isSelected ? 'socios-row socios-row--selected' : 'socios-row'}
                          onClick={() => setSelectedSocioId(socio.idUsuario)}
                        >
                          <td data-label="Seleccion">
                            <span
                              className={
                                isSelected
                                  ? 'socios-radio socios-radio--selected'
                                  : 'socios-radio'
                              }
                              aria-hidden="true"
                            />
                          </td>
                          <td
                            data-label="Socio"
                            className={isAlertState ? 'socios-cell socios-cell--alert' : 'socios-cell'}
                          >
                            <strong>{getSocioDisplayName(socio)}</strong>
                          </td>
                          <td
                            data-label="Estado"
                            className={isAlertState ? 'socios-cell socios-cell--alert' : 'socios-cell'}
                          >
                            {socio.estadoSocio}
                          </td>
                          <td
                            data-label="Fecha vencimiento cuota"
                            className={isAlertState ? 'socios-cell socios-cell--alert' : 'socios-cell'}
                          >
                            {formatDateCell(socio.fechaFinPeriodo)}
                          </td>
                        </tr>
                      )
                    })
                  )}
                </tbody>
              </table>
            </div>
          ) : null}

          <div className="socios-actions">
            {canAgregar ? (
              <button className="submit-button socios-action socios-action--primary" type="button" onClick={() => navigate('/socios/agregar')}>
                Agregar
              </button>
            ) : null}

            {canConsultar ? (
              <button
                className="ghost-button socios-action"
                type="button"
                disabled={!selectedSocio}
                onClick={() => goToSelected((idUsuario) => `/socios/${idUsuario}/consultar`)}
              >
                Consultar
              </button>
            ) : null}

            {canEliminar ? (
              <button
                className="ghost-button socios-action"
                type="button"
                disabled={!selectedSocio}
                onClick={() => goToSelected((idUsuario) => `/socios/${idUsuario}/eliminar`)}
              >
                Eliminar
              </button>
            ) : null}

            {canTurnos ? (
              <button
                className="ghost-button socios-action"
                type="button"
                disabled={!selectedSocio}
                onClick={() => goToSelected((idUsuario) => `/socios/${idUsuario}/turnos`)}
              >
                Turnos
              </button>
            ) : null}
          </div>
        </section>

        {consultarSocioId && !Number.isNaN(consultarSocioId) && canConsultar ? (
          <ConsultarSocioModal
            idUsuario={consultarSocioId}
            userPermissions={userPermissions}
            onClose={() => navigate('/socios')}
            onDeleted={() => {
              navigate('/socios')
              void loadSocios({ keepLoading: false })
            }}
            onUpdated={() => void loadSocios({ keepLoading: false })}
          />
        ) : null}
      </main>
    </AppLayout>
  )
}

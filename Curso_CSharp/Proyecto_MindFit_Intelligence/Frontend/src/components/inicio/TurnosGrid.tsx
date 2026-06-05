import type { TurnoDetalle } from '../../types/turno'

interface TurnosGridProps {
  turnos: readonly TurnoDetalle[]
  isLoading: boolean
  error: string
}

export function TurnosGrid({ turnos, isLoading, error }: TurnosGridProps) {
  return (
    <section className="turnos-section">
      <div className="section-heading">
        <div>
          <span className="section-kicker">Agenda operativa</span>
          <h2>Turnos del dia</h2>
        </div>
        <span className="section-count">{turnos.length} registrados</span>
      </div>

      {isLoading ? <p className="inicio-status">Cargando turnos del dia...</p> : null}
      {error ? <p className="inicio-status inicio-status--error">{error}</p> : null}
      {!isLoading && !error && turnos.length === 0 ? (
        <p className="inicio-status">No hay turnos registrados para hoy.</p>
      ) : null}

      {turnos.length > 0 ? (
        <div className="turnos-table-wrap">
          <table className="turnos-table">
            <thead>
              <tr>
                <th>Hora</th>
                <th>Socio</th>
                <th>Entrenador</th>
                <th>Cupos</th>
                <th>Estado</th>
              </tr>
            </thead>
            <tbody>
              {turnos.map((turno) => (
                <tr key={turno.idTurno}>
                  <td data-label="Hora">
                    <strong>{turno.hora}</strong>
                  </td>
                  <td data-label="Socio">{turno.socio}</td>
                  <td data-label="Entrenador">{turno.entrenador}</td>
                  <td data-label="Cupos">{turno.cupos}</td>
                  <td data-label="Estado">
                    <span className="turno-state">{turno.estadoTurno}</span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : null}
    </section>
  )
}

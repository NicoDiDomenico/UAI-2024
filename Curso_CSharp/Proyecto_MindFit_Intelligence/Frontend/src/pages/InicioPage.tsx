import { NavigationPanel } from '../components/inicio/NavigationPanel'
import { TurnosGrid } from '../components/inicio/TurnosGrid'
import { useAuth } from '../hooks/useAuth'
import { useInicioData } from '../hooks/useInicioData'
import { AppLayout } from '../layouts/AppLayout'
import { formatLocalDateForDisplay } from '../utils/date'
import { getVisibleNavigationItems } from '../utils/navigationPermissions'

export function InicioPage() {
  const { session } = useAuth()
  const {
    formularios,
    turnos,
    today,
    isLoadingFormularios,
    isLoadingTurnos,
    formulariosError,
    turnosError,
  } = useInicioData()
  const visibleNavigationItems = getVisibleNavigationItems(session?.permisos ?? [], formularios)

  return (
    <AppLayout>
      <main className="dashboard-page">
        <section className="inicio-intro">
          <div>
            <span className="inicio-eyebrow">Inicio / Operacion diaria</span>
            <h1 className="dashboard-title">Todo listo para hoy.</h1>
            <p className="dashboard-copy">
              Revisa la agenda y entra directamente a las secciones que tienes habilitadas.
            </p>
          </div>
          <time className="inicio-date" dateTime={today.toISOString()}>
            <span>Fecha de trabajo</span>
            <strong>{formatLocalDateForDisplay(today)}</strong>
          </time>
        </section>

        <div className="inicio-workspace">
          <NavigationPanel
            items={visibleNavigationItems}
            isLoading={isLoadingFormularios}
            error={formulariosError}
          />
          <TurnosGrid turnos={turnos} isLoading={isLoadingTurnos} error={turnosError} />
        </div>
      </main>
    </AppLayout>
  )
}

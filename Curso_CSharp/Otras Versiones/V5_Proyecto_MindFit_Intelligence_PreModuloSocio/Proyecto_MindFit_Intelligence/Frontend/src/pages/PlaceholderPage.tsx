import { Link } from 'react-router-dom'
import { AppLayout } from '../layouts/AppLayout'

interface PlaceholderPageProps {
  title: string
}

export function PlaceholderPage({ title }: PlaceholderPageProps) {
  return (
    <AppLayout>
      <main className="placeholder-page">
        <section className="placeholder-card">
          <span className="section-kicker">Modulo en preparacion</span>
          <h1 className="placeholder-title">{title}</h1>
          <p className="placeholder-copy">Proximamente</p>
          <Link className="placeholder-link" to="/dashboard">
            Volver a Inicio
          </Link>
        </section>
      </main>
    </AppLayout>
  )
}

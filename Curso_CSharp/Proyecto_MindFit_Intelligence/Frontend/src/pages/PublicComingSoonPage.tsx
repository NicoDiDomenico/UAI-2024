import { Link } from 'react-router-dom'
import { LandingHeader } from '../components/landing/LandingHeader'

interface PublicComingSoonPageProps {
  title: string
}

export function PublicComingSoonPage({ title }: PublicComingSoonPageProps) {
  return (
    <main className="landing-page">
      <LandingHeader />
      <section className="landing-coming-soon" aria-labelledby="coming-soon-title">
        <span>MindFit Intelligence</span>
        <h1 id="coming-soon-title">{title}</h1>
        <p>Próximamente...</p>
        <Link className="landing-button" to="/">
          Volver al inicio
        </Link>
      </section>
    </main>
  )
}

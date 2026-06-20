import type { CSSProperties } from 'react'
import digitalizacionImage from '../assets/blog-digitalizacion.png'
import heroImage from '../assets/blog-hero.png'
import iaImage from '../assets/blog-ia.png'
import marketingImage from '../assets/blog-marketing.png'
import retencionImage from '../assets/blog-retencion.png'
import sociosImage from '../assets/blog-socios.png'
import tendenciasImage from '../assets/blog-tendencias.png'
import { LandingHeader } from '../components/landing/LandingHeader'
import { PublicFooter } from '../components/landing/PublicFooter'

const articles = [
  { title: 'Cómo digitalizar tu gimnasio', image: digitalizacionImage },
  { title: 'IA aplicada al entrenamiento', image: iaImage },
  { title: 'Gestión eficiente de socios', image: sociosImage },
  { title: 'Marketing digital para gimnasios', image: marketingImage },
  { title: 'Retención de clientes', image: retencionImage },
  { title: 'Nuevas tendencias en fitness', image: tendenciasImage },
]

const categories = ['Gestión', 'Marketing', 'Tecnología', 'Tendencias', 'Clientes']

export function BlogPage() {
  return (
    <main className="landing-page blog-page">
      <LandingHeader />

      <div className="blog-content">
        <section className="blog-main" aria-label="Artículos del blog">
          <div className="blog-hero">
            <img
              src={heroImage}
              alt="Ilustración editorial de un equipo colaborando entre plantas"
            />
          </div>

          <div className="blog-grid">
            {articles.map((article, index) => (
              <article
                className="blog-article"
                key={article.title}
                style={{ '--blog-order': index } as CSSProperties}
              >
                <div className="blog-article__image">
                  <img src={article.image} alt="" aria-hidden="true" />
                </div>
                <h2>{article.title}</h2>
              </article>
            ))}
          </div>
        </section>

        <aside className="blog-categories" aria-labelledby="blog-categories-title">
          <h1 id="blog-categories-title">Categorías</h1>
          <ul>
            {categories.map((category) => (
              <li key={category}>{category}</li>
            ))}
          </ul>
        </aside>
      </div>
      <PublicFooter />
    </main>
  )
}

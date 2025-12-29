import { NavLink } from "react-router";

export default function Menu() {
  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container-fluid">
        <NavLink className="navbar-brand" to="/">
          React Películas
        </NavLink>

        <div className="collapse navbar-collapse">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink className="nav-link" to="/peliculas/filtrar">
                Filtrar Peliculas
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/generos">
                Géneros
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/actores">
                Actores
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/cines">
                Cines
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/peliculas/crear">
                Crear Películas
              </NavLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}

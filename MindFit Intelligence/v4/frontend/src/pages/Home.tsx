import { useAuth } from "../hooks/useAuth";
import { Button } from "../components/ui/Button";
import { useNavigate } from "react-router-dom";

export const Home = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <div className="home-container">
      <div className="home-header">
        <h1>Bienvenido, {user?.persona.nombreYApellido}</h1>
        <Button variant="secondary" onClick={handleLogout}>
          Cerrar Sesión
        </Button>
      </div>

      <div className="home-content">
        <div className="info-card">
          <h2>Información del Usuario</h2>
          <p>
            <strong>Usuario:</strong> {user?.nombreUsuario}
          </p>
          <p>
            <strong>Email:</strong> {user?.persona.email}
          </p>
          <p>
            <strong>Gimnasio:</strong> {user?.gym.nombre}
          </p>
          <p>
            <strong>Roles:</strong> {user?.roles.join(", ")}
          </p>
          <p>
            <strong>Permisos:</strong> {user?.permisos.length} permisos asignados
          </p>
        </div>
      </div>
    </div>
  );
};

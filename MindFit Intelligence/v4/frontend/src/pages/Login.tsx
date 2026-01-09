import { useState, useEffect } from "react";
import type { FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import { gymService, authService } from "../services/api.service";
import type { Gym } from "../types/auth.types";
import { Input } from "../components/ui/Input";
import { Button } from "../components/ui/Button";
import { Select } from "../components/ui/Select";

export const Login = () => {
  const navigate = useNavigate();
  const { login, isAuthenticated } = useAuth();

  const [gyms, setGyms] = useState<Gym[]>([]);
  const [gymId, setGymId] = useState("");
  const [nombreUsuario, setNombreUsuario] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/home");
    }
  }, [isAuthenticated, navigate]);

  useEffect(() => {
    gymService
      .getGyms()
      .then(setGyms)
      .catch(() => setError("Error al cargar gimnasios"));
  }, []);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError("");

    if (!gymId || !nombreUsuario || !password) {
      setError("Todos los campos son requeridos");
      return;
    }

    setIsLoading(true);

    try {
      const response = await authService.login({
        gymId: parseInt(gymId),
        nombreUsuario,
        password,
      });

      login(response.token, response.usuario);
      navigate("/home");
    } catch (err) {
      const message =
        (err as { response?: { data?: { error?: string } } }).response?.data?.error || "Error al iniciar sesión";
      setError(message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <h1 className="login-title">MindFit Intelligence</h1>
        
        <form onSubmit={handleSubmit} className="login-form">
          <Select
            label="Seleccionar Gimnasio"
            value={gymId}
            onChange={(e) => setGymId(e.target.value)}
            options={gyms.map((gym) => ({
              value: gym.gymId,
              label: gym.nombre,
            }))}
            disabled={isLoading}
          />

          <Input
            type="text"
            label="Usuario"
            value={nombreUsuario}
            onChange={(e) => setNombreUsuario(e.target.value)}
            placeholder="Ingrese su usuario"
            disabled={isLoading}
          />

          <Input
            type="password"
            label="Contraseña"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Ingrese su contraseña"
            disabled={isLoading}
          />

          {error && <div className="error-message">{error}</div>}

          <Button type="submit" disabled={isLoading}>
            {isLoading ? "Ingresando..." : "INICIAR SESIÓN"}
          </Button>
        </form>
      </div>
    </div>
  );
};

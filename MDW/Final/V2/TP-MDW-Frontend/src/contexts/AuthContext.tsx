import React, {
  createContext,
  useContext,
  useState,
  ReactNode,
  useEffect,
} from "react";
import axiosInstance from "../utils/axiosInstance";
import { AxiosError } from "axios";

export interface User {
  id: string;
  email: string;
  name: string;
  lastname: string;
  birthdate: string;
}

interface AuthContextProps {
  user: User | null;
  login: (user: User) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined); // Este contexto sirve como un "contenedor global" para guardar el estado de autenticación (user) y las funciones (login, logout).

/* AuthProvider es quien llena la caja (AuthContext) con valores específicos (user, login, logout) y los comparte. */
export const AuthProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<User | null>(null); // con <User | null> estamos tipando el estado user, entonces puede tomar esos 2 valores

  //Traigo al usuario desde el back con su token
  useEffect(() => {
    /* NICOLÁS PELOTUDO getUserData SE DEFINE NOMAS PERO SE EJECUTA DESPUES DEL IF */
    // Este useEffect verifica si hay un token en localStorage al iniciar la aplicación.
    // Si encuentra un token, intenta obtener los datos del usuario desde el backend.
    // Si no hay token (primera visita o no autenticado), no hace nada y el estado `user` queda como null.
    // localStorage permite que el token persista incluso después de cerrar la página,
    // por lo que se puede reutilizar en futuras sesiones para autenticar automáticamente al usuario.
    const getUserData = async () => {
      /* Esta función hace una solicitud al backend (/users/user-info) para obtener los datos del usuario autenticado. */
      try {
        const res = await axiosInstance.get("/users/user-info");
        setUser(
          res.data.user
        ); /* Si la solicitud es exitosa (res.data.user), se actualiza el estado user con la información del usuario. */
      } catch (error) {
        if (error instanceof AxiosError) {
          localStorage.removeItem(
            "token"
          ); /* Si falla (por ejemplo, porque el token es inválido o ha expirado), se elimina el token del almacenamiento local con localStorage.removeItem("token"). */
        }
      }
    };

    if (localStorage.getItem("token") && !user) {
      /* Verifica si hay un token guardado en localStorage y si el usuario no está cargado en el estado (user === null). */
      getUserData(); /* Si ambas condiciones son verdaderas, llama a getUserData() para cargar los datos del usuario desde el backend. */
    }
    console.log(user); /* eliminar */
  }, [user]);

  /* Las funciones login y logout no dependen del useEffect. Son independientes y están disponibles todo el tiempo. */
  // Las constantes como login y logout que defines dentro del componente AuthProvider no se ejecutan automáticamente al renderizar el componente, sino que simplemente se definen como funciones.
  const login = (user: User) =>
    setUser(
      user
    ); /* Se utiliza para iniciar sesión. Toma los datos del usuario como argumento y los guarda en el estado user. */
  const logout = () =>
    setUser(
      null
    ); /* Se utiliza para cerrar sesión. Simplemente limpia el estado user (lo establece en null). */

  /* 
  Aquí es donde compartes el contexto (user) con los componentes hijos.
  value={{ user }} define lo que será accesible para los componentes que usen este contexto.
  {children} representa todos los componentes hijos envueltos por AuthProvider.
  */
  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {" "}
      {/* AuthContext.Provider comparte valores a los hijos, estos son los componentes envueltos por AuthProvider en main.tsx y que pueden acceder a esos datos.*/}
      {children}
    </AuthContext.Provider>
  );
};

/* useAuth() es un hook personalizado que facilita el acceso al contexto de autenticación (AuthContext) */
export const useAuth = (): AuthContextProps => {
  const context =
    useContext(
      AuthContext
    ); /* useContext(AuthContext) llama al contexto AuthContext para obtener su valor actual. */
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context; // Devuelve el valor del contexto (user, login, logout).
};

/* Por lo tanto el flujo es AuthProvider → AuthContext → useAuth */

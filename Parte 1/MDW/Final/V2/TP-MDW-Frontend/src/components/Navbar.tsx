import { ReactNode, useState } from "react";
import { useNavigate } from "react-router-dom";

import Footer from "./Footer";
import SearchBar from "./SearchBar";
import { useAuth } from "../contexts/AuthContext";
import { notifySuccess } from "../utils/toastFunctions";

/* const hola = ({name}: {name: String}) => {

} */

const Navbar = ({
  children, // Contenido que se renderiza entre la apertura y cierre del componente Navbar.
  searchNote, // Función para buscar una nota (viene desde Home).
  handleClearSearch, // Función para limpiar la búsqueda (viene desde Home).
}: {
  children: ReactNode; // Tipo de prop: cualquier contenido React.
  searchNote?: (query: string) => Promise<void>; // Función opcional que recibe un string (búsqueda) y retorna una promesa.
  handleClearSearch?: () => void; // Función opcional sin parámetros que limpia la búsqueda.
}) => {
  const { user, logout } = useAuth(); // Obtiene el usuario autenticado y la función logout del contexto AuthContext.
  const navigate = useNavigate(); // Hook para redireccionar a otras rutas.

  const [searchQuery, setSearchQuery] = useState(""); // Estado para el valor actual del input de búsqueda.

  //Manejador para buscar una nota
  const handleSearch = async () => {
    if (searchQuery && searchNote) {
      // Solo se ejecuta si hay un valor en el input y si searchNote está definido.
      await searchNote(searchQuery); // Llama a la función searchNote con el texto de búsqueda como parámetro.
    }
  };

  //Manejador para limpiar la busqueda
  const onClearSearch = () => {
    if (handleClearSearch) {
      // Solo se ejecuta si la función handleClearSearch está definida. --> Esto creo que es asi porque en register o login el navbar no pasa estas propiedades
      setSearchQuery(""); // Borra el estado local del buscador.
      handleClearSearch(); // Llama a la función para limpiar las notas filtradas.
    }
  };

  //Manejador para cerrar sesion
  const handleLogout = () => {
    localStorage.removeItem("token"); // Elimina el token almacenado en el navegador.
    logout(); // Limpia el estado global del usuario llamando a logout desde el contexto.
    navigate("/login"); // Redirige al usuario a la página de login.
    notifySuccess("Sesión cerrada correctamente"); // Muestra un mensaje de éxito.
  };

  return (
    <div className="h-screen flex flex-col justify-between">
      {/* Navbar (Barra superior) */}
      <nav className="bg-white flex items-center justify-between px-6 py-2 drop-shadow">
        <h2 className="text-xl font-medium text-black py-2">Notas</h2>
        {user && ( // Esta logica tambien es para no mostrar la barra de busqueda si estoy en login o register
          <SearchBar
            value={searchQuery} // Pasa el estado actual de búsqueda al input.
            handleSearch={handleSearch} // Lógica de búsqueda.
            onClearSearch={onClearSearch} // Lógica para limpiar la búsqueda.
            onChange={(e) => {
              // La prop onChange en un input o textarea es un manejador de eventos que se dispara cada vez que el valor del input cambia (es decir, cuando el usuario escribe, borra o modifica el contenido del input).
              setSearchQuery(e.target.value); // Actualiza el estado local con el texto del input. O sea basicamente cada cambio de la palabra que estoy formando en la busqueda es el mismo cambio para la variable de estado.
            }}
          />
        )}{" "}
        {/* Estoy haciendo un if sin un else --> (condicion) && (Si es True se ejecuta esto)*/}
        {user && <button onClick={handleLogout}>Cerrar sesión</button>}
      </nav>

      {children}

      <Footer />
    </div>
  );
};

export default Navbar;

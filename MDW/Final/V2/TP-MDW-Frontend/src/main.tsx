/* Este es el archivo donde React se conecta al DOM y define cómo se renderiza la aplicación. */
import { createRoot } from "react-dom/client"; // createRoot es la forma en la que React 18 maneja el renderizado de la aplicación. Permite que React "controle" el HTML a partir del elemento raíz (#root).
import { AuthProvider } from "./contexts/AuthContext.tsx"; // Importa un contexto llamado AuthProvider. Este es un Contexto de React, que probablemente maneja la autenticación del usuario en la aplicación.
import App from "./App.tsx"; // Importa el componente principal App. Es el "cerebro" de la aplicación que probablemente contiene las rutas y vistas principales.

import "./index.css"; // Importa los estilos globales para la aplicación. Aquí se incluyen estilos que afectan a toda la app.
import "react-toastify/dist/ReactToastify.css"; // Importa estilos específicos para React Toastify, una biblioteca que probablemente se usa para mostrar notificaciones (mensajes emergentes).

// Selecciona el elemento HTML con id="root" del archivo index.html
// Este es el punto donde React "monta" toda la aplicación dentro del DOM del navegador.
// El "!" después de getElementById le dice a TypeScript que estamos seguros de que este elemento no será null.
createRoot(document.getElementById("root")!).render(
  /* <AuthProvider> es un componente que alguien creó como "contexto" para manejar la autenticación del usuario. Un contexto en React es una herramienta que permite compartir datos (como la información de autenticación, tema oscuro/claro, idioma, etc.) entre múltiples componentes sin necesidad de pasar esos datos manualmente como props a través de cada nivel de la jerarquía de componentes.
  Sirve para:
  1. Guardar información sobre el usuario (por ejemplo, si está autenticado o no, su token, etc.).
  2. Compartir esa información con otros componentes de la app sin tener que pasarla manualmente como propiedades a cada uno.
  En términos simples: <AuthProvider> actúa como el "cerebro" que controla quién tiene acceso a ciertas partes de la aplicación. */
  <AuthProvider>
    {/* `App` es el componente principal de la aplicación. */}
    {/* Maneja las rutas y los componentes principales de toda la estructura. */}
    <App />
  </AuthProvider>
);

/*Conclusión: React inserta los componentes AuthProvider y App dentro del <div id="root"> en el DOM del navegador.*/

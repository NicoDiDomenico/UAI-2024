import { BrowserRouter, Route } from "react-router-dom";
// Route: Define una ruta específica de la aplicación. Cada ruta se asocia a un componente que se renderiza cuando la URL coincide con el path de la ruta.
// BrowserRouter: sin <BrowserRouter> no puedes usar lógica de rutas en React. Esto es porque BrowserRouter es el componente que proporciona el contexto necesario para que toda la lógica de enrutamiento funcione en tu aplicación.
import { lazy } from "react"; // lazy: Es una función de React que permite la carga diferida (lazy loading) de componentes. Solo se carga el código del componente cuando realmente se necesita, mejorando el rendimiento.
import { PrivateRoutes, PublicRoutes } from "../models/routes.model"; // PrivateRoutes y PublicRoutes: Son objetos o constantes que probablemente contienen las rutas organizadas de manera lógica (públicas y privadas) para estandarizar el uso de las rutas en la aplicación.

/* Componentes cargados de forma diferida - no se cargan inmediatamente al iniciar la aplicación, sino hasta que son necesarios, como cuando el usuario navega a la ruta correspondiente. */
const NotFound = lazy(() => import("../pages/NotFound/NotFound"));
const Home = lazy(() => import("../pages/Home/Home"));
const Login = lazy(() => import("../pages/Login/Login"));
const Register = lazy(() => import("../pages/Register/Register"));

const AppRouter = () => {
  // AppRouter organiza las rutas de la aplicación
  return (
    <BrowserRouter>
      {" "}
      {/* BrowserRouter proporciona el contexto de navegacióes decir, habilita la navegación.Dentro de él, defines todas las rutas de tu aplicación. */}
      <NotFound>
        <Route path={PublicRoutes.LOGIN} element={<Login />} />
        <Route path={PublicRoutes.REGISTER} element={<Register />} />

        <Route path={PrivateRoutes.HOME} element={<Home />} />
        {/* Cuando accedes a la aplicación por primera vez, React Router detecta la URL actual. Si no escribes una ruta específica (por ejemplo, solo accedes a http://localhost:5173), se considera la raíz (/). */}
      </NotFound>
    </BrowserRouter>
  );
};

export default AppRouter;

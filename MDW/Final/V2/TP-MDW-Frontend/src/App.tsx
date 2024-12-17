import { Suspense } from "react"; // Suspense: Es un **componente de React** que sirve para manejar "esperas" (loading) mientras otros componentes o datos se cargan.
import AppRouter from "./routes/AppRouter"; // AppRouter: Es un **componente personalizado** que organiza las rutas de la aplicación (decide qué "página" mostrar según la URL).
import { ToastContainer } from "react-toastify"; // ToastContainer: Es un **componente externo** de una biblioteca que se encarga de mostrar notificaciones emergentes (mensajes tipo alerta).
import { SpinnerCircularFixed } from "spinners-react"; // SpinnerCircularFixed: Es un **componente externo** que representa un spinner (un círculo animado que indica carga).

// Componente principal de la aplicación
const App = () => {
  // App: Es un **componente funcional de React**, el "componente raíz" de toda la aplicación.
  return (
    <>
      <Suspense
        fallback={
          <main className="w-full h-screen flex justify-center items-center">
            <SpinnerCircularFixed color="#2B85FF" />
            {/* SpinnerCircularFixed: Un elemento visual que aparece mientras el contenido se carga.*/}
          </main>
        }
      >
        <main className="">
          <AppRouter />
          {/* AppRouter: Este componente maneja la navegación de la aplicación. Según la URL actual, muestra el componente correspondiente (por ejemplo, la página de inicio, el login, etc.). */}
          <ToastContainer />
          {/* ToastContainer: Este componente renderiza (muestra en pantalla) las notificaciones que se generan en cualquier parte de la aplicación (porque lo puse en app). Cuando digo "renderizar notificaciones", me refiero a que toma los mensajes que envías mediante funciones como toast.success o toast.error y los muestra visualmente en la interfaz del usuario. */}
          {/* Sin ToastContainer, no verías ninguna notificación aunque llames a notifySuccess o toast.success(). */}
        </main>
      </Suspense>{" "}
      {/* React.lazy en AppRouter depende de <Suspense> para manejar la carga asíncrona del componente y mostrar un contenido de respaldo mientras el componente se carga. Si intentas renderizar un componente cargado con React.lazy sin envolverlo en <Suspense>, verás un error que significa que React no sabe qué mostrar mientras espera a que el componente se cargue. */}
    </>
  );
};
export default App;

/* 
Forma general de suspense:
  <Suspense fallback={<p>Cargando...</p>}>
    <SomeLazyComponent />
  </Suspense>

fallback: Es lo que se muestra mientras los componentes hijos están esperando datos o recursos
Componentes hijos (<SomeLazyComponent />): Son componentes que pueden tardar en cargarse. Suspense los envuelve para que muestre el fallback mientras están en proceso.
*/

/* 
Diferencias en la escritura de componentes

Componentes autocerrados (<AppRouter />, <ToastContainer />):
  Forma: <Componente />
  - Cuándo se usa:
  - Cuando el componente no tiene hijos que necesitan ser definidos dentro de él.
    Es simplemente una unidad funcional independiente que no encapsula otros componentes ni contenido adicional.
  - Ejemplos:
    <AppRouter />: No necesita incluir o manejar otros componentes dentro de sí.
    <ToastContainer />: Funciona de manera independiente y no encapsula otros elementos.

Componentes con apertura y cierre (<AuthProvider> ... </AuthProvider>):
  - Forma: <Componente> ... </Componente>
  - Cuándo se usa:
    Cuando el componente actúa como un contenedor y necesita encapsular componentes hijos.
    Los componentes hijos (o el contenido que está dentro de las etiquetas) se pasan automáticamente como un prop especial llamado children.
  - Ejemplo:
    <AuthProvider> encapsula a <App />, lo que significa que AuthProvider tiene acceso a App como su children.

Impacto en el flujo u orden de ejecución
Componentes autocerrados:
  - Se ejecutan de arriba hacia abajo en el orden en el que aparecen.
  - Cada componente es una unidad independiente; no afecta a los demás en términos de flujo, salvo que tengan relaciones internas explícitas (por ejemplo, si comparten contexto o estado global).
*/

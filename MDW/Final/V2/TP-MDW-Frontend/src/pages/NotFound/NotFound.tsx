import { FC } from "react"; /* FC Es un tipo que proporciona React para definir componentes funcionales, es decir, indica que el componente es una función y que recibe propiedades y retorna JSX (el HTML en React). */
import { Route, Routes } from "react-router-dom";
import NotFoundElement from "../../components/NotFoundElement";

// Esto es una declaración de interfaz en TypeScript. Las interfaces son estructuras que definen la forma que deben tener los objetos.
// En este caso, RoutesWithNotFoundProps define las propiedades (props) que espera recibir el componente NotFound.
// En React, los COMPONENTES HIJOS SON PASADOS COMO PROPIEDADES DEL COMPONENTE PADRE (children), lo que permite que el componente padre reciba y renderice contenido dinámico o personalizado.
// Al igual que en el backend, las interfaces en TypeScript validan que las propiedades estén bien definidas durante la compilación, lo que ayuda a evitar errores y proporciona una mayor seguridad en el código.

interface RoutesWithNotFoundProps {
  children: JSX.Element[] | JSX.Element;
  /* propiedad: array de elementos JSX o único elemento JSX */
}

/* Definición del componente */
/*  componente: Tipo del componente(FC)<Tipo de propiedades recibe(Interface)> */
const NotFound: FC<RoutesWithNotFoundProps> = ({ children }) => {
  return (
    <Routes>
      {children}
      <Route path="*" element={<NotFoundElement />} />
    </Routes>
  );
};

export default NotFound;

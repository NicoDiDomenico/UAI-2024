import type { SubmitHandler } from "react-hook-form";
import FormularioPelicula from "./FormularioPelicula";
import type PeliculaCreacion from "../modelos/PeliculaCreacion.model";
import type Genero from "../../generos/modelos/Genero.model";

export default function CrearPelicula() {
  const onSubmit: SubmitHandler<PeliculaCreacion> = async (data) => {
    console.log("creando película...");
    await new Promise((resolve) => setTimeout(resolve, 500));
    console.log(data);
  };

  const generosSeleccionados: Genero[] = [];
  const generosNoSeleccionados: Genero[] = [
    { id: 1, nombre: "Acción" },
    { id: 2, nombre: "Drama" },
    { id: 3, nombre: "Comedia" },
  ];

  return (
    <>
      <h3>Crear Película</h3>
      <FormularioPelicula
        onSubmit={onSubmit}
        generosNoSeleccionados={generosNoSeleccionados}
        generosSeleccionados={generosSeleccionados}
      />
    </>
  );
}

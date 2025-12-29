import { useState, useEffect } from "react";
import type { SubmitHandler } from "react-hook-form";
import { useParams } from "react-router";
import type PeliculaCreacion from "../modelos/PeliculaCreacion.model";
import FormularioPelicula from "./FormularioPelicula";
import type Genero from "../../generos/modelos/Genero.model";

export default function EditarPelicula() {
  const [modelo, setModelo] = useState<PeliculaCreacion | undefined>(undefined);
  const { id } = useParams();

  useEffect(() => {
    setTimeout(() => {
      setModelo({
        titulo: "Avengers " + id,
        fechaLanzamiento: "2020-05-11",
        trailer: "abc",
        poster:
          "https://upload.wikimedia.org/wikipedia/en/0/0d/Avengers_Endgame_poster.jpg",
      });
    }, 500);
  }, [id]);

  const onSubmit: SubmitHandler<PeliculaCreacion> = async (data) => {
    console.log("editando pelicula...");
    await new Promise((resolve) => setTimeout(resolve, 500));
    console.log(data);
  };

  const generosSeleccionados: Genero[] = [{ id: 2, nombre: "Drama" }];

  const generosNoSeleccionados: Genero[] = [
    { id: 1, nombre: "Acción" },
    { id: 3, nombre: "Comedia" },
  ];

  return (
    <>
      <h3>Editar Película</h3>
      {modelo ? (
        <FormularioPelicula
          modelo={modelo}
          onSubmit={onSubmit}
          generosNoSeleccionados={generosNoSeleccionados}
          generosSeleccionados={generosSeleccionados}
        />
      ) : (
        <p>Cargando</p>
      )}
    </>
  );
}

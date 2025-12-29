import { useState, useEffect } from "react";
import { useParams } from "react-router";
import Cargando from "../../../componentes/cargando";
import type ActorCreacion from "../modelos/ActorCreacion.model";
import FormularioActor from "./FormularioActor";
import type { SubmitHandler } from "react-hook-form";

export default function EditarActor() {
  const { id } = useParams();

  const [modelo, setModelo] = useState<ActorCreacion | undefined>(undefined);

  useEffect(() => {
    const timerId = setTimeout(() => {
      setModelo({
        nombre: "Tom " + id,
        fechaNacimiento: "2022-11-23",
        foto: "https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg/250px-Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg",
      });
    }, 1000);

    return () => clearTimeout(timerId);
  }, [id]);

  const onSubmit: SubmitHandler<ActorCreacion> = async (data) => {
    console.log("editando actor...");
    await new Promise((resolve) => setTimeout(resolve, 2000));
    console.log(data);
  };

  return (
    <>
      <h3>Editar Actor</h3>
      {modelo ? (
        <FormularioActor modelo={modelo} onSubmit={onSubmit} />
      ) : (
        <Cargando />
      )}
    </>
  );
}

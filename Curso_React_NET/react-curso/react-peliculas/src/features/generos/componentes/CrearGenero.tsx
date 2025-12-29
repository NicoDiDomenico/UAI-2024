// CrearGenero.tsx
import { type SubmitHandler } from "react-hook-form";
import type GenerosCreacion from "../modelos/GeneroCreacion.model";
import FormularioGenero from "./FormularioGenero";

export default function CrearGenero() {
  const onSubmit: SubmitHandler<GenerosCreacion> = async (datos) => {
    console.log("Creando el Género...:");
    await new Promise((resolve) => setTimeout(resolve, 1000));
    console.log(datos);
  };

  return (
    <>
      <h3>Crear Género</h3>
      <FormularioGenero onSubmit={onSubmit} />
    </>
  );
}

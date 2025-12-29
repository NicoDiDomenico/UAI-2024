import { useForm, type SubmitHandler } from "react-hook-form";
import type GeneroCreacion from "../modelos/GeneroCreacion.model";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { NavLink } from "react-router";
import Boton from "../../../componentes/Boton";
import { primeraLetraMayuscula } from "../../../validaciones/Validaciones";

export default function FormularioGenero(props: FormularioGeneroProps) {
  const {
    register,
    handleSubmit,
    formState: { errors, isValid, isSubmitting },
  } = useForm<GeneroCreacion>({
    resolver: yupResolver(reglasDeValidacionGenero),
    mode: "onChange",
    defaultValues: props.modelo ?? {
      nombre: "",
    } /* Si no me pasan un modelo, inicializo el formulario con un nombre vacío */,
  });

  return (
    <>
      <form onSubmit={handleSubmit(props.onSubmit)}>
        <div className="form-group">
          <label htmlFor="nombre">Nombre</label>
          <input
            autoComplete="off"
            className="form-control"
            {...register("nombre")}
          />
          {errors.nombre && <p className="error"> {errors.nombre.message} </p>}
        </div>
        <div className="mt-2">
          <Boton type="submit" disabled={!isValid || isSubmitting}>
            {isSubmitting ? "Enviando..." : "Enviar"}
          </Boton>
          <NavLink to="/generos" className="btn btn-secondary ms-2">
            Cancelar
          </NavLink>
        </div>
      </form>
    </>
  );
}

interface FormularioGeneroProps {
  modelo?: GeneroCreacion;
  onSubmit: SubmitHandler<GeneroCreacion>;
}

const reglasDeValidacionGenero = yup.object({
  nombre: yup
    .string()
    .required("El nombre es obligatorio")
    .test(
      primeraLetraMayuscula()
    ) /* Puedo pasar directamente la función que devuelve el objeto de validación a traveés de la importación */
    .test({
      name: "minimo-dos-palabras",
      message: "El nombre debe contener al menos dos palabras",
      test: (valor: string | undefined) => {
        if (valor) {
          const palabras = valor.trim().split(" ");
          return palabras.length >= 2;
        }
        return true;
      },
    }) /* O puedo hacer el el objeto de validación completo directamente */,
});

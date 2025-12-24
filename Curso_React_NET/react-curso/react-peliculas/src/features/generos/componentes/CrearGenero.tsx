// CrearGenero.tsx
import { useForm, type SubmitHandler } from "react-hook-form";
import { NavLink } from "react-router";
import Boton from "../../../componentes/Boton";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

// 1) Esto describe "la forma" (estructura) de los datos del formulario.
//    Es como decir: el formulario construye un objeto { nombre: string }.
interface FormType {
  nombre: string;
}

export default function CrearGenero() {

  // 2) useForm() es el hook de React Hook Form que maneja el formulario por vos.
  //    - register: "conecta" inputs al formulario
  //    - handleSubmit: junta los valores y ejecuta tu función onSubmit(datos)
  //    <FormType> le dice a TS qué forma tendrá el objeto "datos".
  const { register, handleSubmit, formState: { errors, isValid } } = useForm<FormType>({
    resolver: yupResolver(reglasDeValidacionGenero), mode: "onChange"
  });

  // 3) onSubmit es la función que se ejecuta cuando el formulario se envía OK.
  //    "datos" ya viene armado por React Hook Form con lo que escribió el usuario.
  //    Ejemplo: { nombre: "Acción" }
  const onSubmit: SubmitHandler<FormType> = (datos) => console.log(datos);

  return (
    <>
      <h3>Crear Género</h3>

      {/* 4) onSubmit del <form> se dispara cuando un botón type="submit" se presiona.
            handleSubmit(onSubmit) hace esto:
            a) recolecta todos los campos registrados
            b) arma el objeto "datos"
            c) llama a onSubmit(datos) */}
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="form-group">
          <label htmlFor="nombre">Nombre</label>

          {/* 5) {...register('nombre')} "engancha" este input con el formulario.
                - 'nombre' debe coincidir con la propiedad de FormType (nombre)
                - No usás useState ni onChange: la librería guarda el valor internamente */}
          <input
            autoComplete="off"
            className="form-control"
            {...register("nombre")}
          />
          {/* Mostrar error de validación si existe */}
          {errors.nombre && <p className="error"> {errors.nombre.message} </p>}
        </div>

        <div className="mt-2">
          {/* 6) type="submit" es CLAVE:
                - Si es submit => dispara el onSubmit del <form>
                - No hace falta onClick acá, porque el envío lo maneja el <form> */}
          <Boton type="submit" disabled={!isValid}>Enviar</Boton>

          {/* 7) NavLink navega sin recargar la página (React Router).
                Acá se usa como "Cancelar": vuelve al listado /generos */}
          <NavLink to="/generos" className="btn btn-secondary ms-2">
            Cancelar
          </NavLink>
        </div>
      </form>
    </>
  );
}
// Explicación paso a paso:
// 1) Definimos FormType, que describe la estructura de los datos del formulario.
// 2) Usamos useForm<FormType>() para manejar el formulario, obteniendo register y handleSubmit.
// 3) Definimos onSubmit, que se ejecuta al enviar el formulario con los datos recolectados.
// 4) En el <form>, usamos onSubmit={handleSubmit(onSubmit)} para manejar el envío.
// 5) En el input, usamos {...register("nombre")} para conectar el campo al formulario.
// 6) El botón "Enviar" tiene type="submit" para activar el envío del formulario.
// 7) Usamos NavLink para navegar de vuelta al listado sin recargar la página.

const reglasDeValidacionGenero = yup.object({
  nombre: yup
    .string()
    .required("El nombre es obligatorio")
});

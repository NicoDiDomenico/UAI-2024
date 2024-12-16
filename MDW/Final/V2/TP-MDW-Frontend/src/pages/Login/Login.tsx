// Link: Se usa en el JSX, como una etiqueta HTML <a>, pero sin recargar la página. --> <Link> </Link>
// useNavigate: Es un hook que permite la navegación programática. En este caso, se usa para redirigir al usuario al Home (/) después de un inicio de sesión exitoso o si ya está autenticado.
import { Link, useNavigate } from "react-router-dom";
// Importa el hook `useForm` de la biblioteca "react-hook-form".
// Este hook es usado para manejar formularios en React de forma sencilla y eficiente,
// permitiendo registrar y validar datos de entrada sin necesidad de escribir tanto código boilerplate.
import { useForm } from "react-hook-form";

// Importa la librería `zod` y el tipo `ZodType` de la misma.
// Zod es una herramienta de validación y esquema de datos. Permite definir y validar
// estructuras de datos de forma declarativa.
// - `z` es el objeto principal que te permite crear esquemas.
// - `ZodType` es un tipo que representa un esquema creado con Zod.
import z, { ZodType } from "zod";

// Importa `zodResolver` desde "@hookform/resolvers/zod".
// Esto conecta `zod` con "react-hook-form", permitiendo que los esquemas de Zod se usen
// como reglas de validación para los formularios gestionados con react-hook-form.
import { zodResolver } from "@hookform/resolvers/zod";

import { PublicRoutes } from "../../models/routes.model";
import Navbar from "../../components/Navbar";
import LoginSchema from "../../models/schemas/login.model";
import Field from "../../components/InputField";
import Input from "../../components/InputField";
import axiosInstance from "../../utils/axiosInstance";
import { useEffect, useState } from "react";
import { AxiosError } from "axios";
import { notifySuccess } from "../../utils/toastFunctions";
import { useAuth } from "../../contexts/AuthContext";

const Login = () => {
  const [error, setError] = useState(""); // error: Estado local para manejar errores de inicio de sesión.
  const { user, login } = useAuth(); // useAuth: Obtiene el usuario actual (user) y la función login desde el contexto global de autenticación.
  const navigate = useNavigate(); // // Hook para cambiar de ruta

  // Define las reglas de validación (esquema) usando zod
  const loginValidation: ZodType<LoginSchema> = z.object({
    // z.object: Crea un esquema que valida un objeto con las claves definidas (email y password).

    email: z.string().email("Email invalido"),
    // z.string(): Valida que el valor sea una cadena.
    // .email(): Valida que la cadena tenga el formato de un correo electrónico válido.
    // Si no cumple con el formato, muestra el mensaje de error "Email invalido".

    password: z
      .string()
      // Valida que el valor sea una cadena de texto.
      .min(5, "La contraseña debe ser mayor a 5 caracteres")
      // .min(): Valida que la cadena tenga al menos 5 caracteres.
      // Si no cumple, muestra el mensaje "La contraseña debe ser mayor a 5 caracteres".
      .max(30, "La contraseña debe ser menor a 30 caracteres"),
    // .max(): Valida que la cadena no tenga más de 30 caracteres.
    // Si excede este límite, muestra el mensaje "La contraseña debe ser menor a 30 caracteres".
  });

  // Configura el formulario usando react-hook-form y conecta las reglas de Zod con zodResolver.
  const {
    register, // Conecta los campos del formulario al estado interno gestionado por react-hook-form.
    handleSubmit, // Maneja el envío del formulario y aplica la validación antes de ejecutar cualquier lógica.
    formState: {
      errors,
    } /* errors e la forma de mostrar el mensaje de las reglas de validacion del esquema segun sea email o password */,
    // Contiene los mensajes de error generados si los datos ingresados no cumplen con las reglas de validación.
  } = useForm<LoginSchema>({
    resolver: zodResolver(loginValidation),
    // zodResolver: Conecta el esquema de validación de Zod (loginValidation) con react-hook-form.
    // Valida los datos del formulario automáticamente y llena `errors` con los mensajes si hay errores.
  });

  /* 
  La función useForm es un hook proporcionado por la biblioteca react-hook-form que simplifica la gestión y validación de formularios en React. La forma general de usarlo se ve así:
  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
    reset,
    setValue,
  } = useForm({
    defaultValues: {},        // Valores iniciales para los campos del formulario.
    resolver: undefined,      // Resolver opcional para usar una biblioteca de validación (como Zod o Yup).
    mode: "onSubmit",         // Modo de validación: "onSubmit" (por defecto), "onBlur", "onChange", etc.
    reValidateMode: "onChange", // Revalidación: cuándo vuelve a validar los campos (opcional).
    shouldFocusError: true,   // Mover el foco al primer campo con error (opcional).
  });
  */

  useEffect(() => {
    if (user) navigate("/"); // Redirige al Home si el usuario ya está autenticado
  }, [user]);

  // La función submitData se ejecuta cuando el usuario hace clic en el botón "Iniciar sesión".
  const submitData = async (data: LoginSchema) => {
    try {
      setError(""); // Limpia errores previos

      // Envía una solicitud POST al backend con el correo y la contraseña ingresados por el usuario.
      const response = await axiosInstance.post("/users/login", {
        // response es un objeto que contiene información sobre la respuesta del servidor.
        email: data.email,
        password: data.password,
      });

      const { token, message, user } = response.data;

      localStorage.setItem("token", token); // Guarda el token en localStorage
      login(user); // Actualiza el contexto global con los datos del usuario autenticado
      notifySuccess(message); // Muestra un mensaje de éxito
      navigate("/"); // Redirige al Home después del login
    } catch (error) {
      if (error instanceof AxiosError) {
        console.log(error);
        setError(error.response?.data.message); // Guarda el mensaje de error del backend
      }
    }
  };

  return (
    <>
      <Navbar>
        <div className="flex items-center justify-center my-12">
          <div className="w-96 border rounded bg-white px-7 py-10">
            <form onSubmit={handleSubmit(submitData)}>
              {" "}
              {/* 2) handleSubmit(submitData) toma el control validando los datos del form. Si todo está bien: Llama a submitData. Si hay errores: Detiene el flujo y llena formState.errors */}
              <h4 className="text-2xl mb-7">Login</h4>
              <Input
                type="email"
                {...register("email")} // "email" es el nombre del campo. Será usado como clave en los datos enviados (ejemplo: { email: "valor" }).
                error={errors.email?.message} // Accede a los errores asociados al campo "email".
                placeholder="Email"
              ></Input>
              {/* 
                Cambie este input por un componente separado
                <input
                type="email"
                placeholder="Email"
                className="input-box"
                {...register("email")}
              /> */}
              <Field
                type="password"
                {...register("password")} // "password" es el nombre del campo. Será usado como clave en los datos enviados (ejemplo: { password: "valor" }).
                error={errors.password?.message} // Accede a los errores asociados al campo "password".
                placeholder="Contraseña"
              ></Field>
              {/* <input
                type="password"
                placeholder="Contraseña"
                className="input-box"
                {...register("password")}
              /> */}
              {error && <p className="error-msg pt-0">{error}</p>} {/* l estado error se muestra en la interfaz dentro de este bloque, para proporcionar mensajes de error generales (no relacionados con validaciones específicas de un campo, por ejemplo, email duplicado). */}
              <button type="submit" className="btn-primary">
                {" "}
                {/* 1) React activa el evento onSubmit del formulario cuando se hace clic en este boton */}
                Iniciar sesión
              </button>
            </form>
            <p className="text-sm text-center mt-4">
              No tenes cuenta?{" "}
              <Link
                to={`/${PublicRoutes.REGISTER}`} // Este enlace cambia la URL del navegador a "/register".
                replace // Reemplaza la entrada actual en el historial del navegador en lugar de agregar una nueva, Esto significa que, al regresar con el botón "Atrás", no volverás a la página previa al registro.
                className="text-primary" // Aplica estilos personalizados al enlace.
              >
                Registrate! {/* Texto visible en la UI. Al hacer clic, lleva al usuario a la página de registro. */}
              </Link>
            </p>
          </div>
        </div>
      </Navbar>
    </>
  );
};

export default Login;

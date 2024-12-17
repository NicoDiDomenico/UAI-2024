// Importaciones necesarias para el componente Register
import { Link, useNavigate } from "react-router-dom"; // Link para navegación declarativa y useNavigate para redirecciones programáticas.
import { useForm } from "react-hook-form"; // Hook para manejar formularios.
import z, { ZodType } from "zod"; // Biblioteca Zod para validación de datos.
import { zodResolver } from "@hookform/resolvers/zod"; // Conecta Zod con react-hook-form.

import { PublicRoutes } from "../../models/routes.model"; // Define rutas públicas como "/login" y "/register".
import RegisterSchema from "../../models/schemas/register.model"; // Tipos y esquemas de datos para el formulario.
import Navbar from "../../components/Navbar"; // Componente de barra de navegación.
import Input from "../../components/InputField"; // Componente reutilizable para campos del formulario.
import { useEffect, useState } from "react"; // Hooks de React para estado y efectos secundarios.
import { notifySuccess } from "../../utils/toastFunctions"; // Función para mostrar mensajes de éxito.
import axiosInstance from "../../utils/axiosInstance"; // Configuración personalizada de Axios para solicitudes HTTP.
import { AxiosError } from "axios"; // Manejo de errores de Axios.
import { useAuth } from "../../contexts/AuthContext"; // Contexto global para obtener el usuario actual.

const Register = () => {
  const [error, setError] = useState(""); // Estado para almacenar mensajes de error.

  const navigate = useNavigate();
  const { user } = useAuth();

  // Redirige al Home si el usuario ya está autenticado
  useEffect(() => {
    if (user) navigate("/");
  }, [user]);

  const registerValidation: ZodType<RegisterSchema> = z
    .object({
      email: z.string().email("Email invalido"), // Valida que el campo email sea una cadena en formato de correo electrónico.
      name: z
        .string()
        .min(3, "El nombre debe tener más de 3 caracteres") // Valida que el nombre tenga al menos 3 caracteres.
        .max(30, "El nombre debe tener menos de 30 caracteres"), // Valida que el nombre no supere los 30 caracteres.
      lastname: z
        .string()
        .min(3, "El apellido debe tener más de 3 caracteres") // Valida que el apellido tenga al menos 3 caracteres.
        .max(30, "El apellido debe tener menos de 30 caracteres"), // Valida que el apellido no supere los 30 caracteres.
      password: z
        .string()
        .min(5, "La contraseña debe tener más de 5 caracteres") // Valida que la contraseña tenga al menos 5 caracteres.
        .max(30, "La contraseña debe tener menos de 30 caracteres"), // Valida que la contraseña no supere los 30 caracteres.
      confirmPassword: z
        .string()
        .min(5, "La contraseña debe tener más de 5 caracteres") // Valida que la confirmación de contraseña tenga al menos 5 caracteres.
        .max(30, "La contraseña debe tener menos de 30 caracteres"), // Valida que la confirmación de contraseña no supere los 30 caracteres.
      birthdate: z.string().nonempty("La fecha de nacimiento es obligatoria"), // Valida que el campo de fecha de nacimiento no esté vacío.
    }) /* El método .refine en Zod permite agregar reglas de validación personalizadas que no se pueden expresar con los métodos estándar de Zod (como .string() o .min()). */
    .refine((data) => data.password === data.confirmPassword, {
      // Verifica que la contraseña y su confirmación coincidan.
      /* Zod no tiene un método integrado para comparar dos campos (password y confirmPassword), pero .refine permite agregar esta lógica. */
      message: "Las contraseñas no coinciden", // Mensaje de error si no coinciden.
      path: ["confirmPassword"], // Indica que el error se asocia al campo confirmPassword.
    });

  // Configuración del formulario
  const {
    register, // Registra los campos del formulario y los conecta al estado interno.
    handleSubmit, // Maneja el evento de envío del formulario.
    formState: { errors }, // Contiene los errores generados durante la validación.
  } = useForm<RegisterSchema>({
    resolver: zodResolver(registerValidation), // Conecta las reglas de Zod con react-hook-form.
  });

  // Función para manejar el envío del formulario
  const submitData = async (data: RegisterSchema) => {
    /* 
      data:
      - Es un objeto generado automáticamente por react-hook-form que contiene los valores ingresados en el formulario.
      - Ejemplo:
        {
          name: "Juan",
          lastname: "Pérez",
          email: "juan@example.com",
          password: "12345",
          confirmPassword: "12345",
          birthdate: "2000-01-01"
        }
      - Este objeto se construye a partir de los campos registrados mediante `register("nombreDelCampo")`.
    */
    try {
      setError(""); // Limpia cualquier error previo.

      // Envía los datos del formulario al backend.
      const response = await axiosInstance.post("/users/register", {
        name: data.name,
        lastname: data.lastname,
        birthdate: data.birthdate,
        email: data.email,
        password: data.password,
      });

      const { message } = response.data; // Extrae el mensaje de éxito del backend.

      notifySuccess(message); // Muestra un mensaje de éxito al usuario.
      navigate("/login"); // Redirige al usuario al login tras un registro exitoso.
    } catch (error) {
      if (error instanceof AxiosError) {
        console.log(error); // Muestra el error en la consola para depuración.
        setError(error.response?.data.message); // Guarda el mensaje de error recibido del backend.
      }
    }
  };

  return (
    <>
      <Navbar>
        {" "}
        {/* Componente de barra de navegación */}
        <div className="flex items-center justify-center my-12">
          <div className="w-96 border rounded bg-white px-7 py-10">
            <form onSubmit={handleSubmit(submitData)}>
              {" "}
              {/* Valida y envía los datos al backend */}
              {/* 
                  handleSubmit(submitData):
                  - Cuando el usuario hace clic en el botón "Registrate", handleSubmit valida los datos del formulario usando el esquema definido con Zod.
                  - Si los datos son válidos:
                    - Construye un objeto `data` automáticamente con los valores ingresados en los campos registrados mediante `register`.
                    - Llama a la función `submitData` y le pasa `data` como argumento.
                  - Si los datos no son válidos:
                    - Detiene el flujo y llena `formState.errors` con los errores generados, que luego pueden mostrarse en la interfaz.
                */}
              <h4 className="text-2xl mb-7">Registrate</h4>
              <div className="flex gap-2">
                <Input
                  type="text"
                  {...register("name")} // Registra el campo "name" y conecta su estado al formulario.
                  error={errors.name?.message} // Muestra el mensaje de error si existe.
                  placeholder="Nombre"
                />
                <Input
                  type="text"
                  {...register("lastname")} // Registra el campo "lastname" ya que lastname será la etiqueta que identifica el input, register("lastname") devuelve un objeto que tiene name="lastname" y otras funciones, con ... copiamos todo y lo mandamos como propiedad del input. IMPORTANTE: TAMBIEN MANDA --> ref: (element) => {...} // Referencia al input real
                  error={errors.lastname?.message} // Muestra el mensaje de error si existe.
                  placeholder="Apellido"
                />
              </div>
              <Input
                type="email"
                {...register("email")} // Registra el campo "email".
                error={errors.email?.message} // Muestra el mensaje de error si existe.
                placeholder="Email"
              />
              <Input
                type="date"
                {...register("birthdate")} // Registra el campo "birthdate".
                error={errors.birthdate?.message} // Muestra el mensaje de error si existe.
                placeholder="Fecha de nacimiento"
              />
              <Input
                type="password"
                {...register("password")} // Registra el campo "password".
                error={errors.password?.message} // Muestra el mensaje de error si existe.
                placeholder="Contraseña"
              />
              <Input
                type="password"
                {...register("confirmPassword")} // Registra el campo "confirmPassword".
                error={errors.confirmPassword?.message} // Muestra el mensaje de error si existe.
                placeholder="Confirmar contraseña"
              />
              {error && <p className="error-msg pt-0">{error}</p>}{" "}
              {/* Muestra errores generales del backend */}
              <button type="submit" className="btn-primary">
                {" "}
                {/* Envía el formulario */}
                Registrate
              </button>
              <p className="text-sm text-center mt-4">
                Ya tenes cuenta?{" "}
                <Link
                  to={`/${PublicRoutes.LOGIN}`} // Navega a la ruta de login.
                  replace
                  className="text-primary"
                >
                  Inicia sesión!
                </Link>
              </p>
            </form>
          </div>
        </div>
      </Navbar>
    </>
  );
};

export default Register;

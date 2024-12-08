import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import z, { ZodType } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

import { PublicRoutes } from "../../models/routes.model";
import RegisterSchema from "../../models/schemas/register.model";
import Navbar from "../../components/Navbar";
import Input from "../../components/InputField";
import { useEffect, useState } from "react";
import { notifySuccess } from "../../utils/toastFunctions";
import axiosInstance from "../../utils/axiosInstance";
import { AxiosError } from "axios";
import { useAuth } from "../../contexts/AuthContext";

const Register = () => {
  const [error, setError] = useState("");

  const navigate = useNavigate();
  const {user} = useAuth()

  useEffect(()=> {
    if(user) navigate("/")
  },[])

  const registerValidation: ZodType<RegisterSchema> = z
    .object({
      email: z.string().email("Email invalido"),
      name: z
        .string()
        .min(3, "El nombre debe tener mas de 3 caracteres")
        .max(30, "El nombre debe tener menos de 30 caracteres"),
      lastname: z
        .string()
        .min(3, "El apellido debe tener mas de 3 caracteres")
        .max(30, "El apellido debe tener menos de 30 caracteres"),
      password: z
        .string()
        .min(5, "La contraseña debe ser mayor a 5 caracteres")
        .max(30, "La contraseña debe ser menor a 30 caracteres"),
      confirmPassword: z
        .string()
        .min(5, "La contraseña debe ser mayor a 5 caracteres")
        .max(30, "La contraseña debe ser menor a 30 caracteres"),
      birthdate: z.string().nonempty("La fecha de nacimiento es obligatoria"),
    })
    .refine((data) => data.password === data.confirmPassword, {
      message: "Las contraseñas no coinciden",
      path: ["confirmPassword"],
    });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterSchema>({
    resolver: zodResolver(registerValidation),
  });

  const submitData = async (data: RegisterSchema) => {
    try {
      setError("");

      const response = await axiosInstance.post("/users/register", {
        name: data.name,
        lastname: data.lastname,
        birthdate: data.birthdate,
        email: data.email,
        password: data.password,
      });

      const { message } = response.data;

      notifySuccess(message);
      navigate("/login");
    } catch (error) {
      if (error instanceof AxiosError) {
        console.log(error);
        setError(error.response?.data.message);
      }
    }
  };
  return (
    <>
      <Navbar>
        <div className="flex items-center justify-center my-12">
          <div className="w-96 border rounded bg-white px-7 py-10">
            <form onSubmit={handleSubmit(submitData)}>
              <h4 className="text-2xl mb-7">Registrate</h4>

              <div className="flex gap-2">
                <Input
                  type="text"
                  {...register("name")}
                  error={errors.name?.message}
                  placeholder="Nombre"
                ></Input>

                <Input
                  type="text"
                  {...register("lastname")}
                  error={errors.lastname?.message}
                  placeholder="Apellido"
                ></Input>
              </div>

              <Input
                type="email"
                {...register("email")}
                error={errors.email?.message}
                placeholder="Email"
              ></Input>

              <Input
                type="date"
                {...register("birthdate")}
                error={errors.birthdate?.message}
                placeholder="Fecha de nacimiento"
              ></Input>

              <Input
                type="password"
                {...register("password")}
                error={errors.password?.message}
                placeholder="Contraseña"
              ></Input>

              <Input
                type="password"
                {...register("confirmPassword")}
                error={errors.confirmPassword?.message}
                placeholder="Confirmar contraseña"
              ></Input>

              {error && <p className="error-msg pt-0">{error}</p>}

              <button type="submit" className="btn-primary">
                Registrate
              </button>

              <p className="text-sm text-center mt-4">
                Ya tenes cuenta?{" "}
                <Link
                  to={`/${PublicRoutes.LOGIN}`}
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

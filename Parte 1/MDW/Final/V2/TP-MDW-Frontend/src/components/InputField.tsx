import { forwardRef, HTMLProps, Ref, useState } from "react";
import { FaRegEye, FaRegEyeSlash } from "react-icons/fa6";

interface FieldProps extends HTMLProps<HTMLInputElement> {
  type: "email" | "password" | "text" | "date" | "file";
  error?: string;
  accept?: string;
}

const Input = forwardRef((props: FieldProps, ref: Ref<HTMLInputElement>) => {
  // forwardRef permite "pasar" la referencia (ref) desde un componente padre hacia un elemento DOM dentro del componente hijo. Con forwardRef, el componente hijo puede aceptar una referencia y asignarla manualmente al elemento que querés.

  const { placeholder, error, type, ...FieldProps } = props;
  const [showPassword, setShowPassword] = useState(false); // Maneja el estado local para mostrar u ocultar la contraseña. Se inicializa con false porque, al inicio, la contraseña debe estar oculta.

  return (
    <>
      <div
        className={`flex items-center bg-transparent border-[1.5px] px-5 rounded mb-3 ${
          type === "file" && "cursor-pointer"
        }`}
      >
        {/* Campo del form --> Use Form lo gestiona. */}
        <input
          type={type !== "password" ? type : !showPassword ? type : "text"}
          placeholder={placeholder}
          className={`w-full text-sm bg-transparent py-3 mr-3 rounded outline-none bg-black${
            type === "file" || (type === "date" && "cursor-pointer")
          }`}
          ref={ref}
          {...FieldProps}
        />
        {/* Logica para cambiar el icono a ojo abierto o a cerrado --> Al cambiar showPassword afecta directamente al input*/}
        {type === "password" && !showPassword ? (
          <FaRegEye /* (ojo abierto)  */
            size={22}
            className="text-primary cursor-pointer"
            onClick={() => setShowPassword(!showPassword)}
          />
        ) : (
          type === "password" && (
            <FaRegEyeSlash
              size={22}
              className="text-slate-500 cursor-pointer"
              onClick={() => setShowPassword(!showPassword)}
            />
          )
        )}
      </div>
      {error && <p className="text-sm text-red-500 mb-2">{error}</p>}
    </>
  );
});

export default Input;

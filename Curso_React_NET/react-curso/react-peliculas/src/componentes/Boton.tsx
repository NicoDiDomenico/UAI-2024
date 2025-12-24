// Boton.tsx
export default function Boton(props: BotonProps) {
  return (
    <button
      /* 1) props.type ?? "button"
            - "??" significa: si props.type es null/undefined, usar "button"
            - Esto evita un bug común: si no definís type, algunos botones dentro de <form>
              podrían comportarse como submit y enviar el formulario sin querer.
            - Entonces: por defecto este componente es un botón normal ("button")
              y SOLO será submit si vos lo pedís: <Boton type="submit" /> */
      type={props.type ?? "button"}
      className="btn btn-primary"

      /* 2) onClick es opcional:
            - Para botones normales ("button") suele usarse onClick
            - Para botones submit, normalmente NO hace falta onClick,
              porque el submit lo maneja el <form onSubmit=...> */
      onClick={props.onClick}
      disabled={props.disabled ?? false}
    >
      {/* 3) children es el texto/JSX que va dentro del botón.
            Ej: <Boton>Enviar</Boton> => children = "Enviar" */}
      {props.children}
    </button>
  );
}

interface BotonProps {
  // Lo que pongas entre <Boton> ... </Boton>
  children: React.ReactNode;

  // Opcional: se usa cuando querés ejecutar algo al click (para type="button" normalmente)
  onClick?: () => void;

  // Opcional: restringimos a 3 valores válidos de HTML.
  // Si no lo pasás, se usa "button" gracias al ?? de arriba.
  type?: "button" | "submit" | "reset";
  disabled?: boolean;
}

// Explicación paso a paso:
// 1) Usamos props.type ?? "button" para definir el tipo del botón, evitando bugs en formularios.
// 2) onClick es opcional y se usa principalmente para botones normales.
// 3) children permite pasar texto o JSX dentro del botón.
// Recordar: La interfaz BotonProps define las propiedades esperadas para el componente Boton.
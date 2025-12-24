import type Persona from "./persona.model";

export default function Fila(props: FilaProps) {
  return (
    <tr>
      <td>{props.persona.nombre}</td>
      <td>{props.persona.departamento}</td>
      <td>
        <button onClick={() => props.remover(props.persona)}>
          Remover
        </button>
      </td>
    </tr>
  );
}

interface FilaProps {
  persona: Persona;
  remover: (p: Persona) => void;
}

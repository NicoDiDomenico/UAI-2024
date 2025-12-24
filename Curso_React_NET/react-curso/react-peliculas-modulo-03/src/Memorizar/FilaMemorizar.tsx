import { memo } from "react";
import type Persona from "../persona.model";

 const FilaMemorizar = memo(function FilaMemorizar(props: FilaProps) {
  console.log(`Se renderiza la fila de ${props.persona.nombre}`);

  if (props.persona.nombre === 'Roberto') {
    throw new Error("Error intencional para Roberto");
  }

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
});

interface FilaProps {
  persona: Persona;
  remover: (p: Persona) => void;
}

export default FilaMemorizar;


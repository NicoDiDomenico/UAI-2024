import { useState } from "react";
import TablaMemorizar from "./TablaMemorizar";

export default function EjemploMemoizarTabla() {
  const [texto, setTexto] = useState("");

  return (
    <>
      <input
        type="text"
        onChange={e => setTexto(e.target.value)}
      />

      <p>
        El texto es: {texto}
      </p>

      <TablaMemorizar />
    </>
  );
}

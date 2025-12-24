import { useContext } from "react";
import ValorContext from "./valorContext";

export default function Hijo() {
    const valor = useContext(ValorContext);
    
    return (
        <>
            <h3>Este es el componente hijo</h3>;
            <p>Valor del contexto: {valor}</p>
        </>
    );
}


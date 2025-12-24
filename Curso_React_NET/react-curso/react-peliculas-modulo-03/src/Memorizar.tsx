import React, { useMemo } from "react";

export default function EjemploMemorizar() {
    const [numero, setNumero] = React.useState(1);
    const [nombre, setNombre] = React.useState("");

    const factorial = useMemo(() => {
        console.log("Calculando factorial");
        let resultado = 1;
        for (let i = 1; i <= numero; i++) {
            resultado *= i;
        }
        return resultado;
    }, [numero]); /* El cálculo del factorial solo se realiza cuando cambia numero */

    return (
        <>
            <p>Calcular el factorial de <input type="number" onChange={e => setNumero(Number(e.target.value))} /></p>
            
            <p>Factorial de {numero}: {factorial}</p> 

            <p>Nombre: <input type="text" onChange={e => setNombre(e.target.value)} /></p>
            <p>Hola, {nombre}</p>
        </>
    );
}
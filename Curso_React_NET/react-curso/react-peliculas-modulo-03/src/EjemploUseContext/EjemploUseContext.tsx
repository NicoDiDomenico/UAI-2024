import Abuelo from "./Abuelo";
import ValorContext from "./valorContext";

export default function EjemploUseContext() {
    const texto = "Valor desde el proveedor de contexto";
    return (
    <ValorContext.Provider value={texto}> {/* Proporciona el valor "texto" a todos los componentes dentro de este proveedor */}
        <Abuelo/>;
    </ValorContext.Provider>
    );
}


export default function MostrarTexto(props: MostrarTextoProps) {
    return (
        <div>
            <p>Haz escrito: {props.texto}</p>
        </div>
    );
}

interface MostrarTextoProps {
    texto: string;
}

/* 
--- Esta es laforma de trabajar con interfaces en React + Typescript ---
Typescript hace esto con las interface automáticamente:
props = {
  texto: "algo que viene del componente padre"
}
Por eso despues usas:
props.texto
*/
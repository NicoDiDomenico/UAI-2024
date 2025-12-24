import Boton from "../../../componentes/Boton";
import { useNavigate } from "react-router";

export default function IndiceGeneros() {
    const navigate = useNavigate();
    return (
        <>
            <h3>Géneros</h3>
            <Boton onClick={() => navigate("/generos/crear")}>Crear Géneros</Boton>
        </>
    )
}
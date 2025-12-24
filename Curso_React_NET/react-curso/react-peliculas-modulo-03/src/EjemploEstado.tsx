import { useState } from "react";
import Cabecera from "./cabecera";
import MostrarTexto from "./mostrarTexto";

export default function App() {

  const [texto, setTexto] = useState('');
  let texto2 = '';
  console.log(texto2); /* Notar como texto2 no se mantiene  */

  const manejarClick = () => alert('click');

  const manejarKeyUp = (e: React.KeyboardEvent<HTMLInputElement>) =>{
    texto2 = e.currentTarget.value;
    setTexto(e.currentTarget.value);
  }
    

  return (
    <>
      <Cabecera/>

      <button onClick={manejarClick}>Clickeame</button>

      <div>
        <input onKeyUp={(e) => manejarKeyUp(e)} />
      </div>

      <MostrarTexto texto={texto} />
    </>
  );
}

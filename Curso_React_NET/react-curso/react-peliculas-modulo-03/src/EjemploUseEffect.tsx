import { useEffect, useState } from "react";

export default function EjemploUseEffect() {

  const [clicks, setClicks] = useState(0);
  const [hora, setHora] = useState(new Date());

  /* useEffect con [] indica que el efecto se jecuta una sola vez al montar el componente */
  useEffect(() => {
    console.log('el componente ha cargado');

    return () => console.log('desmontando el componente');
  }, []);

  /* useEffect con [clicks] indica que se ejecuta cada vez que cambia 'clicks' */
  useEffect(() => { 
    console.log(`hook del clic`);
    document.title = `${clicks} veces`;
  }, [clicks]); 
  
  /* useEffect sin dependencias indica que el efecto se ejecuta en cada renderizado del componente */
  useEffect(() => {
    const timeId = setInterval(() => {
        setHora(new Date());
    }, 1000);

    return () => clearInterval(timeId); // Limpieza del intervalo al desmontar
  },); 

  return (
    <>
      <h2>Ejemplo UseEffect</h2>

      <div>
        <button onClick={() => setClicks(clicks + 1)}>
          Me has clickeado {clicks} veces
        </button>
      </div>
      <div>
        La hora actual es {hora.toTimeString()}
      </div>
    </>
  );
}

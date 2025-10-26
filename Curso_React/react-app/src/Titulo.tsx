function Titulo() {
  const nombre = "Aprobame Pablo";
  if (nombre) {
    return <p>Hola {nombre}</p>;
  } else return <p>Hola Mundo</p>;
}

export default Titulo;

const express = require('express');
const app = express();
const PORT = 3001;

// Permitir solicitudes desde el frontend
app.use((req, res, next) => {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});

// Ruta para obtener la lista de socios
app.get('/api/socios', (req, res) => {
  const socios = [
    {
      nombre: "Juan Pérez",
      estado: "Nuevo",
      fechaVencimiento: "2023-12-31"
    }
  ];
  res.json(socios);
});

app.listen(PORT, () => {
  console.log(`Servidor ejecutándose en http://localhost:${PORT}`);
});


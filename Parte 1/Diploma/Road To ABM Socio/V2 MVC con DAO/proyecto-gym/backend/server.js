const express = require('express');
const cors = require('cors');
const SocioRoutes = require('./routes/SocioRoutes');

const app = express();
const PORT = 3001;

app.use(cors());
app.use(express.json());

// Configurar rutas
app.use('/api/socios', SocioRoutes);

// Iniciar servidor
app.listen(PORT, () => {
  console.log(`Servidor ejecutándose en http://localhost:${PORT}`);
});





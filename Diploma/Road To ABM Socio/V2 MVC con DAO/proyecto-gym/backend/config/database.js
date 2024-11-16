const mysql = require('mysql2/promise');

// Configuración de la conexión a la base de datos
const pool = mysql.createPool({
  host: 'localhost',
  user: 'gimnasio_user',
  password: '8845',
  database: 'gimnasio',
});

module.exports = pool;

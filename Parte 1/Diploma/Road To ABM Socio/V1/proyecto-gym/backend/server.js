const express = require('express');
const mysql = require('mysql2/promise'); // Usamos el cliente con soporte de promesas
const app = express();
const PORT = 3001;

// Configurar la conexión a la base de datos
const pool = mysql.createPool({
  host: 'localhost',
  user: 'gimnasio_user',
  password: '8845',
  database: 'gimnasio',
});

const cors = require('cors');
app.use(cors());
app.use(express.json()); // Asegúrate de incluir esto para recibir JSON

// Ruta para obtener la lista de socios
app.get('/api/socios', async (req, res) => {
  try {
    const [rows] = await pool.query('SELECT * FROM socios');
    res.json(rows); // Enviar los datos obtenidos como JSON al frontend
  } catch (error) {
    console.error('Error al obtener los socios:', error);
    res.status(500).send('Error al obtener los socios');
  }
});

// Ruta para eliminar un socio por ID
app.delete('/api/socios/:id', async (req, res) => {
  const { id } = req.params;

  try {
    const [result] = await pool.query('DELETE FROM socios WHERE id = ?', [id]);
    if (result.affectedRows > 0) {
      res.status(200).send({ message: 'Socio eliminado correctamente' });
    } else {
      res.status(404).send({ message: 'Socio no encontrado' });
    }
  } catch (error) {
    console.error("Error al eliminar el socio:", error);
    res.status(500).send({ message: 'Error al eliminar el socio' });
  }
});

// Ruta para agregar un nuevo socio
app.post('/api/socios', async (req, res) => {
  const { nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial, diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan, estado = "Nuevo" } = req.body;

  // Establecer fecha de inicio de actividades (fecha de alta)
  const fechaInicioActividades = new Date();

  // Calcular fecha de fin de actividades según el plan
  let fechaFinActividades = new Date(fechaInicioActividades);
  if (plan === 'Mensual') {
    fechaFinActividades.setDate(fechaFinActividades.getDate() + 30);
  } else if (plan === 'Anual') {
    fechaFinActividades.setDate(fechaFinActividades.getDate() + 365);
  }

  try {
    const [result] = await pool.query(
      `INSERT INTO socios (nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial, diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan, estado, fechaInicioActividades, fechaFinActividades)
      VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`,
      [nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial, diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan, estado, fechaInicioActividades.toISOString().split('T')[0], fechaFinActividades.toISOString().split('T')[0]]
    );

    res.status(201).send({ message: 'Socio agregado correctamente', id: result.insertId });
  } catch (error) {
    console.error("Error al agregar el socio:", error);
    res.status(500).send({ message: 'Error al agregar el socio' });
  }
});

// Ruta para obtener un socio por ID
app.get('/api/socios/:id', async (req, res) => {
  const { id } = req.params;

  try {
    const [rows] = await pool.query('SELECT * FROM socios WHERE id = ?', [id]);
    if (rows.length > 0) {
      res.json(rows[0]); // Enviar el primer elemento del resultado como JSON
    } else {
      res.status(404).send({ message: 'Socio no encontrado' });
    }
  } catch (error) {
    console.error('Error al obtener el socio:', error);
    res.status(500).send({ message: 'Error al obtener el socio' });
  }
});

// Ruta para actualizar un socio por ID
app.put('/api/socios/:id', async (req, res) => {
  const { id } = req.params;
  const { nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial, diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan } = req.body;

  console.log("Datos recibidos para actualización:", req.body); // Agrega este log para ver los datos recibidos

  try {
    const [result] = await pool.query(
      `UPDATE socios SET nombre = ?, fechaNacimiento = ?, genero = ?, dni = ?, direccion = ?, telefono = ?, email = ?, obraSocial = ?, diasAsistencia = ?, nombreUsuario = ?, contrasena = ?, preguntaSeguridad = ?, respuestaSeguridad = ?, plan = ? WHERE id = ?`,
      [nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial, diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan, id]
    );

    if (result.affectedRows > 0) {
      res.status(200).send({ message: 'Socio actualizado correctamente' });
    } else {
      res.status(404).send({ message: 'Socio no encontrado' });
    }
  } catch (error) {
    console.error("Error al actualizar el socio:", error);
    res.status(500).send({ message: 'Error al actualizar el socio' });
  }
});

// Iniciar el servidor
app.listen(PORT, () => {
  console.log(`Servidor ejecutándose en http://localhost:${PORT}`);
});




const pool = require('../config/database');

class SocioDAO {
  static async obtenerTodos() {
    const [rows] = await pool.query('SELECT * FROM socios');
    return rows;
  }

  static async obtenerPorId(id) {
    const [rows] = await pool.query('SELECT * FROM socios WHERE id = ?', [id]);
    return rows[0];
  }

  static async agregar(socio) {
    const {
      nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial,
      diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad,
      plan, estado, fechaInicioActividades, fechaFinActividades
    } = socio;

    const [result] = await pool.query(
      `INSERT INTO socios (nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial,
      diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan, estado,
      fechaInicioActividades, fechaFinActividades)
      VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`,
      [nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial,
        diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad,
        plan, estado, fechaInicioActividades, fechaFinActividades]
    );
    return result.insertId;
  }

  static async actualizar(id, socio) {
    const {
      nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial,
      diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad,
      plan, fechaInicioActividades, fechaFinActividades
    } = socio;

    const [result] = await pool.query(
      `UPDATE socios SET nombre = ?, fechaNacimiento = ?, genero = ?, dni = ?, direccion = ?, telefono = ?, 
      email = ?, obraSocial = ?, diasAsistencia = ?, nombreUsuario = ?, contrasena = ?, preguntaSeguridad = ?, 
      respuestaSeguridad = ?, plan = ?, fechaInicioActividades = ?, fechaFinActividades = ? WHERE id = ?`,
      [nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial,
        diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad,
        plan, fechaInicioActividades, fechaFinActividades, id]
    );
    return result.affectedRows;
  }

  static async eliminar(id) {
    const [result] = await pool.query('DELETE FROM socios WHERE id = ?', [id]);
    return result.affectedRows;
  }
}

module.exports = SocioDAO;


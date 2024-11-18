const pool = require('../config/database');

class SocioDAO {
  // Obtener todos los socios
  static async obtenerTodos() {
    const [rows] = await pool.query(`
      SELECT s.*, u.nombreUsuario 
      FROM Socio s 
      INNER JOIN Usuario u ON s.idUsuario = u.idUsuario
    `);
    return rows;
  }

  // Obtener un socio por ID
  static async obtenerPorId(id) {
    const [rows] = await pool.query(`
        SELECT 
            s.idUsuario, 
            s.idGimnasio, 
            s.nombreApellido AS nombreApellido, 
            s.fechaNacimiento, 
            s.genero, 
            s.nroDocumento, 
            s.ciudad, 
            s.direccion, 
            s.telefono, 
            s.email, 
            s.obraSocial, 
            u.nombreUsuario, 
            u.contrasena, 
            s.pregunta AS preguntaSeguridad, 
            s.respuesta AS respuestaSeguridad, 
            s.plan, 
            s.estadoSocio AS estado, 
            s.fechaInicioActividades, 
            s.fechaFinActividades, 
            s.fechaNotificacion, 
            s.respuestaNotificacion
        FROM Socio s
        INNER JOIN Usuario u ON s.idUsuario = u.idUsuario
        WHERE s.idUsuario = ?
    `, [id]);
    return rows[0];
}

  // Agregar un nuevo socio
  static async agregar(socio) {
    const {
        idUsuario, idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento,
        ciudad, direccion, telefono, email, obraSocial, nombreUsuario, contrasena,
        pregunta, respuesta, plan, estadoSocio = "Nuevo", fechaInicioActividades, fechaFinActividades,
        fechaNotificacion, respuestaNotificacion
    } = socio;

    // Crear el usuario primero en la tabla Usuario
    const [usuarioResult] = await pool.query(
        `INSERT INTO Usuario (nombreUsuario, contrasena) VALUES (?, ?)`,
        [nombreUsuario, contrasena]
    );

    const nuevoIdUsuario = usuarioResult.insertId;

    // Luego insertar el socio vinculado al usuario
    await pool.query(
        `INSERT INTO Socio (
            idUsuario, idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, 
            ciudad, direccion, telefono, email, obraSocial, pregunta, respuesta, plan, 
            estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion, 
            respuestaNotificacion
        ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`,
        [
            nuevoIdUsuario, idGimnasio || null, nombreApellido || "No especificado", fechaNacimiento || null,
            genero || "No especificado", nroDocumento || "No especificado", ciudad || "No especificado",
            direccion || "No especificado", telefono || "No especificado", email || "No especificado",
            obraSocial || "No especificado", pregunta || "No especificado", respuesta || "No especificado",
            plan || "No especificado", estadoSocio || "No especificado", fechaInicioActividades || null,
            fechaFinActividades || null, fechaNotificacion || null, respuestaNotificacion || null
        ]
    );

    return nuevoIdUsuario;
}

  // Actualizar un socio
  static async actualizar(id, socio) {
    const {
      idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento,
      ciudad, direccion, telefono, email, obraSocial, pregunta, respuesta, plan,
      estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion, respuestaNotificacion
    } = socio;

    const [result] = await pool.query(
      `UPDATE Socio SET 
        idGimnasio = ?, nombreApellido = ?, fechaNacimiento = ?, genero = ?, nroDocumento = ?, 
        ciudad = ?, direccion = ?, telefono = ?, email = ?, obraSocial = ?, pregunta = ?, 
        respuesta = ?, plan = ?, estadoSocio = ?, fechaInicioActividades = ?, fechaFinActividades = ?, 
        fechaNotificacion = ?, respuestaNotificacion = ? 
      WHERE idUsuario = ?`,
      [
        idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento,
        ciudad, direccion, telefono, email, obraSocial, pregunta, respuesta, plan,
        estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion, respuestaNotificacion,
        id
      ]
    );
    return result.affectedRows;
  }

  // Eliminar un socio
  static async eliminar(id) {
    const [result] = await pool.query(
      `DELETE FROM Socio WHERE idUsuario = ?`, [id]
    );
    return result.affectedRows;
  }
}

module.exports = SocioDAO;


const pool = require('../config/database');

class SocioDAO {
    static async obtenerTodos() {
        const [rows] = await pool.query('SELECT * FROM Socio');
        return rows;
    }

    static async obtenerPorId(idUsuario) {
        const [rows] = await pool.query('SELECT * FROM Socio WHERE idUsuario = ?', [idUsuario]);
        return rows[0];
    }

    static async agregar(socio) {
        const {
            idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion,
            telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta,
            plan, estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion
        } = socio;

        const [result] = await pool.query(
          `INSERT INTO socios (idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion,
          telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta,
          plan, estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion)
          VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`,
            [idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion,
                telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta,
                plan, estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion]
        );
        return result.insertId;
    }

    static async actualizar(idUsuario, socio) {
        const {
            idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion,
            telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta,
            plan, estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion
        } = socio;

        const [result] = await pool.query(
            `UPDATE Socio SET
                idGimnasio = ?, nombreApellido = ?, fechaNacimiento = ?, genero = ?, nroDocumento = ?, 
                ciudad = ?, direccion = ?, telefono = ?, email = ?, obraSocial = ?, nombreUsuario = ?, 
                contrasena = ?, pregunta = ?, respuesta = ?, plan = ?, estadoSocio = ?, 
                fechaInicioActividades = ?, fechaFinActividades = ?, fechaNotificacion = ?
            WHERE idUsuario = ?`,
            [idGimnasio, nombreApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion,
                telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta,
                plan, estadoSocio, fechaInicioActividades, fechaFinActividades, fechaNotificacion, idUsuario]
        );
        return result.affectedRows;
    }

    static async eliminar(idUsuario) {
        const [result] = await pool.query('DELETE FROM Socio WHERE idUsuario = ?', [idUsuario]);
        return result.affectedRows;
    }
}
module.exports = SocioDAO;



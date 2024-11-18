const SocioDAO = require('../models/SocioDAO');

class SocioController {
  static async obtenerTodos(req, res) {
    try {
      const socios = await SocioDAO.obtenerTodos();
      res.json(socios);
    } catch (error) {
      console.error('Error al obtener los socios:', error);
      res.status(500).send({ message: 'Error al obtener los socios' });
    }
  }

  static async obtenerPorId(req, res) {
    try {
      const { id } = req.params;
      const socio = await SocioDAO.obtenerPorId(id);
      console.log('Datos obtenidos del socio:', socio); // Verificar datos en la consola
      if (socio) {
        res.json(socio);
      } else {
        res.status(404).send({ message: 'Socio no encontrado' });
      }
    } catch (error) {
      res.status(500).send({ message: 'Error al obtener el socio' });
    }
  }

  static async agregar(req, res) {
    try {
        const socio = req.body;

      // Calcular fechas en el modelo antes de enviar al DAO
      socio.calcularFechas();

        const nuevoId = await SocioDAO.agregar(socio);

        // Obtener los datos completos del socio recién agregado
        const socioCreado = await SocioDAO.obtenerPorId(nuevoId);

        res.status(201).send({ message: 'Socio agregado correctamente', socio: socioCreado });
    } catch (error) {
        res.status(500).send({ message: 'Error al agregar el socio' });
    }
}

  static async actualizar(req, res) {
    try {
      const { id } = req.params;
      const socio = req.body;
      // Calcular fechas en el modelo antes de enviar al DAO
      socio.calcularFechas();
      const updated = await SocioDAO.actualizar(id, socio);
      if (updated) {
        res.status(200).send({ message: 'Socio actualizado correctamente' });
      } else {
        res.status(404).send({ message: 'Socio no encontrado' });
      }
    } catch (error) {
      console.error('Error al actualizar el socio:', error);
      res.status(500).send({ message: 'Error al actualizar el socio' });
    }
  }

  static async eliminar(req, res) {
    try {
      const { id } = req.params;
      const deleted = await SocioDAO.eliminar(id);
      if (deleted) {
        res.status(200).send({ message: 'Socio eliminado correctamente' });
      } else {
        res.status(404).send({ message: 'Socio no encontrado' });
      }
    } catch (error) {
      console.error('Error al eliminar el socio:', error);
      res.status(500).send({ message: 'Error al eliminar el socio' });
    }
  }
}

module.exports = SocioController;



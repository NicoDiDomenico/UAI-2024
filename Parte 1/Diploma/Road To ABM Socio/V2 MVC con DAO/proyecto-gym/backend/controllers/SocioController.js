const Socio = require('../models/Socio');
const SocioDAO = require('../models/SocioDAO');

class SocioController {
  static async obtenerTodos(req, res) {
    try {
      const socios = await SocioDAO.obtenerTodos();
      res.json(socios);
    } catch (error) {
      console.error('Error al obtener los socios:', error);
      res.status(500).send('Error al obtener los socios');
    }
  }

  static async obtenerPorId(req, res) {
    try {
      const { id } = req.params;
      const socio = await SocioDAO.obtenerPorId(id);
      if (socio) {
        res.json(socio);
      } else {
        res.status(404).send({ message: 'Socio no encontrado' });
      }
    } catch (error) {
      console.error('Error al obtener el socio:', error);
      res.status(500).send('Error al obtener el socio');
    }
  }

  static async agregar(req, res) {
    try {
      const datosSocio = req.body;
      const socio = new Socio(datosSocio);

      // Calcular fechas en el modelo antes de enviar al DAO
      socio.calcularFechas();

      // Invocar al DAO para guardar el socio
      const nuevoId = await SocioDAO.agregar(socio);
      res.status(201).send({ message: 'Socio agregado correctamente', id: nuevoId });
    } catch (error) {
      console.error('Error al agregar el socio:', error);
      res.status(500).send('Error al agregar el socio');
    }
  }

  static async actualizar(req, res) {
    try {
      const { id } = req.params;
      const datosSocio = req.body;

      // Obtener datos del socio original
      const socioActual = await SocioDAO.obtenerPorId(id);
      if (!socioActual) {
        return res.status(404).send({ message: 'Socio no encontrado' });
      }

      // Crear un objeto Socio con los datos actualizados
      const socio = new Socio({ ...socioActual, ...datosSocio });

      // Calcular las fechas si el plan cambió
      if (datosSocio.plan) {
        socio.calcularFechas();
      }

      const updated = await SocioDAO.actualizar(id, socio);
      if (updated) {
        res.status(200).send({ message: 'Socio actualizado correctamente' });
      } else {
        res.status(404).send({ message: 'No se pudieron aplicar los cambios' });
      }
    } catch (error) {
      console.error('Error al actualizar el socio:', error);
      res.status(500).send('Error al actualizar el socio');
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
      res.status(500).send('Error al eliminar el socio');
    }
  }
}

module.exports = SocioController;


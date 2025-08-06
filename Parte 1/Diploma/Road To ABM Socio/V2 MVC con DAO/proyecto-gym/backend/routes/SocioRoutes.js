const express = require('express');
const SocioController = require('../controllers/SocioController');

const router = express.Router();

router.get('/', SocioController.obtenerTodos);
router.get('/:id', SocioController.obtenerPorId);
router.post('/', SocioController.agregar);
router.put('/:id', SocioController.actualizar);
router.delete('/:id', SocioController.eliminar);

module.exports = router;

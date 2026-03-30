using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Ejercicios;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Controllers
{
    /// Módulo de Gestión del Gimnasio 
    [Route("api/[controller]")]
    [ApiController]
    public class EjercicioController : ControllerBase
    {
        private readonly IEjercicioService _ejercicioService;
        private readonly IValidator<EjercicioInsertDto> _insertValidator;
        private readonly IValidator<EjercicioUpdateDto> _updateValidator;

        public EjercicioController(
            IEjercicioService ejercicioService,
            IValidator<EjercicioInsertDto> insertValidator,
            IValidator<EjercicioUpdateDto> updateValidator)
        {
            _ejercicioService = ejercicioService;
            _insertValidator = insertValidator;
            _updateValidator = updateValidator;
        }

        // Testeado --> Anda bien
        /// Modulo de Gestion de Rutinas y Gestión del Gimnasio 
        /* Front: Para el grid/Mapa de ejercicios ya sea en la seccion de "Gestionar Ejercicios" o en "Gestionar Rutinas", con opción de filtrar por grupo muscular.
        - El Entrenador consulta el listado de ejercicios para editar o eliminar uno, o para decidir crear uno nuevo.   
        - El Entrenador selecciona del grid o Mapa Anatomica el Ejercicio para que el IdEjercicio sea asignado a Calentamiento/Entrenamiento/Estiramiento  */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EjercicioDto>>> Get([FromQuery] int? idGrupoMuscular)
        {
            var ejercicios = await _ejercicioService.GetEjerciciosAsync(idGrupoMuscular);
            return Ok(ejercicios);
        }

        // Testeado --> Anda bien
        // Front: No creo que lo use porque el detalle ya se trae en el grid o Mapa anatomico.
        [HttpGet("{id}")]
        public async Task<ActionResult<EjercicioDto>> GetById(int id)
        {
            var ejercicio = await _ejercicioService.GetEjercicioByIdAsync(id);
            if (ejercicio == null)
                return NotFound(_ejercicioService.Errors);

            return Ok(ejercicio);
        }

        // Testeado --> Anda bien
        // Front: Para el formulario de creación de ejercicio.
        [HttpPost]
        public async Task<IActionResult> Add(EjercicioInsertDto dto)
        {
            var validationResult = await _insertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            if (!await _ejercicioService.ValidateAsync(dto))
                return BadRequest(_ejercicioService.Errors);

            var result = await _ejercicioService.CreateEjercicioAsync(dto);
            return Ok(result);
        }

        // Testeado --> Anda bien
        // Front: Para el formulario de edición de ejercicio.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EjercicioUpdateDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            if (!await _ejercicioService.ValidateAsync(dto))
                return BadRequest(_ejercicioService.Errors);

            var result = await _ejercicioService.UpdateEjercicioAsync(id, dto);
            if (result == null)
                return NotFound(_ejercicioService.Errors);

            return Ok(result);
        }

        // Testeado --> Anda bien
        // Front: Para eliminar un ejercicio del sistema desde el grid.
        [HttpDelete("{id}")]
        public async Task<ActionResult<EjercicioDto>> Delete(int id)
        {
            var ejercicioEliminado = await _ejercicioService.DeleteEjercicioAsync(id);
            if (ejercicioEliminado == null)
                return NotFound(_ejercicioService.Errors);

            return Ok(ejercicioEliminado);
        }
    }
}
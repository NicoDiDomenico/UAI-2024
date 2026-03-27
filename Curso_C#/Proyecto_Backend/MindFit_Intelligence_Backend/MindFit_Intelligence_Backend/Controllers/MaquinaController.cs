using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Maquinas;
using MindFit_Intelligence_Backend.Services.Interfaces;

namespace MindFit_Intelligence_Backend.Controllers
{
    // Módulo de Gestión del Gimnasio
    [Route("api/[controller]")]
    [ApiController]
    public class MaquinaController : ControllerBase
    {
        private readonly IMaquinaService _maquinaService;

        public MaquinaController(IMaquinaService maquinaService)
        {
            _maquinaService = maquinaService;
        }

        // Front: Para el grid de máquinas.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaquinaDto>>> Get()
        {
            var maquinas = await _maquinaService.GetMaquinasAsync();
            return Ok(maquinas);
        }

        // No creo que lo use porque el detalle ya se tare cuando armo el grid.
        [HttpGet("{id}")]
        public async Task<ActionResult<MaquinaDto>> GetById(int id)
        {
            var maquina = await _maquinaService.GetMaquinaByIdAsync(id);
            if (maquina == null)
                return NotFound();

            return Ok(maquina);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MaquinaInsertDto dto)
        {
            if (!_maquinaService.Validate(dto))
                return BadRequest(_maquinaService.Errors);

            var result = await _maquinaService.CreateMaquinaAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MaquinaUpdateDto dto)
        {
            if (!_maquinaService.Validate(dto))
                return BadRequest(_maquinaService.Errors);

            var result = await _maquinaService.UpdateMaquinaAsync(id, dto);
            if (result == null)
                return NotFound(_maquinaService.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _maquinaService.DeleteMaquinaAsync(id);
            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
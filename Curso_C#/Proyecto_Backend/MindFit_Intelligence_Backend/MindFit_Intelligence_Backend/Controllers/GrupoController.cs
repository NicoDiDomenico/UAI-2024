using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Grupos;
using MindFit_Intelligence_Backend.Services;

namespace MindFit_Intelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;
        public GrupoController(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        // Front: Mostrar todos los grupos en una grilla para seleccionar uno y hacer modificacion o eliminación
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GrupoDto>>> Get()
        {
            var gruposDtos = await _grupoService.Get();

            return gruposDtos.Any()! ? NotFound("No hay grupos cargados") : Ok(gruposDtos);
        }

        // Front: ?
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GrupoDto>> GetById(int id)
        {
            var result = await _grupoService.GetById(id);
            return (result == null) ? NotFound() : Ok(result);
        }


        // Front: Formulario para crear un nuevo grupo, con un select multiple para elegir los permisos que va a tener el grupo
        [HttpPost]
        [Authorize(Policy = "CrearGrupo")]
        public async Task<ActionResult<GrupoDto>> Add(GrupoInsertDto grupoInsertDto)
        {
            var result = await _grupoService.Add(grupoInsertDto);


            return (result == null) 
                ? NotFound() 
                : CreatedAtAction(
                    nameof(GetById), 
                    new { id = result.IdGrupo }, 
                    result);
        }

        // Front: Formulario para modificar un grupo existente, con un select multiple para elegir los permisos que va a tener el grupo
        [HttpPut]
        [Authorize(Policy = "EditarGrupo")]
        public async Task<ActionResult<GrupoDto>> Update(int id, GrupoUpdateDto grupoUpdateDto)
        {
            var result = await _grupoService.Update(id, grupoUpdateDto);
            
            return (result == null) 
                ? NotFound() 
                : Ok(result);
        }

        // Front: Botón para eliminar un grupo existente, con una confirmación antes de eliminar
        [HttpDelete("{id}")]
        [Authorize(Policy = "EliminarGrupo")]
        public async Task<ActionResult<GrupoDto>> Delete(int id)
        {
            var result = await _grupoService.Delete(id);
            return (result == null) 
                ? NotFound() 
                : Ok(result);
        }
    }
}

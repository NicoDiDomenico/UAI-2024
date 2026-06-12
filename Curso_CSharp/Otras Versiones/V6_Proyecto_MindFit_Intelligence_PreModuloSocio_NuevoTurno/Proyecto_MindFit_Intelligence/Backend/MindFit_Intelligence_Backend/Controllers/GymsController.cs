using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindFit_Intelligence_Backend.DTOs.Gyms;
using MindFit_Intelligence_Backend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindFit_Intelligence_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymsController : ControllerBase
    {
        private readonly IGymPublicoService _gymPublicoService;

        public GymsController(IGymPublicoService gymPublicoService)
        {
            _gymPublicoService = gymPublicoService;
        }

        // Front: Para dropdown de NombreGym para que se asigne el IdGym al header del endpoint POST api/Auth/login.
        [HttpGet("activos")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GymPublicoDTO>>> GetAllActivos()
        {
            var gyms = await _gymPublicoService.GetAllGymsActivosAsync();
            return Ok(gyms);
        }

        [AllowAnonymous]
        [HttpPost("onboarding")]
        public async Task<IActionResult> RegistrarNuevoGym([FromBody] NuevoGymRequestDto dto)
        {
            var idGym = await _gymPublicoService.RegistrarNuevoGymAsync(dto);

            return Ok(new
            {
                mensaje = "Gimnasio registrado con éxito.",
                idGym = idGym
            });
        }
    }
}
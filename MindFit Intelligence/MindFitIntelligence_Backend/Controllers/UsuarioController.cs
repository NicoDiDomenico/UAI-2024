using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Services;
using MindFitIntelligence_Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace MindFitIntelligence_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration; // JWT
        private ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto, LoginUsuarioDto>         _usuarioService;
        public static Usuario _unUsuario = new();

        public UsuarioController(
            IConfiguration configuration,
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, InsertUsuarioDto, UpdateUsuarioDto, LoginUsuarioDto> usuarioService)
        {
            _configuration = configuration; // JWT
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            if (_usuarioService.Errors.Count > 0)
                return BadRequest(_usuarioService.Errors);

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto?>> GetById(int id)
        {
            var usuarioDto = await _usuarioService.GetById(id);

            if(_usuarioService.IsNull(usuarioDto))
                return NotFound(_usuarioService.Errors);

            return Ok(usuarioDto);
        }
        
        // Apliqué JWT
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Register(InsertUsuarioDto insertUsuarioDto)
        {
            var usuarioDto = await _usuarioService.Register(insertUsuarioDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = usuarioDto.IdUsuario },
                usuarioDto
            );
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUsuarioDto loginUsuarioDto)
        {
            var token = await _usuarioService.Login(loginUsuarioDto);

            if (token == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(token);
        }

        #region JWT
        // Acá no aplico buenas prácticas
        [HttpPost("register")]
        public ActionResult<Usuario> RegisterProfe(InsertUsuarioDto insertUsuarioDto)
        {
            var hasheredPassword = new PasswordHasher<Usuario>()
                .HashPassword(_unUsuario, insertUsuarioDto.Password);


            _unUsuario.Username = insertUsuarioDto.Username;
            _unUsuario.PasswordHash = hasheredPassword; // En la realdiad no se devuelve la contraseña, esto lo hace solo para ver.

            return Ok(_unUsuario);
        }

        [HttpPost("loginProfe")]
        public ActionResult<string> LoginProfe(UsuarioDto usuarioDto)
        {
            if (_unUsuario.Username != usuarioDto.Username)
                return Unauthorized("El usuario no existe");
            if (new PasswordHasher<Usuario>().VerifyHashedPassword(_unUsuario, _unUsuario.PasswordHash, usuarioDto.Password)
                == PasswordVerificationResult.Failed)
            {
                return BadRequest("Contraseña incorrecta");
            }

            string token = CreateToken(_unUsuario);

            return Ok(token);
        }

        private string CreateToken(Usuario user)
        {
            // 1) Creamos la lista de "claims" (información que vamos a guardar dentro del token)
            //    Los claims son como los "datos impresos en la credencial" del usuario
            var claims = new List<Claim>
            {
                // Guardamos el nombre de usuario dentro del token
                new Claim(ClaimTypes.Name, user.Username)

                // Podrías agregar más claims, por ejemplo:
                // new Claim(ClaimTypes.Role, "Entrenador");
                // new Claim(ClaimTypes.Email, user.Email);
            };

            // 2) Obtenemos la clave secreta desde appsettings.json (AppSettings:Token)
            //    Esta clave sirve para "firmar" el token y que nadie lo pueda falsificar
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!)
            );

            // 3) Configuramos las credenciales de firma
            //    Es decir, qué clave usamos y con qué algoritmo vamos a firmar el token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // 4) Armamos el token JWT
            //    Acá definimos:
            //    - issuer: quién emite el token (tu API)
            //    - audience: quién va a consumirlo (el cliente, ej: tu frontend)
            //    - claims: los datos del usuario que pusimos antes
            //    - expires: cuándo vence el token (en este caso, dentro de 1 día)
            //    - signingCredentials: cómo se firma el token (clave + algoritmo)
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            // 5) Convertimos el objeto JwtSecurityToken a un string en formato JWT
            //    Este string es lo que recibe el frontend y luego manda en cada request
            //    dentro del header "Authorization: Bearer <token>"
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        // Este atributo indica que el endpoint requiere autenticación.
        // Solo los usuarios que envíen un token JWT válido podrán acceder.
        // Si el token es inválido, está vencido o no se envía, el servidor responderá con 401 (Unauthorized).
        [Authorize]

        // Define el tipo de solicitud HTTP (GET) y la ruta específica del endpoint.
        // En este caso, la URL completa será:  api/Usuario/autenticado
        // (asumiendo que el controlador se llama UsuarioController y tiene [Route("api/[controller]")]).
        [HttpGet("autenticado")]

        // Método que será ejecutado solo si la autenticación fue exitosa.
        // IActionResult permite devolver distintos tipos de respuestas HTTP.
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            // Si el usuario está autenticado correctamente, se devuelve una respuesta 200 OK
            // junto con un mensaje confirmando la autenticación.
            return Ok("Estás autenticado!");
        }
        // Ver 53:58
        #endregion

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto?>> Update(int id, UpdateUsuarioDto updateUsuarioDto)
        {
            var usuarioDto =  await _usuarioService.Update(id, updateUsuarioDto);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            var usuarioDto = await _usuarioService.Delete(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using MindFitIntelligence_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MindFitIntelligence_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Usuario user)
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
    }
}

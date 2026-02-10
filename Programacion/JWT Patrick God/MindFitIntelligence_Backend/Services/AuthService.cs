using Microsoft.IdentityModel.Tokens;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        /*
            ESTRUCTURA DE UN TOKEN JWT
            Un JWT se compone de tres partes separadas por puntos
                HEADER.PAYLOAD.SIGNATURE

            🔹 HEADER:
                Contiene información sobre el tipo de token y el algoritmo de firma.
                Ejemplo:
                {
                    "alg": "HS512",
                    "typ": "JWT"
                }

            🔹 PAYLOAD:
                Contiene los "claims" o datos del usuario (información embebida en el token).
                Ejemplo:
                {
                    "unique_name": "nicolas",
                    "role": "Entrenador",
                    "exp": 1739299200,
                    "iss": "https://tuservidor.com",
                    "aud": "https://tufrontend.com"
                }

            🔹 SIGNATURE:
                Es la firma que asegura que el token no fue alterado.
                Se genera así:
                    HMACSHA512(
                        base64UrlEncode(header) + "." + base64UrlEncode(payload),
                        claveSecreta
                    )

            🔸 El resultado final es un string con este formato:
                xxxxx.yyyyy.zzzzz
                donde:
                - xxxxx → Header codificado en Base64URL
                - yyyyy → Payload codificado en Base64URL
                - zzzzz → Firma codificada en Base64URL
        */
        // En .NET contruimos las estructura de un JWT de la siguiente manera:
        public string CreateToken(Usuario user)
        {
            // (1) PAYLOAD → datos (claims) que van dentro del token
            var claims = new List<Claim>
            {
                // Guardamos el nombre de usuario dentro del token
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Rol)
                // Podrías agregar más claims, por ejemplo:
                // new Claim(ClaimTypes.Role, "Entrenador");
                // new Claim(ClaimTypes.Email, user.Email);
            };

            // (2) SIGNATURE → se usa esta CLAVE SECRETA para firmar el token
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!)
            );

            // (3) (HEADER + (2) SIGNATURE) → define algoritmo y credenciales de firma
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // (4) (1) PAYLOAD + (3) (HEADER + SIGNATURE) → se construye el token completo
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"), // quién emite el token (tu API)
                audience: _configuration.GetValue<string>("AppSettings:Audience"), // audience: quién va a consumirlo (el cliente, ej: tu frontend)
                claims: claims, // claims: los datos del usuario que pusimos antes
                expires: DateTime.UtcNow.AddDays(1), // expires: cuándo vence el token (en este caso, dentro de 1 día)
                signingCredentials: creds //signingCredentials: cómo se firma el token (clave + algoritmo)
            );

            // (5) Combina HEADER + PAYLOAD + SIGNATURE (4) → genera el token final en formato JWT
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}

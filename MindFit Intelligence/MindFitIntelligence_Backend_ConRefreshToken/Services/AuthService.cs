using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MindFitIntelligence_Backend.DTOs;
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
        private IRepository<Usuario> _usuarioRepository;
        private IMapper _mapper;

        public AuthService(IConfiguration configuration,
            [FromKeyedServices("usuarioRepository")] IRepository<Usuario> usuarioRepository,
            IMapper mapper)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(Usuario usuario)
        {
            var refreshToken = GenerateRefreshToken();
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _usuarioRepository.Save();
            return refreshToken;
        }

        public async Task<UsuarioDto> Register(InsertUsuarioDto insertUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(insertUsuarioDto);

            usuario.PasswordHash = new PasswordHasher<Usuario>()
                .HashPassword(usuario, insertUsuarioDto.Password);

            await _usuarioRepository.Register(usuario);
            await _usuarioRepository.Save();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public async Task<TokenResponseDto?> Login(LoginUsuarioDto loginUsuarioDto)
        {
            // Busco el usuario por username
            var usuario = await _usuarioRepository.GetByUsername(loginUsuarioDto.Username);

            if (usuario == null)
                return null; // usuario no existe

            // Verifico la contraseña
            var result = new PasswordHasher<Usuario>()
                .VerifyHashedPassword(usuario, usuario.PasswordHash, loginUsuarioDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null; // contraseña incorrecta

            // Usando Resfreh JWT
            var response = new TokenResponseDto {
                AccessToken = CreateToken(usuario),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(usuario),
            };

            return response;
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

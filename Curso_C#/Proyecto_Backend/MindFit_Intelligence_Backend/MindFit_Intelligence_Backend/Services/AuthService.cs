using MindFit_Intelligence_Backend.DTOs;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Services
{
    public class AuthService : IAuthService
    {
        public string CreateToken(Usuario user)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponseDto?> Login(LoginUsuarioDto entityLogin)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponsableDto> Register(UsuarioResponsableInsertDto entityInsert)
        {
            throw new NotImplementedException();
        }
    }
}

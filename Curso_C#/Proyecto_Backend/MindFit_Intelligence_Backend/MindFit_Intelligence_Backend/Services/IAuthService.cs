using MindFit_Intelligence_Backend.DTOs;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Services
{
    public interface IAuthService
    {
        public Task<UsuarioResponsableDto> Register(UsuarioResponsableInsertDto entityInsert);
        public Task<TokenResponseDto?> Login(LoginUsuarioDto entityLogin);
        string CreateToken(Usuario user);
        public Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}

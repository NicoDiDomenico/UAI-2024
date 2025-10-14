using MindFitIntelligence_Backend.DTOs;
using MindFitIntelligence_Backend.Models;

namespace MindFitIntelligence_Backend.Services
{
    public interface IAuthService
    {
        public Task<UsuarioDto> Register(InsertUsuarioDto entityInsert);
        public Task<TokenResponseDto?> Login(LoginUsuarioDto entityLogin);
        string CreateToken(Usuario user);
        public Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
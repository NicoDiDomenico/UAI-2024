using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Services
{
    public interface IAuthService
    {
        public string CreateToken(Usuario user);
        public void SetPasswordHash(Usuario usuario, UsuarioInsertDto entityInsert);
        public Task<TokenResponseDto?> LoginAsync(LoginUsuarioDto entityLogin);
        public Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task ForgotPasswordAsync(ForgotPasswordRequestDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto dto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto dto);
    }
}

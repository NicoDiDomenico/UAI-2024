using MindFit_Intelligence_Backend.DTOs.Permisos;
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
        public Task ForgotPasswordAsync(ForgotPasswordRequestDto dto);
        public Task<bool> ResetPasswordAsync(ResetPasswordRequestDto dto);
        public Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto dto);
        public Task<PermisosActualizadosDto> ObtenerPermisosActuales(int idUsuario);
    }
}

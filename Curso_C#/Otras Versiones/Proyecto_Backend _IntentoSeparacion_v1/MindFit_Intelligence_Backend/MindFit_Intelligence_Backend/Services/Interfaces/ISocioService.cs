using MindFit_Intelligence_Backend.DTOs.Dia;
using MindFit_Intelligence_Backend.DTOs.Socios;
using MindFit_Intelligence_Backend.DTOs.Usuarios;

namespace MindFit_Intelligence_Backend.Services.Interfaces
{
    public interface ISocioService
    {
        List<string> Errors { get; }
        Task<IEnumerable<DiaDto>> GetDias();
        Task<List<UsuarioGridDto>> GetSociosGrid();
        Task<UsuarioDto?> GetSocioById(int id);
        Task<UsuarioDto?> Add(SocioInsertDto dto);
        Task<UsuarioDto?> Update(int id, SocioUpdateDto dto);
        Task<UsuarioDto?> SoftDeleteSocio(int id);
        Task<UsuarioDto?> AutoSoftDeleteSocio(int id);
        Task<UsuarioDto?> RecoverSoftDeletedSocio(int id);
        Task<UsuarioDto?> Delete(int id);
        Task<bool> ValidateDelete(int id);
        Task<bool> ValidateRecover(int id);
    }
}

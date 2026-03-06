using MindFit_Intelligence_Backend.DTOs.Responsables;
using MindFit_Intelligence_Backend.DTOs.Usuarios;

namespace MindFit_Intelligence_Backend.Services.Interfaces
{
    public interface IResponsableService
    {
        List<string> Errors { get; }
        Task<List<UsuarioGridDto>> GetResponsablesGrid();
        Task<UsuarioDto?> GetResponsableById(int id);
        Task<UsuarioDto?> Add(ResponsableInsertDto dto);
        Task<UsuarioDto?> Update(int id, ResponsableUpdateDto dto);
        Task<UsuarioDto?> Delete(int id);
        Task<bool> ValidateDelete(int id);
    }
}

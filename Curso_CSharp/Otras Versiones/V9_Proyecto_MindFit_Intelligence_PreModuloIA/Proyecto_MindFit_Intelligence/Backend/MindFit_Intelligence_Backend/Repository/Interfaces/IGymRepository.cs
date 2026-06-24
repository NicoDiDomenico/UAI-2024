using MindFit_Intelligence_Backend.DTOs.Gyms;
using MindFit_Intelligence_Backend.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindFit_Intelligence_Backend.Repository.Interfaces
{
    public interface IGymRepository
    {
        /// <summary>
        /// Obtiene todos los gimnasios activos desde la base maestra.
        /// </summary>
        Task<IEnumerable<GymPublicoDTO>> GetAllGymsActivosAsync();

        /// <summary>
        /// Indica si ya existe un gimnasio con el nombre normalizado indicado.
        /// </summary>
        Task<bool> ExistsByNombreAsync(string nombreGym);

        /// <summary>
        /// Agrega un nuevo gym a la base maestra y retorna su Id.
        /// </summary>
        Task<int> AddGymAsync(Gym gym);

        /// <summary>
        /// Actualiza el estado activo del gimnasio indicado.
        /// </summary>
        Task ActualizarActivoAsync(int idGym, bool activo);
    }
}

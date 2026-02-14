using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.DTOs.Usuarios
{
    public class UsuarioResponsableUpdateDto
    {
        public PersonaResponsableUpdateDto? PersonaResponsableUpdateDto { get; set; }

        // public DateTime FechaRegistro { get; set; } = DateTime.Now;

        #region JWT
        public string Username { get; set; } = null!;
        
        // Acá el password lo pienso agregar depues ya que en el update se necesita una trato especial, no se si se va a actualizar o no, y si se actualiza se tiene que hashear, entonces lo dejo para despues
        
        public string Rol { get; set; } = null!;
        #endregion
    }
}

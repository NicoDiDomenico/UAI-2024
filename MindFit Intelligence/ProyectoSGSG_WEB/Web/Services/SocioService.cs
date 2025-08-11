using Controlador;
using Modelo;
using System.Collections.Generic;

namespace Web.Services
{
    public class SocioService
    {
        private readonly ControladorGymSocio _ctrl = new ControladorGymSocio();

        public List<Socio> Listar() => _ctrl.Listar();

        public Socio Get(int id) => _ctrl.GetSocio(id);

        // Devuelve el Id generado (>0 si OK), y mensaje (del SP)
        public int Registrar(Socio socio, out string mensaje)
        {
            // evitar nulls
            socio.Rutinas ??= new List<Rutina>();
            return _ctrl.Registrar(socio, out mensaje);
        }

        // Devuelve true si OK, y mensaje (del SP)
        public bool Actualizar(Socio socio, out string mensaje)
        {
            socio.Rutinas ??= new List<Rutina>();
            return _ctrl.Actualizar(socio, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            var socio = new Socio { IdSocio = id };
            return _ctrl.Eliminar(socio, out mensaje);
        }
    }
}

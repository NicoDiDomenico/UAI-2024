using Controlador.State.Turno;

namespace Controlador.State.Turno
{
    public static class EstadoFactoryTurno
    {
        public static IEstadoTurno ObtenerEstado(string estadoNombre)
        {
            switch (estadoNombre)
            {
                case "En Curso":
                    return new EstadoEnCurso();
                case "Cancelado":
                    return new EstadoCancelado();
                case "Finalizado":
                    return new EstadoFinalizado();
                default:
                    return null;
            }
        }
    }
}

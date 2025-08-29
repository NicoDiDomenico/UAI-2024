using AutoMapper;
using MindFitIntelligence_Backend.Models;
using MindFitIntelligence_Backend.DTOs;

namespace MindFitIntelligence_Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>(); // Mapeo de Usuario a UsuarioDto, los nombres de las propiedades coinciden. Cuando Usuario tenga una propiedad que no esté en UsuarioDto, se ignora.
            CreateMap<IEnumerable<Usuario>, IEnumerable<UsuarioDto>>();
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}

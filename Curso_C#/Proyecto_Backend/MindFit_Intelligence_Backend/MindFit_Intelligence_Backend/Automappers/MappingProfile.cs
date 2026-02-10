using AutoMapper;
using MindFit_Intelligence_Backend.DTOs.Personas;
using MindFit_Intelligence_Backend.DTOs.Usuarios;
using MindFit_Intelligence_Backend.Models;

namespace MindFit_Intelligence_Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Usuario con PersonaResponsable y viceversa
            CreateMap<Usuario, UsuarioResponsableDto>();
            CreateMap<PersonaResponsable, PersonaResponsableDto>();
            CreateMap<UsuarioResponsableInsertDto, Usuario>();
            CreateMap<UsuarioResponsableUpdateDto, Usuario>();
            CreateMap<PersonaResponsableUpdateDto, PersonaResponsable>();
        }
    }
}

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
            // Entidad -> DTO
            CreateMap<Usuario, UsuarioResponsableDto>()
                .ForMember(
                    dest => dest.PersonaResponsableDto,
                    opt => opt.MapFrom(src => src.PersonaResponsable)
                );
            CreateMap<PersonaResponsable, PersonaResponsableDto>();

            // DTO -> Entidad
            CreateMap<UsuarioResponsableInsertDto, Usuario>();
            CreateMap<UsuarioResponsableUpdateDto, Usuario>();
            CreateMap<PersonaResponsableInsertDto, PersonaResponsable>();
            CreateMap<PersonaResponsableUpdateDto, PersonaResponsable>();
        }
    }
}

using MindFit_Intelligence_Backend.Repository;
using AutoMapper;
using MindFit_Intelligence_Backend.Models;
using MindFit_Intelligence_Backend.DTOs.Personas;

namespace MindFit_Intelligence_Backend.Services
{
    public class PersonaResponsableService : IPersonaService<PersonaResponsableDto>
    {
        private IPersonaResponsableRepository _PersonaResponsableRepository;
        private IMapper _mapper;
        // falta manejo de errores

        public PersonaResponsableService(IPersonaResponsableRepository PersonaResponsableRepository, IMapper mapper) {
            _PersonaResponsableRepository = PersonaResponsableRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonaResponsableDto>> Get() 
        {
            IEnumerable<PersonaResponsable> PersonaResponsables = await _PersonaResponsableRepository.Get();

            IEnumerable<PersonaResponsableDto> PersonaResponsablesDtos = PersonaResponsables.Select(p => _mapper.Map<PersonaResponsableDto>(p));

            return PersonaResponsablesDtos;
        }
            
        public async Task<PersonaResponsableDto?> GetById(int id)
        {
            PersonaResponsable? PersonaResponsable = await _PersonaResponsableRepository.GetById(id);

            if (PersonaResponsable != null)
            {
                PersonaResponsableDto PersonaResponsableDto = _mapper.Map<PersonaResponsableDto>(PersonaResponsable);
                return PersonaResponsableDto;
            }
            return null;
        }
    }
}

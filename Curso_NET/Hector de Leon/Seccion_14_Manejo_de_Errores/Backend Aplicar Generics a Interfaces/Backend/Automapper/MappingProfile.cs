using AutoMapper;
using Backend.DTOs;
using Backend.Modelos;

namespace Backend.Automapper
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            //// 73. AutoMapper con propiedades con el mismo
            //       <ObjetoOrigen, ObjetoDestino> --> Sirve para insertar los atributos con el mismo nombre a otro objeto, independientemente de la cantidad de atributus
            CreateMap<BeerInsertDto, Beer>();

            //// 74.AutoMapper con propiedades con distinto nombre.
            // 📌 AutoMapper: configuración de mapeo entre Beer y BeerDto
            // CreateMap<Beer, BeerDto>() → Indica que queremos transformar objetos Beer en BeerDto.
            // .ForMember(dto => dto.Id, m => m.MapFrom(b => b.BeerID))
            //   👉 Le decimos que, al llenar el DTO, la propiedad "Id" se obtenga desde "BeerID".
            //   Esto es necesario porque los nombres son distintos (BeerID ≠ Id).
            // 🔄 Resultado: Beer.BeerID se asigna a BeerDto.Id, y las demás propiedades con el mismo nombre se copian automáticamente.
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.Id,
                           m => m.MapFrom(b => b.BeerID));
            //// 75.AutoMapper con objeto existente - se define igual ya que es <ObjetoOrigen, ObjetoDestino(ObjetoExistente)>, pero cuando se llame a _mapper.Map se tendra que poner ambos parametros, dandoa a entender que es una modificación
            CreateMap<BeerUpdateDto, Beer>();

        }
    }
}

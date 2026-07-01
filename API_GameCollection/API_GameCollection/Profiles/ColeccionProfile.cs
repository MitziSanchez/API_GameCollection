using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class ColeccionProfile : Profile
    {        
        public ColeccionProfile() {

            // Mapeo de colecciones existentes, considera los detalles de la coleccion (list)
            CreateMap<Coleccion, ColeccionCompletaDTO>()
                .ForMember(dest => dest.DetallesColeccion, opt => opt.MapFrom(src => src.DetalleColeccions));

            // Mapeo de coleccion existente, simple
            CreateMap<Coleccion, ColeccionDTO>();
        }
    }
}

using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class DetalleColeccionProfile : Profile
    {
        public DetalleColeccionProfile()
        {
            // Mapeo de nuevos detalles de colección
            CreateMap<DetalleColeccionCreateDTO, DetalleColeccion>();

            // Mapeo de detalles de colección existentes, define mapeo para dtos anidados
            CreateMap<DetalleColeccion, DetalleColeccionDTO>()
                .ForMember(dest => dest.Videojuego, opt => opt.MapFrom(src => src.Videojuego))
                .ForMember(dest => dest.EstadoJuego, opt => opt.MapFrom(src => src.EstadoJuego));
        }
    }
}

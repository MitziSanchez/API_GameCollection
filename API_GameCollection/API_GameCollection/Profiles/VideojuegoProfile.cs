using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class VideojuegoProfile : Profile
    {
        public VideojuegoProfile()
        {
            // Mapeo Videojuegos existentes
            CreateMap<Videojuego, VideojuegoDTO>()
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Plataforma, opt => opt.MapFrom(src => src.Plataforma));

            // Mapeo create videojuego
            CreateMap<VideojuegoCreateDTO, Videojuego>();
        }
    }
}

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
                .ForMember(dest => dest.GeneroNombre, opt => opt.MapFrom(src => src.Genero.Nombre))
                .ForMember(dest => dest.PlataformaNombre, opt => opt.MapFrom(src => src.Plataforma.Nombre));

            // Mapeo create videojuego
            CreateMap<VideojuegoCreateDTO, Videojuego>();
        }
    }
}

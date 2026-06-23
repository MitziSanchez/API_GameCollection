using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class EstadoJuegoProfile : Profile
    {
        public EstadoJuegoProfile()
        {
            // Mapeo estados de juego existentes
            CreateMap<EstadoJuego, EstadoJuegoDTO>();
        }
    }
}

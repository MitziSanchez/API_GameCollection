using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class PlataformaProfile : Profile
    {
        public PlataformaProfile()
        {
            // Mapeo de plataformas existentes
            CreateMap<Plataforma, PlataformaDTO>();
        }
    }
}

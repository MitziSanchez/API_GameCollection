using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class GeneroProfile : Profile
    {
        public GeneroProfile()
        {
            // Mapeo generos existentes
            CreateMap<Genero, GeneroDTO>();
        }
    }
}

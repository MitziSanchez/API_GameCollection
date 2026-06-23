using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;

namespace API_GameCollection.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // Mapeo para creación de usuario
            CreateMap<UsuarioCreateDTO, Usuario>();

            // Mapeo para usuario existente
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}

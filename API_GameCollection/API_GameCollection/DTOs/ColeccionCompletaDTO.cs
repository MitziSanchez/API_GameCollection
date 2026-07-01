using API_GameCollection.Models;

namespace API_GameCollection.DTOs
{
    public class ColeccionCompletaDTO
    {
        public int ColeccionId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<DetalleColeccionDTO>? DetallesColeccion { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace API_GameCollection.DTOs
{
    public class DetalleColeccionCreateDTO
    {
        [Required(ErrorMessage = "El Id de colección es obligatorio.")]
        public int ColeccionId { get; set; }

        [Required(ErrorMessage = "El Id de videojuego es obligatorio.")]
        public int VideojuegoId { get; set; }

        // Se crea automaticamente en bd
        //public DateTime FechaCreacion { get; set; }

        // No se solicita, se define internamente en adquirido 1
        //public int EstadoJuegoId { get; set; } = 1;
    }
}

using API_GameCollection.Models;
using System.ComponentModel.DataAnnotations;

namespace API_GameCollection.DTOs
{
    public class DetalleColeccionUpdateDTO
    {        
        /// <summary>
        /// Calificación considera valores de 0 a 10
        /// </summary>
        public decimal? Calificacion { get; set; }

        // Se asigna internamente
        //public DateTime? FechaCalificacion { get; set; }

        [Required(ErrorMessage = "El estado de juego es obligatorio.")]
        public int EstadoJuegoId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace API_GameCollection.DTOs
{
    public class VideojuegoCreateDTO
    {
        [Required(ErrorMessage = "El título del videojuego es obligatorio.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El género del videojuego es obligatorio.")]
        public int GeneroId { get; set; }

        [Required(ErrorMessage = "La plataforma del videojuego es obligatoria.")]
        public int PlataformaId { get; set; }
    }
}

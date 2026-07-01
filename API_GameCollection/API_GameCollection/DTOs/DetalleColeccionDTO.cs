namespace API_GameCollection.DTOs
{
    public class DetalleColeccionDTO
    {
        public int DetalleColeccionId { get; set; }

        public int ColeccionId { get; set; }

        //public int VideojuegoId { get; set; }

        public VideojuegoDTO Videojuego { get; set; }

        //public DateTime FechaCreacion { get; set; }

        public decimal? Calificacion { get; set; }

        //public DateTime? FechaCalificacion { get; set; }

        //public int EstadoJuegoId { get; set; }

        public EstadoJuegoDTO EstadoJuego { get; set; }    
        
    }
}

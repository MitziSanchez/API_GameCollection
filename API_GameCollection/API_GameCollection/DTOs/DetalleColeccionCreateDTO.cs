namespace API_GameCollection.DTOs
{
    public class DetalleColeccionCreateDTO
    {
        public int ColeccionId { get; set; }

        public int VideojuegoId { get; set; }

        // Se crea automaticamente en bd
        //public DateTime FechaCreacion { get; set; }

        // No se solicita, se define internamente en adquirido 1
        //public int EstadoJuegoId { get; set; } = 1;
    }
}

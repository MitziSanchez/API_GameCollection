namespace API_GameCollection.DTOs
{
    public class ColeccionDTO
    {
        public class ColeccionCompletaDTO
        {
            public int ColeccionId { get; set; }

            public int UsuarioId { get; set; }

            public DateTime FechaCreacion { get; set; }
        }
    }
}

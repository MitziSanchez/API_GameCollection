namespace API_GameCollection.DTOs
{
    public class VideojuegoDTO
    {
        public int VideojuegoId { get; set; }
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public string? GeneroNombre { get; set; }
        public int PlataformaId { get; set; }
        public string? PlataformaNombre { get; set; }
    }
}

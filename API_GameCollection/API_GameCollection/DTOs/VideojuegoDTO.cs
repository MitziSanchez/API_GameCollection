namespace API_GameCollection.DTOs
{
    public class VideojuegoDTO
    {
        public int VideojuegoId { get; set; }

        public string Titulo { get; set; }

        //public int GeneroId { get; set; }

        //public int PlataformaId { get; set; }

        public GeneroDTO Genero { get; set; }
        
        public PlataformaDTO Plataforma { get; set; }
    }
}

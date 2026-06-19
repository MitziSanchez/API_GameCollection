using System;
using System.Collections.Generic;

namespace API_GameCollection.Models;

public partial class Plataforma
{
    public int PlataformaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
}

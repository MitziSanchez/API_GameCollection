using System;
using System.Collections.Generic;

namespace API_GameCollection.Models;

public partial class Videojuego
{
    public int VideojuegoId { get; set; }

    public int GeneroId { get; set; }

    public int PlataformaId { get; set; }

    public string Titulo { get; set; } = null!;

    public virtual ICollection<DetalleColeccion> DetalleColeccions { get; set; } = new List<DetalleColeccion>();

    public virtual Genero Genero { get; set; } = null!;

    public virtual Plataforma Plataforma { get; set; } = null!;
}

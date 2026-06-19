using System;
using System.Collections.Generic;

namespace API_GameCollection.Models;

public partial class EstadoJuego
{
    public int EstadoJuegoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<DetalleColeccion> DetalleColeccions { get; set; } = new List<DetalleColeccion>();
}

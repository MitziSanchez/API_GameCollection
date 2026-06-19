using System;
using System.Collections.Generic;

namespace API_GameCollection.Models;

public partial class Coleccion
{
    public int ColeccionId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<DetalleColeccion> DetalleColeccions { get; set; } = new List<DetalleColeccion>();

    public virtual Usuario Usuario { get; set; } = null!;
}

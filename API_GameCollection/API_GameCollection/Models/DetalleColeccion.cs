using System;
using System.Collections.Generic;

namespace API_GameCollection.Models;

public partial class DetalleColeccion
{
    public int DetalleColeccionId { get; set; }

    public int ColeccionId { get; set; }

    public int VideojuegoId { get; set; }

    public DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Calificación considera valores de 0 a 10
    /// </summary>
    public decimal? Calificacion { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public int EstadoJuegoId { get; set; }

    public virtual Coleccion Coleccion { get; set; } = null!;

    public virtual EstadoJuego EstadoJuego { get; set; } = null!;

    public virtual Videojuego Videojuego { get; set; } = null!;
}

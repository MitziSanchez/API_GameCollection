using System;
using System.Collections.Generic;

namespace API_GameCollection.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public virtual Coleccion? Coleccion { get; set; }
}

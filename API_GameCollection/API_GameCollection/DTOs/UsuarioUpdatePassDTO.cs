using System.ComponentModel.DataAnnotations;

namespace API_GameCollection.DTOs
{
    public class UsuarioUpdatePassDTO
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public string ContrasenaActual { get; set; }

        [Required(ErrorMessage = "La contraseña nueva es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
            ErrorMessage = "La contraseña debe incluir al menos una mayúscula, un número y un caracter especial.")]
        public string ContrasenaNueva { get; set; }
    }
}

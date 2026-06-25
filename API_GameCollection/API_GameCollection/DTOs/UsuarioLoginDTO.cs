using System.ComponentModel.DataAnnotations;

namespace API_GameCollection.DTOs
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
            ErrorMessage = "La contraseña debe incluir al menos una mayúscula, un número y un caracter especial.")]
        public string Contrasena { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace API_GameCollection.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<string> _hasher = new PasswordHasher<string>();

        // Crear hash para guardar en BD
        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        // Validar contraseña 
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}

using ReserveX.Core.Application.Interfaces;

namespace ReserveX.Core.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public  string Hash(string input)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(input, workFactor: 12);
            return hash;
        }

        public  bool PasswordIsValid(string input, string hash)
        {
            bool ok = BCrypt.Net.BCrypt.Verify(input, hash);
            return ok;
        }
    }
}

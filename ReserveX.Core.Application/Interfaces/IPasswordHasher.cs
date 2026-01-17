namespace ReserveX.Core.Application.Interfaces
{
    public interface IPasswordHasher
    {
        public  string Hash(string input);
         public  bool PasswordIsValid(string input, string hash);
    }
}
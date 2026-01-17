namespace ReserveX.Core.Application.Interfaces
{
    public interface IHasher
    {
        public  string Hash(string input);
         public  bool CompareHash(string input, string hash);
    }
}
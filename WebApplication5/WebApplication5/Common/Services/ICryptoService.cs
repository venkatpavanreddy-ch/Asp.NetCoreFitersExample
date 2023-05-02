namespace WebApplication5.Common.Services
{
    public interface ICryptoService
    {
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);
    }
}

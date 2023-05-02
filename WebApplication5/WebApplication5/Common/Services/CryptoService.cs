using WebApplication5.Models;

namespace WebApplication5.Common.Services
{
    public class CryptoService : ICryptoService
    {

        protected static CryptoSettings _cryptoSettings;

        public CryptoService(CryptoSettings cryptoSettings)
        {
            _cryptoSettings = cryptoSettings;
        }

        public string Encrypt(string plainText)
        {
            AesCryptography crypto = new AesCryptography(_cryptoSettings.AesCryptoKey, _cryptoSettings.AesCryptoIV);
            return crypto.Encrypt(plainText);
        }

        public string Decrypt(string cipherText)
        {
            AesCryptography crypto = new AesCryptography(_cryptoSettings.AesCryptoKey, _cryptoSettings.AesCryptoIV);
            return crypto.Decrypt(cipherText);
        }
    }
}

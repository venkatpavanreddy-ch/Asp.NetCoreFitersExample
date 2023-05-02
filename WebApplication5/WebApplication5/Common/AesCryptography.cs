using System.Security.Cryptography;
using System.Text;

namespace WebApplication5.Common
{
    public class AesCryptography
    {
        private string _key;
        private string _iv;

        public AesCryptography(string key, string iv)
        {
            _key = key;
            _iv = iv;
        }

        public string Decrypt(string cipherText)
        {
            var key = Encoding.UTF8.GetBytes(_key);
            var iv = Encoding.UTF8.GetBytes(_iv);

            var encrypted = Convert.FromBase64String(cipherText);
            return Decrypt(encrypted, key, iv);
        }

        public string Encrypt(string plaintText)
        {
            var key = Encoding.UTF8.GetBytes(_key);
            var iv = Encoding.UTF8.GetBytes(_iv);

            var encryptedBytes = Encrypt(plaintText, key, iv);
            return Convert.ToBase64String(encryptedBytes);
        }

        private string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("iv");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = Aes.Create())
            {
                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(key, iv);
                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }
            return plaintext;
        }

        private byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("iv");
            }

            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = Aes.Create())
            {
                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(key, iv);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
    }
}

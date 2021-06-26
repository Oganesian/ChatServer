using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyServices.DecryptionServices
{
    public class DiffieHellmanDecryptionService : IDiffieHellmanDecryptionService
    {
        public string Decrypt(byte[] encryptedMessage, byte[] sharedPrivateKey, byte[] iv)
        {
            using Aes aes = new AesCryptoServiceProvider();
            aes.Key = sharedPrivateKey;
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;

            // Decrypt the message
            using MemoryStream plaintext = new MemoryStream();
            using CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(encryptedMessage, 0, encryptedMessage.Length);
            cs.Close();

            return Encoding.UTF8.GetString(plaintext.ToArray());
        }
    }
}

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyServices.EncryptionServices
{
    public class DiffieHellmanEncryptionService : IDiffieHellmanEncryptionService
    {
        public byte[] Encrypt(string message, byte[] sharedPrivateKey, out byte[] iv)
        {
            using Aes aes = new AesCryptoServiceProvider();
            aes.Key = sharedPrivateKey;
            iv = aes.IV;
            aes.Padding = PaddingMode.PKCS7;

            // Encrypt the message
            using MemoryStream ciphertext = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] plaintextMessage = Encoding.UTF8.GetBytes(message);
            cs.Write(plaintextMessage, 0, plaintextMessage.Length);
            cs.Close();

            // Return the encrypted message as a byte array
            return ciphertext.ToArray();
        }
    }
}

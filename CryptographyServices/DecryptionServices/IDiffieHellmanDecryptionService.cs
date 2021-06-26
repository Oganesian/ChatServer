using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyServices.DecryptionServices
{
    public interface IDiffieHellmanDecryptionService
    {
        public string Decrypt(byte[] encryptedMessage, byte[] sharedPrivateKey, byte[] iv);
    }
}

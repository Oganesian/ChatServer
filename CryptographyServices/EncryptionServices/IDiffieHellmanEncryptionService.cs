using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyServices.EncryptionServices
{
    public interface IDiffieHellmanEncryptionService
    {
        public byte[] Encrypt(string message, byte[] sharedPrivateKey, out byte[] iv);
    }
}

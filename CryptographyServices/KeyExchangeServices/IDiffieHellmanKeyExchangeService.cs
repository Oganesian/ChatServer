using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyServices.KeyExchangeServices
{
    public interface IDiffieHellmanKeyExchangeService
    {
        public (byte[] publicKey, byte[] privateKey) CreateKeyPair();
        public byte[] CreateSharedPrivateKey(byte[] privateKey, byte[] secondPartyPublicKey);
    }
}

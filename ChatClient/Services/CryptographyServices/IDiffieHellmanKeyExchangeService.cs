using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Services.CryptographyServices
{
    public interface IDiffieHellmanKeyExchangeService
    {
        public (byte[] publicKey, byte[] privateKey) CreateKeyPair();
        public byte[] CreateSharedPrivateKey(byte[] privateKey, byte[] secondPartyPublicKey);
    }
}

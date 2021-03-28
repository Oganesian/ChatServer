using System.Security.Cryptography;

namespace CryptographyServices.KeyExchangeServices
{
    public class DiffieHellmanKeyExchangeService : IDiffieHellmanKeyExchangeService
    {
        public (byte[] publicKey, byte[] privateKey) CreateKeyPair()
        {
            using ECDiffieHellmanCng cng = new ECDiffieHellmanCng(
                // need to do this to be able to export private key
                CngKey.Create(
                    CngAlgorithm.ECDiffieHellmanP256,
                    null,
                    new CngKeyCreationParameters
                    { ExportPolicy = CngExportPolicies.AllowPlaintextExport }));
            cng.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            cng.HashAlgorithm = CngAlgorithm.Sha256;
            // export both private and public keys and return
            var privateKey = cng.Key.Export(CngKeyBlobFormat.EccPrivateBlob);
            var publicKey = cng.PublicKey.ToByteArray();

            return (publicKey, privateKey);
        }

        public byte[] CreateSharedPrivateKey(byte[] privateKey, byte[] secondPartyPublicKey)
        {
            // this returns shared secret, not private key
            // initialize algorithm with private key of one party
            using ECDiffieHellmanCng cng = new ECDiffieHellmanCng(CngKey.Import(privateKey, CngKeyBlobFormat.EccPrivateBlob));
            cng.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            cng.HashAlgorithm = CngAlgorithm.Sha256;

            // use public key of another party
            return cng.DeriveKeyMaterial(CngKey.Import(secondPartyPublicKey, CngKeyBlobFormat.EccPublicBlob));
        }
    }
}

using System;
using System.Text;
using BlockChainCourse.Cryptography;

namespace BlockChainCourse.BlockWithTransactionPool
{
    public class KeyStore : IKeyStore
    {
        private DigitalSignature DigitalSignature { get; set; }
        public byte[] AuthenticatedHashKey { get; private set; }

        public KeyStore(byte[] authenticatedHashKey)
        {
            AuthenticatedHashKey = authenticatedHashKey;
            DigitalSignature = new DigitalSignature();
            DigitalSignature.AssignNewKey();
        }

        public string SignBlock(string blockHash)
        {
            return Convert.ToBase64String(DigitalSignature.SignData(Convert.FromBase64String(blockHash)));
        }

        public bool VerifyBlock(string blockHash, string signature)
        {
            return DigitalSignature.VerifySignature(Convert.FromBase64String(blockHash), Convert.FromBase64String(signature));  
        }
    }
}

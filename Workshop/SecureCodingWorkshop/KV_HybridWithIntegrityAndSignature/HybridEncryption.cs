using System;
using System.Security.Cryptography;

namespace AzureKeyVault.HybridWithIntegrityAndSignature
{
    public class HybridEncryption
    {
        readonly IKeyVault _keyVault;
        readonly AesEncryption _aes = new AesEncryption();

        public HybridEncryption(IKeyVault keyVault)
        {
            _keyVault = keyVault;
        }
   

        public EncryptedPacket EncryptData(byte[] original, string keyId)
        {
            throw new NotImplementedException();
        }

        public byte[] DecryptData(EncryptedPacket encryptedPacket, string keyId)
        {
            throw new NotImplementedException();
        }

        private static bool Compare(byte[] array1, byte[] array2)
        {
            var result = array1.Length == array2.Length;

            for (var i = 0; i < array1.Length && i < array2.Length; ++i)
            {
                result &= array1[i] == array2[i];
            }

            return result;
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;

        } 
    }
}

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
            var sessionKey = _aes.GenerateRandomNumber(32);

            var encryptedPacket = new EncryptedPacket { Iv = _aes.GenerateRandomNumber(16) };

            encryptedPacket.EncryptedData = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv);

            encryptedPacket.EncryptedSessionKey = _keyVault.EncryptAsync(keyId, sessionKey).Result;

            using (var hmac = new HMACSHA256(sessionKey))
            {
                encryptedPacket.Hmac = hmac.ComputeHash(Combine(encryptedPacket.EncryptedData, encryptedPacket.Iv));
            }

            encryptedPacket.Signature = _keyVault.Sign(keyId, encryptedPacket.Hmac).Result;

            return encryptedPacket;
        }

        public byte[] DecryptData(EncryptedPacket encryptedPacket, string keyId)
        {
            var decryptedSessionKey = _keyVault.DecryptAsync(keyId, encryptedPacket.EncryptedSessionKey).Result;

            using (var hmac = new HMACSHA256(decryptedSessionKey))
            {
                var hmacToCheck = hmac.ComputeHash(Combine(encryptedPacket.EncryptedData, encryptedPacket.Iv));

                if (!Compare(encryptedPacket.Hmac, hmacToCheck))
                {
                    throw new CryptographicException(
                        "HMAC for decryption does not match encrypted packet.");
                }

                if (!_keyVault.Verify(keyId, encryptedPacket.Hmac, encryptedPacket.Signature).Result)
                {
                    throw new CryptographicException(
                        "Digital Signature can not be verified.");
                }
            }

            var decryptedData = _aes.Decrypt(encryptedPacket.EncryptedData, decryptedSessionKey,
                                             encryptedPacket.Iv);

            return decryptedData;
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

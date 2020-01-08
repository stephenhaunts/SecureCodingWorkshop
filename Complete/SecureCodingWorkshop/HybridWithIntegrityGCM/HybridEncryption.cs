using System;
using System.Security.Cryptography;

namespace SecureCodingWorkshop.HybridWithIntegrityAndSignature
{
    public class HybridEncryption
    {
        private readonly AesGCMEncryption _aes = new AesGCMEncryption();

        public EncryptedPacket EncryptData(byte[] original, RSAWithRSAParameterKey rsaParams)
        {
            var sessionKey = _aes.GenerateRandomNumber(32);

            var encryptedPacket = new EncryptedPacket { Iv = _aes.GenerateRandomNumber(12) };

            (byte[] ciphereText, byte[] tag) encrypted = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv, null);

            encryptedPacket.EncryptedData = encrypted.ciphereText;
            encryptedPacket.Tag = encrypted.tag;
            encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(sessionKey);
         
            return encryptedPacket;
        }

        public byte[] DecryptData(EncryptedPacket encryptedPacket, RSAWithRSAParameterKey rsaParams)
        {
            var decryptedSessionKey = rsaParams.DecryptData(encryptedPacket.EncryptedSessionKey);

            
            var decryptedData = _aes.Decrypt(encryptedPacket.EncryptedData, decryptedSessionKey,
                                             encryptedPacket.Iv, encryptedPacket.Tag, null);

            return decryptedData;
        }
    }
}

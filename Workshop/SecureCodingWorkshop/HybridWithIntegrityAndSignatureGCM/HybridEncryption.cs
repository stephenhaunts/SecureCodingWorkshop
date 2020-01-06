using System;
using System.Security.Cryptography;

namespace SecureCodingWorkshop.HybridWithIntegrityAndSignature
{
    public class HybridEncryption
    {
        private readonly AesGCMEncryption _aes = new AesGCMEncryption();

        public EncryptedPacket EncryptData(byte[] original, RSAWithRSAParameterKey rsaParams,
                                           DigitalSignature digitalSignature)
        {
            var sessionKey = _aes.GenerateRandomNumber(32);

            var encryptedPacket = new EncryptedPacket { Iv = _aes.GenerateRandomNumber(12) };

            (byte[] ciphereText, byte[] tag) encrypted = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv, null);

            encryptedPacket.EncryptedData = encrypted.ciphereText;
            encryptedPacket.Tag = encrypted.tag;
            encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(sessionKey);

            using (var hmac = new HMACSHA256(sessionKey))
            {
                var temp = hmac.ComputeHash(Combine(encryptedPacket.EncryptedData, encryptedPacket.Iv));
                encryptedPacket.Hmac = hmac.ComputeHash(Combine(temp, encryptedPacket.Tag));
            }

            encryptedPacket.Signature = digitalSignature.SignData(encryptedPacket.Hmac);

            return encryptedPacket;
        }

        public byte[] DecryptData(EncryptedPacket encryptedPacket, RSAWithRSAParameterKey rsaParams,
                                  DigitalSignature digitalSignature)
        {
            var decryptedSessionKey = rsaParams.DecryptData(encryptedPacket.EncryptedSessionKey);


            if (!digitalSignature.VerifySignature(encryptedPacket.Hmac,
                                      encryptedPacket.Signature))
            {
                throw new CryptographicException(
                    "Digital Signature can not be verified.");
            }

            var decryptedData = _aes.Decrypt(encryptedPacket.EncryptedData, decryptedSessionKey,
                                             encryptedPacket.Iv, encryptedPacket.Tag, null);

            return decryptedData;
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

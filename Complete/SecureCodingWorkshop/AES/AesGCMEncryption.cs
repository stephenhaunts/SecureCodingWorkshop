using System.Security.Cryptography;

namespace SecureCodingWorkshop.AES
{
    public class AesGcmEncryption
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);

            return randomNumber;
        }

        public static (byte[], byte[]) Encrypt(byte[] dataToEncrypt, byte[] key, byte[] nonce, byte[] associatedData)
        {
            // these will be filled during the encryption
            var tag = new byte[16];
            var ciphertext = new byte[dataToEncrypt.Length];

            using (var aesGcm = new AesGcm(key))
            {
                aesGcm.Encrypt(nonce, dataToEncrypt, ciphertext, tag, associatedData);
            }

            return (ciphertext, tag);
        }

        public static byte[] Decrypt(byte[] cipherText, byte[] key, byte[] nonce, byte[] tag, byte[] associatedData)
        {
            var decryptedData = new byte[cipherText.Length];

            using var aesGcm = new AesGcm(key);
            aesGcm.Decrypt(nonce, cipherText, tag, decryptedData, associatedData);

            return decryptedData;
        }    
    }
}

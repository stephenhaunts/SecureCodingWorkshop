using System.Security.Cryptography;
using System.Text;

namespace SecureCodingWorkshop.NewRSA
{
    internal class NewRSA
    {
        private readonly System.Security.Cryptography.RSA _rsa; 
 
        public NewRSA()
        {
            _rsa = System.Security.Cryptography.RSA.Create(2048);
        }

        public byte[] Encrypt(string dataToEncrypt)
        {
            return _rsa.Encrypt(Encoding.UTF8.GetBytes(dataToEncrypt), RSAEncryptionPadding.OaepSHA256);
        }

        public byte[] Encrypt(byte[] dataToEncrypt)
        {
            return _rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
        }

        public byte[] Decrypt(byte[] dataToDecrypt)
        {
            return _rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
        }

        public byte[] ExportPrivateKey(int numberOfIterations, string password)
        {
            var keyParams = new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, numberOfIterations);  
            var encryptedPrivateKey = _rsa.ExportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), keyParams);

            return encryptedPrivateKey;
        }

        public void ImportEncryptedPrivateKey(byte[] encryptedKey, string password)
        {
            _rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), encryptedKey, out _);
        }

        public byte[] ExportPublicKey()
        {
            return _rsa.ExportRSAPublicKey();
        }

        public void ImportPublicKey(byte[] publicKey)
        {
            _rsa.ImportRSAPublicKey(publicKey, out _);
        }
    }
}

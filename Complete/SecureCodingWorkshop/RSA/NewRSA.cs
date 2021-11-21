/*
MIT License

Copyright (c) 2021

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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

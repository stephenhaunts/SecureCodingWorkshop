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

namespace SecureCodingWorkshop.DigitalSignature;

class NewDigitalSignature
{
    private RSA rsa; 
 
    public NewDigitalSignature()
    {
        rsa = RSA.Create(2048);
    }

    public static byte[] ComputeHashSha256(byte[] toBeHashed)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(toBeHashed);
    }

    public (byte[], byte[]) SignData(byte[] dataToSign)
    {
        var hashOfDataToSign = ComputeHashSha256(dataToSign);
        
        return (rsa.SignHash(hashOfDataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1), hashOfDataToSign);
    }

    public bool VerifySignature(byte[] signature, byte[] hashOfDataToSign)
    {
        return rsa.VerifyHash(hashOfDataToSign, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);            
    }

    public byte[] ExportPrivateKey(int numberOfIterations, string password)
    {
        byte[] encryptedPrivateKey;
           
        var keyParams = new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, numberOfIterations);  
        encryptedPrivateKey = rsa.ExportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), keyParams);

        return encryptedPrivateKey;
    }

    public void ImportEncryptedPrivateKey(byte[] encryptedKey, string password)
    {
        rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), encryptedKey, out _);
    }

    public byte[] ExportPublicKey()
    {
        return rsa.ExportRSAPublicKey();
    }

    public void ImportPublicKey(byte[] publicKey)
    {
        rsa.ImportRSAPublicKey(publicKey, out _);
    }
}
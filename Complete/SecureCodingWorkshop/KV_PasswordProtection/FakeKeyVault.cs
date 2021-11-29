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
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AzureKeyVault.PasswordProtection;

public class FakeKeyVault : IKeyVault
{
    Dictionary<string, RSAParameters> publicKey = new Dictionary<string, RSAParameters>();
    Dictionary<string, RSAParameters> privateKey = new Dictionary<string, RSAParameters>();
    Dictionary<string, string> secret = new Dictionary<string, string>();

    public async Task<string> CreateKeyAsync(string keyName)
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            publicKey.Add(keyName, rsa.ExportParameters(false));
            privateKey.Add(keyName, rsa.ExportParameters(true));
        }

        await Task.CompletedTask;
        return keyName;
    }

    public async Task<byte[]> DecryptAsync(string keyId, byte[] dataToDecrypt)
    {
        byte[] plain;

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;

            rsa.ImportParameters(privateKey[keyId]);
            plain = rsa.Decrypt(dataToDecrypt, true);
        }

        await Task.CompletedTask;

        return plain;
    }

    public async Task DeleteKeyAsync(string keyName)
    {
        publicKey.Remove(keyName);
        privateKey.Remove(keyName);

        await Task.CompletedTask;
        return;
    }

    public async Task<byte[]> EncryptAsync(string keyId, byte[] dataToEncrypt)
    {
        byte[] cipherbytes;

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(publicKey[keyId]);

            cipherbytes = rsa.Encrypt(dataToEncrypt, true);
        }

        await Task.CompletedTask;

        return cipherbytes;
           
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        await Task.CompletedTask;

        return secret[secretName];
    }

    public async Task<string> SetSecretAsync(string secretName, string secretValue)
    {
        await Task.CompletedTask;
        secret.Add(secretName, secretValue);

        return secretName;
    }

    public async Task<byte[]> Sign(string keyId, byte[] hash)
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(privateKey[keyId]);

            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm("SHA256");

            await Task.CompletedTask;

            return rsaFormatter.CreateSignature(hash);
        }
    }

    public async Task<bool> Verify(string keyId, byte[] hash, byte[] signature)
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.ImportParameters(publicKey[keyId]);

            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA256");

            await Task.CompletedTask;

            return rsaDeformatter.VerifySignature(hash, signature);
        }
    }
}
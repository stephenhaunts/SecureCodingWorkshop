﻿/*
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

namespace SecureCodingWorkshop.HybridWithIntegrityAndSignatureGCM_;

public static class AesGCMEncryption
{
    public static (byte[], byte[]) Encrypt(byte[] dataToEncrypt, byte[] key, byte[] nonce, byte[] associatedData)
    {
        // these will be filled during the encryption
        var tag = new byte[16];
        var ciphertext = new byte[dataToEncrypt.Length];

        using AesGcm aesGcm = new AesGcm(key);
        aesGcm.Encrypt(nonce, dataToEncrypt, ciphertext, tag, associatedData);

        return (ciphertext, tag);
    }

    public static byte[] Decrypt(byte[] cipherText, byte[] key, byte[] nonce, byte[] tag, byte[] associatedData)
    {
        var decryptedData = new byte[cipherText.Length];

        using AesGcm aesGcm = new AesGcm(key);
        aesGcm.Decrypt(nonce, cipherText, tag, decryptedData, associatedData);

        return decryptedData;
    }
}
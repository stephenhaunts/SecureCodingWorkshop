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
using System;
using System.Text;
using BlockChainCourse.Cryptography;

namespace BlockChainCourse.BlockWithProofOfWork
{
    public class KeyStore : IKeyStore
    {
        private DigitalSignature DigitalSignature { get; set; }
        public byte[] AuthenticatedHashKey { get; private set; }

        public KeyStore(byte[] authenticatedHashKey)
        {
            AuthenticatedHashKey = authenticatedHashKey;
            DigitalSignature = new DigitalSignature();
            DigitalSignature.AssignNewKey();
        }

        public string SignBlock(string blockHash)
        {
            return Convert.ToBase64String(DigitalSignature.SignData(Convert.FromBase64String(blockHash)));
        }

        public bool VerifyBlock(string blockHash, string signature)
        {
            return DigitalSignature.VerifySignature(Convert.FromBase64String(blockHash), Convert.FromBase64String(signature));  
        }
    }
}

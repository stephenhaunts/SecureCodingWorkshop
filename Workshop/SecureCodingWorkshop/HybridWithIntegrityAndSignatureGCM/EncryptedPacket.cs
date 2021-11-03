﻿namespace SecureCodingWorkshop.HybridWithIntegrityAndSignature
{
    public class EncryptedPacket
    {
        public byte[] EncryptedSessionKey;
        public byte[] EncryptedData;
        public byte[] Iv;
        public byte[] Tag;
        public byte[] Hmac;
        public byte[] Signature;
    }
}
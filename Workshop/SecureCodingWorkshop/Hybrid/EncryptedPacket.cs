using System;

namespace SecureCodingWorkshop.Hybrid
{
    public class EncryptedPacket
    {
        public byte[] EncryptedSessionKey;
        public byte[] EncryptedData;
        public byte[] Iv;
    }
}

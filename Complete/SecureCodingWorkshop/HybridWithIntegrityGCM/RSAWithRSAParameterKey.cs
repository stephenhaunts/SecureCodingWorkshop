using System.Security.Cryptography;

namespace SecureCodingWorkshop.HybridWithIntegrityAndSignature;

public class RSAWithRSAParameterKey
{
    private RSAParameters _publicKey;
    private RSAParameters _privateKey;

    public void AssignNewKey()
    {
        using var rsa = new RSACryptoServiceProvider(2048);
        rsa.PersistKeyInCsp = false;
        _publicKey = rsa.ExportParameters(false);
        _privateKey = rsa.ExportParameters(true);
    }

    public byte[] EncryptData(byte[] dataToEncrypt)
    {
        using var rsa = new RSACryptoServiceProvider();
        rsa.PersistKeyInCsp = false;
        rsa.ImportParameters(_publicKey);

        var cipherbytes = rsa.Encrypt(dataToEncrypt, true);

        return cipherbytes;
    }

    public byte[] DecryptData(byte[] dataToEncrypt)
    {
        using var rsa = new RSACryptoServiceProvider();
        rsa.PersistKeyInCsp = false;

        rsa.ImportParameters(_privateKey);
        var plain = rsa.Decrypt(dataToEncrypt, true);

        return plain;
    }
}
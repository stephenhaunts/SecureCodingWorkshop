namespace SecureCodingWorkshop.HybridWithIntegrityGCM_;

public class RSAWithRSAParameterKey
{
    private RSAParameters _publicKey;
    private RSAParameters _privateKey;

    public void AssignNewKey()
    {
        using var rsa = RSA.Create(2048);
        _publicKey = rsa.ExportParameters(false);
        _privateKey = rsa.ExportParameters(true);
    }

    public byte[] EncryptData(byte[] dataToEncrypt)
    {
        using var rsa = RSA.Create(2048);
        rsa.ImportParameters(_publicKey);

        var cipherbytes = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);

        return cipherbytes;
    }

    public byte[] DecryptData(byte[] dataToEncrypt)
    {
        using var rsa = RSA.Create(2048);

        rsa.ImportParameters(_privateKey);
        var plain = rsa.Decrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);

        return plain;
    }
}
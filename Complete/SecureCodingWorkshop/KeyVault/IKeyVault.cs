using System.Threading.Tasks;

namespace SecureCodingWorkshop.KeyVault
{
    public interface IKeyVault
    {
        Task<string> CreateKeyAsync(string keyName);
        Task DeleteKeyAsync(string keyName);
        Task<byte[]> EncryptAsync(string keyId, byte[] dataToEncrypt);
        Task<byte[]> DecryptAsync(string keyId, byte[] dataToDecrypt);
		Task<byte[]> WrapSymmetricKeyAsync(string keyId, byte[] symmetricKey);
		Task<byte[]> UnwrapSymmetricKeyAsync(string keyId, byte[] wrappedSymmetricKey);
        Task<string> SetSecretAsync(string secretName, string secretValue);
        Task<string> GetSecretAsync(string secretName);
    }
}

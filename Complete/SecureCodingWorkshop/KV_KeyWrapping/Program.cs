using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureKeyVault.KeyWrapping
{
    static class Program
    {
        public static async Task Main()
        {
            await KeyVault();
        }

        private static async Task KeyVault()
        {
            IKeyVault vault = new KeyVault();

            const string MY_KEY_NAME = "StephenHauntsKey";
            var keyId = await vault.CreateKeyAsync(MY_KEY_NAME);

            var localKey = SecureRandom.GenerateRandomNumber(32);

            // Encrypt our local key with Key Vault and Store it in the database
            var encryptedKey = await vault.EncryptAsync(keyId, localKey);


            // Get our encrypted key from the database and decrypt it with the Key Vault.
            var decryptedKey = await vault.DecryptAsync(keyId, encryptedKey);

            // Now we have recovered the key with the Key Vault we can encrypt with AES locally.
            var iv = SecureRandom.GenerateRandomNumber(16);
            var encryptedData = AesEncryption.Encrypt(Encoding.ASCII.GetBytes("MEGA TOP SECRET STUFF"), decryptedKey, iv);
            var decryptedMessage = AesEncryption.Decrypt(encryptedData, decryptedKey, iv);

            var encryptedText = Convert.ToBase64String(encryptedData);
            var decryptedData = Encoding.UTF8.GetString(decryptedMessage);

            // Remove HSM backed key
            await vault.DeleteKeyAsync(MY_KEY_NAME);
            Console.WriteLine("Key Deleted : " + keyId);
        }
    }
}

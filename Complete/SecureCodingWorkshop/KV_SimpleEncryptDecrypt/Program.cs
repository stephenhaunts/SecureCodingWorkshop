using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureKeyVault.SimpleEncryptDecrypt
{
    static class Program
    {
        static void Main()
        {
            KeyVault().GetAwaiter().GetResult();
        }

        private static async Task KeyVault()
        {
            IKeyVault vault = new KeyVault();

            const string MY_KEY_NAME = "MyKeyVaultKey";

            var keyId = await vault.CreateKeyAsync(MY_KEY_NAME);
            Console.WriteLine("Key Written : " + keyId);

            // Test encryption and decryption.
            var dataToEncrypt = "Hello World!!";

            var encrypted = await vault.EncryptAsync(keyId, Encoding.ASCII.GetBytes(dataToEncrypt));
            var decrypted = await vault.DecryptAsync(keyId, encrypted);

            var encryptedText = Convert.ToBase64String(encrypted);
            var decryptedData = Encoding.UTF8.GetString(decrypted);

            Console.WriteLine("Encrypted Data : " + encryptedText);
            Console.WriteLine("Decrypted Data : " + decryptedData);

            // Remove HSM backed key
            await vault.DeleteKeyAsync(MY_KEY_NAME);
            Console.WriteLine("Key Deleted : " + keyId);
        }
    }
}

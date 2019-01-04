using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureKeyVault.DigitalSignatures
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            await KeyVault();
        }

        private static async Task KeyVault()
        {
            var vault = new KeyVault();

            const string MY_KEY_NAME = "StephenHauntsKey";
            var keyId = await vault.CreateKeyAsync(MY_KEY_NAME);


            const string importantDocument = "This is a really important document that I need to digitally sign.";

            var documentDigest = Hash.Sha256(Encoding.UTF8.GetBytes(importantDocument));

            var signature = await vault.Sign(keyId, documentDigest);

            var verified = await vault.Verify(keyId, documentDigest, signature);

            // Remove HSM backed key
            await vault.DeleteKeyAsync(MY_KEY_NAME);
            Console.WriteLine("Key Deleted : " + keyId);
        }
    }
}

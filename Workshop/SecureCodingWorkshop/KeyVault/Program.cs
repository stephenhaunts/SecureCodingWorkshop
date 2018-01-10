using System;
using System.Text;
using System.Threading.Tasks;

namespace SecureCodingWorkshop.KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyVault().GetAwaiter().GetResult();
        }

        public static async Task KeyVault()
        {
            // Todo : Instance the KeyVault.
            // Todo : Write a secret to the vault.
            // Todo : Retrieve the secret from the vault.

            // Todo : Create RSA Key in the vault.
            // Todo : Encrypt a message using the key id of the above key.
            // Todo : Remove the key from the vault.
            await Task.CompletedTask;
        }
    }
}

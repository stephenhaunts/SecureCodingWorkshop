using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureKeyVault.Secrets
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

            const string MY_SECRET = "StephenHauntsSecret";

            var secretId = await vault.SetSecretAsync(MY_SECRET, "Mary had a little lamb.");
            Console.WriteLine("Secret Written");

            var secret = await vault.GetSecretAsync(MY_SECRET);
            Console.WriteLine("Secret Retrieved : " + secret);
        }
    }
}

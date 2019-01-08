using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AzureKeyVault.HybridWithIntegrityAndSignature
{
    static class Program
    {
        public static async Task Main()
        {
            await KeyVault();
        }

        private static async Task KeyVault()
        {
            // TODO : Implement the methods in HybridEncryption.cs to use the same techniques as earlier, but the
            // TODO : RSA encryption and digital signatures are performed using the Key Vault instead.
        }
    }
}

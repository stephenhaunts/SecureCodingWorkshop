using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureCodingWorkshop.HybridWithIntegrityAndSignature
{
    static class Program
    {
        private static void Main()
        {
            const string original = "Very secret and important information that can not fall into the wrong hands.";

            var hybrid = new HybridEncryption();

            var rsaParams = new RSAWithRSAParameterKey();
            rsaParams.AssignNewKey();

            
            Console.WriteLine("Hybrid Encryption (AES-GCM) with Integrity Check in .NET");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine();

            try
            {
                var encryptedBlock = hybrid.EncryptData(Encoding.UTF8.GetBytes(original), rsaParams);

                var decrpyted = hybrid.DecryptData(encryptedBlock, rsaParams);

                Console.WriteLine("Original Message = " + original);
                Console.WriteLine();
                Console.WriteLine("Message After Decryption = " + Encoding.UTF8.GetString(decrpyted));
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}

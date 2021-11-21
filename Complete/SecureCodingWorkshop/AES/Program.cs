using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureCodingWorkshop.AES
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            TestAesGCM();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            TestAesCBC();

            Console.ReadLine();
        }

        private static void TestAesCBC()
        {
            const string original = "Text to encrypt";
            var aes = new AesEncryption();
            var key = aes.GenerateRandomNumber(32);
            var iv = aes.GenerateRandomNumber(16);


            var encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
            var decrypted = aes.Decrypt(encrypted, key, iv);

            var decryptedMessage = Encoding.UTF8.GetString(decrypted);

            Console.WriteLine("AES Encryption Demonstration in .NET");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);
        }

        private static void TestAesGCM()
        {
            const string original = "Text to encrypt";

            var aesGCM = new AesGcmEncryption();

            var gcmKey = aesGCM.GenerateRandomNumber(32);
            var nonce = aesGCM.GenerateRandomNumber(12);

            try
            {
                (byte[] ciphereText, byte[] tag) result = AesGcmEncryption.Encrypt(Encoding.UTF8.GetBytes(original), gcmKey, nonce, Encoding.UTF8.GetBytes("some metadata"));
                byte[] decryptedText = AesGcmEncryption.Decrypt(result.ciphereText, gcmKey, nonce, result.tag, Encoding.UTF8.GetBytes("some metadata"));

                Console.WriteLine("AES GCM Encryption Demonstration in .NET");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Original Text = " + original);
                Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(result.ciphereText));
                Console.WriteLine("Decrypted Text = " + Encoding.UTF8.GetString(decryptedText));
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

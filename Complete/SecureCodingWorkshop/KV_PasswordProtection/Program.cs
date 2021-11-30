/*
MIT License

Copyright (c) 2021

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace SecureCodingWorkshop.PasswordProtection_;

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
        const string ITERATIONS_VALUE = "PBKDF2Iterations";

        var keyId = await vault.CreateKeyAsync(MY_KEY_NAME);

        // Encrypt our salt with Key Vault and Store it in the database
        var salt = SecureRandom.GenerateRandomNumber(32);
        var encryptedSalt = await vault.EncryptAsync(keyId, salt);
        var iterationsId = await vault.SetSecretAsync(ITERATIONS_VALUE, "20000");

        // Get our encrypted salt from the database and decrypt it with the Key Vault.
        var decryptedSalt = await vault.DecryptAsync(keyId, encryptedSalt);
        var iterations = int.Parse(await vault.GetSecretAsync(ITERATIONS_VALUE));

        // Hash our password with a PBKDF2
        var password = "Pa55w0rd";

        var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), decryptedSalt, iterations);
        Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));

        // Remove HSM backed key
        await vault.DeleteKeyAsync(MY_KEY_NAME);

        Console.WriteLine("Key Deleted : " + keyId);
    }
}
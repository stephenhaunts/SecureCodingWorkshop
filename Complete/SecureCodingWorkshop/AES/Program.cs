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

using AES;

TestAesGCM();

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

TestAesCBC();

Console.ReadLine();


static void TestAesCBC()
{
    const string original = "Text to encrypt";
    var key = RandomNumberGenerator.GetBytes(32);
    var iv =  RandomNumberGenerator.GetBytes(16);


    var encrypted = AesEncryption.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
    var decrypted = AesEncryption.Decrypt(encrypted, key, iv);

    var decryptedMessage = Encoding.UTF8.GetString(decrypted);

    Console.WriteLine("AES Encryption Demonstration in .NET");
    Console.WriteLine("------------------------------------");
    Console.WriteLine();
    Console.WriteLine("Original Text = " + original);
    Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
    Console.WriteLine("Decrypted Text = " + decryptedMessage);
}

static void TestAesGCM()
{
    const string original = "Text to encrypt";

    var gcmKey = RandomNumberGenerator.GetBytes(32);
    var nonce =  RandomNumberGenerator.GetBytes(12);

    try
    {
        (byte[] ciphereText, byte[] tag) result = AesGcmEncryption.Encrypt(Encoding.UTF8.GetBytes(original), gcmKey,
            nonce, Encoding.UTF8.GetBytes("some metadata"));
        byte[] decryptedText = AesGcmEncryption.Decrypt(result.ciphereText, gcmKey, nonce, result.tag,
            Encoding.UTF8.GetBytes("some metadata"));

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
    

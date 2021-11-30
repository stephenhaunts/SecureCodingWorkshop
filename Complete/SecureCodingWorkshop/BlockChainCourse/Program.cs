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

namespace SecureCodingWorkshop.Cryptography_;

class Program
{
    static void Main(string[] args)
    {
        const string originalMessage = "Original Message to hash";
        const string originalMessage2 = "Or1ginal Message to hash";

        SHA256(originalMessage, originalMessage2);
        HMAC(originalMessage, originalMessage2);
        DigitalSignatures();
    }

    private static void SHA256(string originalMessage, string originalMessage2)
    {
        Console.WriteLine("Hashing Demonstration in .NET");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
        Console.WriteLine("Original Message 1 : " + originalMessage);
        Console.WriteLine("Original Message 2 : " + originalMessage2);
        Console.WriteLine();

        var sha256HashedMessage = HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(originalMessage));
        var sha256HashedMessage2 = HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(originalMessage2));

        Console.WriteLine("SHA 256 Hashes");
        Console.WriteLine();
        Console.WriteLine("Message 1 hash = " + Convert.ToBase64String(sha256HashedMessage));
        Console.WriteLine("Message 2 hash = " + Convert.ToBase64String(sha256HashedMessage2));
        Console.WriteLine();
    }

    private static void HMAC(string originalMessage, string originalMessage2)
    {
        Console.WriteLine("HMAC Demonstration in .NET");
        Console.WriteLine("--------------------------");
        Console.WriteLine();

        var key = RandomNumberGenerator.GetBytes(32);

        var hmacSha256Message = Hmac.ComputeHmacsha256(Encoding.UTF8.GetBytes(originalMessage), key);
        var hmacSha256Message2 = Hmac.ComputeHmacsha256(Encoding.UTF8.GetBytes(originalMessage2), key);

        Console.WriteLine();
        Console.WriteLine("SHA 256 HMAC");
        Console.WriteLine();
        Console.WriteLine("Message 1 hash = " + Convert.ToBase64String(hmacSha256Message));
        Console.WriteLine("Message 2 hash = " + Convert.ToBase64String(hmacSha256Message2));
        Console.WriteLine();
    }

    private static void DigitalSignatures()
    {
        var document = Encoding.UTF8.GetBytes("Document to Sign");
        byte[] hashedDocument;

        hashedDocument = HashData.ComputeHashSha256(document);

        var digitalSignature = new DigitalSignature();
        digitalSignature.AssignNewKey();

        var signature = digitalSignature.SignData(hashedDocument);
        var verified = digitalSignature.VerifySignature(hashedDocument, signature);

        Console.WriteLine("Digital Signature Demonstration in .NET");
        Console.WriteLine("---------------------------------------");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("   Original Text = " +
                          Encoding.Default.GetString(document));

        Console.WriteLine();
        Console.WriteLine("   Digital Signature = " +
                          Convert.ToBase64String(signature));

        Console.WriteLine();

        Console.WriteLine(verified
            ? "The digital signature has been correctly verified."
            : "The digital signature has NOT been correctly verified.");
    }
}
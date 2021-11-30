﻿/*
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

namespace SecureCodingWorkshop.DigitalSignature_;

static class Program
{
    static void Main()
    {
        SignAndVerifyData();

        SignAndVerifyData2();

        SignAndVerifyDataWithKeyExport();

        Console.ReadLine();
    }

    private static void SignAndVerifyData()
    {
        var document = Encoding.UTF8.GetBytes("Document to Sign");
        byte[] hashedDocument;

        using (var sha256 = SHA256.Create())
        {
            hashedDocument = sha256.ComputeHash(document);
        }

        var digitalSignature = new DigitalSignature();
        digitalSignature.AssignNewKey();

        var signature = digitalSignature.SignData(hashedDocument);
        var verified = digitalSignature.VerifySignature(hashedDocument, signature);

        Console.WriteLine("Digital Signature Demonstration in .NET");
        Console.WriteLine("---------------------------------------");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("   Original Text = " + Encoding.Default.GetString(document));

        Console.WriteLine();
        Console.WriteLine("   Digital Signature = " + Convert.ToBase64String(signature));

        Console.WriteLine();

        Console.WriteLine(verified
            ? "The digital signature has been correctly verified."
            : "The digital signature has NOT been correctly verified.");
    }

    private static void SignAndVerifyData2()
    {
        var document = Encoding.UTF8.GetBytes("Document to Sign");

        var digitalSignature = new NewDigitalSignature();

        var signature = digitalSignature.SignData(document);

        var valid = digitalSignature.VerifySignature(signature.Item1, signature.Item2);

        Console.WriteLine(valid ? "The digital signature is VALID" : "The digital signature is INVALID");
    }

    private static void SignAndVerifyDataWithKeyExport()
    {
        // Create some RSA keys and export them.
        var digitalSignature = new NewDigitalSignature();
        var encryptedPrivateKey = digitalSignature.ExportPrivateKey(100000, "iwf57yn783425y");
        var publicKey = digitalSignature.ExportPublicKey();


        var document = Encoding.UTF8.GetBytes("Document to Sign");

        // Import our existing keys
        var digitalSignature2 = new NewDigitalSignature();
        digitalSignature2.ImportPublicKey(publicKey);
        digitalSignature2.ImportEncryptedPrivateKey(encryptedPrivateKey, "iwf57yn783425y");

        var signature = digitalSignature2.SignData(document);

        var valid = digitalSignature2.VerifySignature(signature.Item1, signature.Item2);

        Console.WriteLine(valid ? "The digital signature is VALID" : "The digital signature is INVALID");
    }
}
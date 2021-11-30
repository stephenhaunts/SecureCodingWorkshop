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

using System.Diagnostics;
using SecureCodingWorkshop.PBKDF2_;

const string passwordToHash = "VeryComplexPassword";

Console.WriteLine("Password Based Key Derivation Function Demonstration in .NET");
Console.WriteLine("------------------------------------------------------------");
Console.WriteLine();
Console.WriteLine("PBKDF2 Hashes");
Console.WriteLine();

HashPassword(passwordToHash, 100);
HashPassword(passwordToHash, 1000);
HashPassword(passwordToHash, 10000);
HashPassword(passwordToHash, 50000);
HashPassword(passwordToHash, 100000);
HashPassword(passwordToHash, 200000);
HashPassword(passwordToHash, 500000);

Console.ReadLine();


static void HashPassword(string passwordToHash, int numberOfRounds)
{
    var sw = Stopwatch.StartNew();

    var hashedPassword = Pbkdf2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash),
        RandomNumberGenerator.GetBytes(32),
        numberOfRounds);

    sw.Stop();

    Console.WriteLine();
    Console.WriteLine("Password to hash : " + passwordToHash);
    Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
    Console.WriteLine("Iterations <" + numberOfRounds + "> Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
}
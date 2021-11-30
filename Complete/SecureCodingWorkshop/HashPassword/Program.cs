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

using SecureCodingWorkshop.HashPassword_;

const string password = "V3ryC0mpl3xP455w0rd";
var salt = RandomNumberGenerator.GetBytes(32);

Console.WriteLine("Hash Password with Salt Demonstration in .NET");
Console.WriteLine("---------------------------------------------");
Console.WriteLine();
Console.WriteLine("Password : " + password);
Console.WriteLine("Salt = " + Convert.ToBase64String(salt));
Console.WriteLine();

var hashedPassword1 = Hash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);

Console.WriteLine();
Console.WriteLine("Hashed Password = " + Convert.ToBase64String(hashedPassword1));
Console.WriteLine();

Console.ReadLine();

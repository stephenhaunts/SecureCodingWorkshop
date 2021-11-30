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
using SecureCodingWorkshop.BlockWithSingleTransaction_;
using SecureCodingWorkshop.BlockWithSingleTransaction_.Interfaces;

// Single block with a single transaction, in an immutable chain.
// Standard SHA-256 Hashing

BlockChain chain = new BlockChain();
IBlock block1 = new Block(0, "ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss, null);
IBlock block2 = new Block(1, "VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss, block1);
IBlock block3 = new Block(2, "XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss, block2);
IBlock block4 = new Block(3, "CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss, block3);
IBlock block5 = new Block(4, "AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss, block4);
IBlock block6 = new Block(5, "QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss, block5);
IBlock block7 = new Block(6, "CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss, block6);
IBlock block8 = new Block(7, "PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss, block7);

chain.AcceptBlock(block1);
chain.AcceptBlock(block2);
chain.AcceptBlock(block3);
chain.AcceptBlock(block4);
chain.AcceptBlock(block5);
chain.AcceptBlock(block6);
chain.AcceptBlock(block7);
chain.AcceptBlock(block8);

chain.VerifyChain();

Console.WriteLine("");
Console.WriteLine("");

block4.CreatedDate = new DateTime(2017, 09, 20);

chain.VerifyChain();

Console.WriteLine();

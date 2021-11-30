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

using SecureCodingWorkshop.BlockWithMultipleTransactions_;
using SecureCodingWorkshop.BlockWithMultipleTransactions_.Interfaces;


// Single block with a multiple transactions, in an immutable chain.
// Standard SHA-256 Hashing

ITransaction txn1 = new Transaction("ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss);
ITransaction txn2 = new Transaction("VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss);
ITransaction txn3 = new Transaction("XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss);
ITransaction txn4 = new Transaction("CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss);
ITransaction txn5 = new Transaction("AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss);
ITransaction txn6 = new Transaction("QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss);
ITransaction txn7 = new Transaction("CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss);
ITransaction txn8 = new Transaction("PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss);
ITransaction txn9 = new Transaction("ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss);
ITransaction txn10 = new Transaction("VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss);
ITransaction txn11 = new Transaction("XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss);
ITransaction txn12 = new Transaction("CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss);
ITransaction txn13 = new Transaction("AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss);
ITransaction txn14 = new Transaction("QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss);
ITransaction txn15 = new Transaction("CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss);
ITransaction txn16 = new Transaction("PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss);


IBlock block1 = new Block(0);
IBlock block2 = new Block(1);
IBlock block3 = new Block(2);
IBlock block4 = new Block(3);

block1.AddTransaction(txn1);
block1.AddTransaction(txn2);
block1.AddTransaction(txn3);
block1.AddTransaction(txn4);

block2.AddTransaction(txn5);
block2.AddTransaction(txn6);
block2.AddTransaction(txn7);
block2.AddTransaction(txn8);

block3.AddTransaction(txn9);
block3.AddTransaction(txn10);
block3.AddTransaction(txn11);
block3.AddTransaction(txn12);

block4.AddTransaction(txn13);
block4.AddTransaction(txn14);
block4.AddTransaction(txn15);
block4.AddTransaction(txn16);

block1.SetBlockHash(null);
block2.SetBlockHash(block1);
block3.SetBlockHash(block2);
block4.SetBlockHash(block3);


BlockChain chain = new BlockChain();
chain.AcceptBlock(block1);
chain.AcceptBlock(block2);
chain.AcceptBlock(block3);
chain.AcceptBlock(block4);


chain.VerifyChain();

Console.WriteLine("");
Console.WriteLine("");
  
txn5.ClaimNumber = "weqwewe";
chain.VerifyChain();

Console.WriteLine();

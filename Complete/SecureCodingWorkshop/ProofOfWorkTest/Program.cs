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

using SecureCodingWorkshop.ProofOfWorkTest_;

ProofOfWork pow0 = new ProofOfWork("Mary had a little lamb", 0);
ProofOfWork pow1 = new ProofOfWork("Mary had a little lamb", 1);
ProofOfWork pow2 = new ProofOfWork("Mary had a little lamb", 2);
ProofOfWork pow3 = new ProofOfWork("Mary had a little lamb", 3);
ProofOfWork pow4 = new ProofOfWork("Mary had a little lamb", 4);
ProofOfWork pow5 = new ProofOfWork("Mary had a little lamb", 5);
ProofOfWork pow6 = new ProofOfWork("Mary had a little lamb", 6);

pow0.CalculateProofOfWork();
pow1.CalculateProofOfWork();
pow2.CalculateProofOfWork();
pow3.CalculateProofOfWork();
pow4.CalculateProofOfWork();
pow5.CalculateProofOfWork();
pow6.CalculateProofOfWork();
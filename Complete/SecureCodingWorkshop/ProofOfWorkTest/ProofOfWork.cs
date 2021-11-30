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
using SecureCodingWorkshop.Cryptography_;

namespace SecureCodingWorkshop.ProofOfWorkTest_;

public class ProofOfWork
{
    public string MyData { get; private set; }
    public int Difficulty { get; private set; }
    public int Nonce { get; private set; }

    public ProofOfWork(string dataToHash, int difficulty)
    {
        MyData = dataToHash;
        Difficulty = difficulty;
    }

    public string CalculateProofOfWork()
    {
        string difficulty = DifficultyString();
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        while(true)
        {
            var hashedData = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + MyData)));

            if (hashedData.StartsWith(difficulty, StringComparison.Ordinal))
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    
                Console.WriteLine("Difficulty Level " + Difficulty + " - Nonce = " + Nonce + " - Elapsed = " + elapsedTime +  " - " + hashedData);
                return hashedData;
            }

            Nonce++;
        }
    }

    private string DifficultyString()
    {
        string difficultyString = string.Empty;

        for (int i = 0; i < Difficulty; i++ )
        {
            difficultyString += "0";    
        }

        return difficultyString;
    }
}
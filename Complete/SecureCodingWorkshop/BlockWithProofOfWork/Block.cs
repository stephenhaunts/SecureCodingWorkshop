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

using System.Diagnostics;
using SecureCodingWorkshop.BlockWithProofOfWork_.Interfaces;
using SecureCodingWorkshop.BlockWithProofOfWork_.Merkle;
using SecureCodingWorkshop.Cryptography_;

namespace SecureCodingWorkshop.BlockWithProofOfWork_;

public class Block : IBlock
{
    public List<ITransaction> Transaction { get; private set; }

    // Set as part of the block creation process.
    public int BlockNumber { get; private set; }
    public DateTime CreatedDate { get; set; }
    public string BlockHash { get; private set; }
    public string PreviousBlockHash { get; set; }
    public string BlockSignature { get; private set; }
    public int Difficulty { get; private set; }
    public int Nonce { get; private set; }

    public IBlock NextBlock { get; set; }
    private MerkleTree merkleTree = new MerkleTree();
    public IKeyStore KeyStore { get; private set; }

    public Block(int blockNumber, int miningDifficulty)
    {
        BlockNumber = blockNumber;

        CreatedDate = DateTime.UtcNow;
        Transaction = new List<ITransaction>();
        Difficulty = miningDifficulty;
    }

    public Block(int blockNumber, IKeyStore keystore, int miningDifficulty)
    {
        BlockNumber = blockNumber;

        CreatedDate = DateTime.UtcNow;
        Transaction = new List<ITransaction>();
        KeyStore = keystore;
        Difficulty = miningDifficulty;
    }

    public void AddTransaction(ITransaction transaction)
    {
        Transaction.Add(transaction);
    }

    public string CalculateBlockHash(string previousBlockHash)
    {
        string blockheader = BlockNumber + CreatedDate.ToString() + previousBlockHash;
        string combined = merkleTree.RootNode + blockheader;

        string completeBlockHash;

        if (KeyStore == null)
        {
            completeBlockHash = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
        }
        else
        {
            completeBlockHash = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
        }

        return completeBlockHash;
    }

    // Set the block hash
    public void SetBlockHash(IBlock parent)
    {
        if (parent != null)
        {
            PreviousBlockHash = parent.BlockHash;
            parent.NextBlock = this;
        }
        else
        {
            // Previous block is the genesis block.
            PreviousBlockHash = null;
        }

        BuildMerkleTree();

        //BlockHash = CalculateBlockHash(PreviousBlockHash);

        BlockHash = CalculateProofOfWork(CalculateBlockHash(PreviousBlockHash));

        if (KeyStore != null)
        {
            BlockSignature = KeyStore.SignBlock(BlockHash);
        }
    }

    private void BuildMerkleTree()
    {
        merkleTree = new MerkleTree();

        foreach (ITransaction txn in Transaction)
        {
            merkleTree.AppendLeaf(MerkleHash.Create(txn.CalculateTransactionHash()));
        }

        merkleTree.BuildTree();
    }

    public string CalculateProofOfWork(string blockHash)
    {
        string difficulty = DifficultyString();
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        while (true)
        {
            string hashedData = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + blockHash)));

            if (hashedData.StartsWith(difficulty, StringComparison.Ordinal))
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

                Console.WriteLine("Difficulty Level " + Difficulty + " - Nonce = " + Nonce + " - Elapsed = " + elapsedTime + " - " + hashedData);
                return hashedData;
            }

            Nonce++;
        }
    }

    private string DifficultyString()
    {
        string difficultyString = string.Empty;

        for (int i = 0; i < Difficulty; i++)
        {
            difficultyString += "0";
        }

        return difficultyString;
    }

    public bool IsValidChain(string prevBlockHash, bool verbose)
    {
        bool isValid = true;
        bool validSignature = false;

        BuildMerkleTree();

        validSignature = KeyStore.VerifyBlock(BlockHash, BlockSignature);

        // Is this a valid block and transaction
        //string newBlockHash = CalculateBlockHash(prevBlockHash);
        string newBlockHash = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + CalculateBlockHash(prevBlockHash))));

        validSignature = KeyStore.VerifyBlock(newBlockHash, BlockSignature);

        if (newBlockHash != BlockHash)
        {
            isValid = false;
        }
        else
        {
            // Does the previous block hash match the latest previous block hash
            isValid |= PreviousBlockHash == prevBlockHash;
        }

        PrintVerificationMessage(verbose, isValid, validSignature);

        // Check the next block by passing in our newly calculated blockhash. This will be compared to the previous
        // hash in the next block. They should match for the chain to be valid.
        if (NextBlock != null)
        {
            return NextBlock.IsValidChain(newBlockHash, verbose);
        }

        return isValid;
    }

    private void PrintVerificationMessage(bool verbose, bool isValid, bool validSignature)
    {
        if (verbose)
        {
            if (!isValid)
            {
                Console.WriteLine("Block Number " + BlockNumber + " : FAILED VERIFICATION");
            }
            else
            {
                Console.WriteLine("Block Number " + BlockNumber + " : PASS VERIFICATION");
            }

            if (!validSignature)
            {
                Console.WriteLine("Block Number " + BlockNumber + " : Invalid Digital Signature");
            }
        }
    }
}
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
using System;
using System.Collections.Generic;
using System.Text;
using BlockChainCourse.BlockWithMultipleTransactions.Interfaces;
using BlockChainCourse.Cryptography;
using Clifton.Blockchain;

namespace BlockChainCourse.BlockWithMultipleTransactions;

public class Block : IBlock
{
    public List<ITransaction> Transaction { get; private set; }

    // Set as part of the block creation process.
    public int BlockNumber { get; private set; }
    public DateTime CreatedDate { get; set; }
    public string BlockHash { get; private set; }
    public string PreviousBlockHash { get; set; }
    public IBlock NextBlock { get; set; }
    private MerkleTree merkleTree = new MerkleTree();

    public Block(int blockNumber)
    {
        BlockNumber = blockNumber;

        CreatedDate = DateTime.UtcNow;
        Transaction = new List<ITransaction>();
    }

    public void AddTransaction(ITransaction transaction)
    {
        Transaction.Add(transaction);
    }

    public string CalculateBlockHash(string previousBlockHash)
    {
        string blockheader = BlockNumber + CreatedDate.ToString() + previousBlockHash;
        string combined = merkleTree.RootNode + blockheader;

        return Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
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

        BlockHash = CalculateBlockHash(PreviousBlockHash);
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

    public bool IsValidChain(string prevBlockHash, bool verbose)
    {
        bool isValid = true;

        BuildMerkleTree();

        // Is this a valid block and transaction
        string newBlockHash = CalculateBlockHash(prevBlockHash);
        if (newBlockHash != BlockHash)
        {
            isValid = false;
        }
        else
        {
            // Does the previous block hash match the latest previous block hash
            isValid |= PreviousBlockHash == prevBlockHash;
        }

        PrintVerificationMessage(verbose, isValid);

        // Check the next block by passing in our newly calculated blockhash. This will be compared to the previous
        // hash in the next block. They should match for the chain to be valid.
        if (NextBlock != null)
        {
            return NextBlock.IsValidChain(newBlockHash, verbose);
        }

        return isValid;
    }

    private void PrintVerificationMessage(bool verbose, bool isValid)
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
        }
    }
}
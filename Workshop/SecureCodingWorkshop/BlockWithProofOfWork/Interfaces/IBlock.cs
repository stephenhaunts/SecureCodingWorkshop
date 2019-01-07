using System;
using System.Collections.Generic;

namespace BlockChainCourse.BlockWithProofOfWork
{
    public interface IBlock
    {
        // List of transactions
        List<ITransaction> Transaction { get; }

        // Block header data
        int BlockNumber { get; }
        DateTime CreatedDate { get; set; }
        string BlockHash { get; }
        string PreviousBlockHash { get; set; }
        string BlockSignature { get; }
        int Difficulty { get; }
        int Nonce { get; }

        void AddTransaction(ITransaction transaction);
        string CalculateBlockHash(string previousBlockHash);
        void SetBlockHash(IBlock parent);
        IBlock NextBlock { get; set; }
        bool IsValidChain(string prevBlockHash, bool verbose);
        IKeyStore KeyStore { get; }
    }
}

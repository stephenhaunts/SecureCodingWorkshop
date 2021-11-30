﻿namespace SecureCodingWorkshop.BlockWithMultipleTransactions_.Interfaces;

public interface IBlockChain
{
    void AcceptBlock(IBlock block);
    int NextBlockNumber { get; }
    void VerifyChain();
}
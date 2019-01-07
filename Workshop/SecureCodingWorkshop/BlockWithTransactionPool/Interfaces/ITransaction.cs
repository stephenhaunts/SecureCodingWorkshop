using System;

namespace BlockChainCourse.BlockWithTransactionPool
{
    public interface ITransaction
    {
        string ClaimNumber { get; set; }
        decimal SettlementAmount { get; set; }
        DateTime SettlementDate { get; set; }
        string CarRegistration { get; set; }
        int Mileage { get; set; }
        ClaimType ClaimType { get; set; }

        string CalculateTransactionHash();
    }
}

using System.Security.Cryptography;

namespace BlockChainCourse.BlockWithTransactionPool
{
    public interface IKeyStore
    {
        byte[] AuthenticatedHashKey { get; }
        string SignBlock(string blockHash);
        bool VerifyBlock(string blockHash, string signature);
    }
}

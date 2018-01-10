namespace BlockChainCourse.BlockWithSingleTransaction.Interfaces
{
    public interface IBlockChain
    {
        void AcceptBlock(IBlock block);
        void VerifyChain();
    }
}

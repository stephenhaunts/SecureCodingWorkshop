namespace SecureCodingWorkshop.BlockWithSingleTransaction_.Interfaces;

public interface IBlockChain
{
    void AcceptBlock(IBlock block);
    void VerifyChain();
}
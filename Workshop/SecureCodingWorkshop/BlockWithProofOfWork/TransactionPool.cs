using System.Collections.Generic;

namespace BlockChainCourse.BlockWithProofOfWork
{
    public class TransactionPool
    {
        private readonly Queue<ITransaction> _queue;

        public TransactionPool()
        {
            _queue = new Queue<ITransaction>();
        }

        public void AddTransaction(ITransaction transaction)
        {
            _queue.Enqueue(transaction);
        }

        public ITransaction GetTransaction()
        {
            return _queue.Dequeue();
        }
    }
}

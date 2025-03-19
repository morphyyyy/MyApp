using API.Entities;

namespace API.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
    }
}

using API.Entities;
using Models.DTOs;

namespace API.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> List();
        Task<Transaction> Create(Transaction transaction);
    }
}

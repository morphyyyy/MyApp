using Models.DTOs;

namespace FrontEnd.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO>> GetTransactions();
        //Task InsertTransaction(int Id);
    }
}

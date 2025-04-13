using Models.DTOs;

namespace FrontEnd.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO>> List();
        Task<TransactionDTO> Create(TransactionDTO transactionDTO);
        Task<TransactionDTO> Update(TransactionDTO transactionDTO);
        Task<int> Delete(int id);
    }
}

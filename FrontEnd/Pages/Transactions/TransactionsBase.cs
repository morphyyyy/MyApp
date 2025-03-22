using FrontEnd.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models.DTOs;

namespace FrontEnd.Pages.Transactions
{
    public class TransactionsBase : ComponentBase
    {
        [Inject]
        ITransactionService TransactionService { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
        public TransactionDTO newTransactionDTO = new TransactionDTO { Date = DateTime.UtcNow };

        protected override async Task OnInitializedAsync()
        {
            Transactions = await TransactionService.List();
        }

        public async void Create(TransactionDTO transactionDTO)
        {
            newTransactionDTO.TypeId = newTransactionDTO.Amount > 0 ? 1 : 2;
            await TransactionService.Create(transactionDTO);
            Transactions = await TransactionService.List();
            newTransactionDTO = new TransactionDTO { Date = DateTime.UtcNow };
        }

        public async void Delete(int Id)
        {
            await TransactionService.Delete(Id);
        }

    }
}

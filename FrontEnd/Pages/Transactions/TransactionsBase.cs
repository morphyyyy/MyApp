using BlazorBootstrap;
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
            Transactions = (await TransactionService.List()).OrderByDescending(t => t.Date).ToList();
        }

        public async void Create(TransactionDTO transactionDTO)
        {
            newTransactionDTO.Balance = Transactions.Where(t => t.Date <= newTransactionDTO.Date).First().Balance + newTransactionDTO.Amount;
            await TransactionService.Create(transactionDTO);
            Transactions = (await TransactionService.List()).OrderByDescending(t => t.Date).ToList();
            newTransactionDTO = new TransactionDTO { Date = DateTime.UtcNow };
        }

        public async void Delete(int Id)
        {
            await TransactionService.Delete(Id);
            Transactions = (await TransactionService.List()).OrderByDescending(t => t.Date).ToList();
        }

    }
}

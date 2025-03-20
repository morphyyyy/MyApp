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

        protected override async Task OnInitializedAsync()
        {
            Transactions = await TransactionService.GetTransactions();
        }

        public async void InsertTransaction(int Id)
        {
            Console.WriteLine($"Insert Transaction: {Id}");
        }

        public async void DeleteTransaction(int Id)
        {
            Console.WriteLine($"Delete Transaction: {Id}");
        }

    }
}

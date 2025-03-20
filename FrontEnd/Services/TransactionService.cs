using FrontEnd.Services.Contracts;
using Models.DTOs;
using System.Net.Http.Json;

namespace FrontEnd.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<List<TransactionDTO>> GetTransactions()
        {
            var response = await _httpClient.GetAsync($"/api/Transaction");

            return await response.Content.ReadFromJsonAsync<List<TransactionDTO>>();
        }

        //public async Task InsertTransaction(int Id)
        //{
        //    var response = await _httpClient.PostAsJsonAsync($"/api/Transaction", new { Id });

        //    return await response.Content.ReadFromJsonAsync<List<TransactionDTO>>();
        //}

    }
}

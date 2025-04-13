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

        public async Task<List<TransactionDTO>> List()
        {
            var response = await _httpClient.GetAsync($"/api/Transaction/List");

            return await response.Content.ReadFromJsonAsync<List<TransactionDTO>>();
        }

        public async Task<TransactionDTO> Create(TransactionDTO transactionDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Transaction/Create", transactionDTO);

            return await response.Content.ReadFromJsonAsync<TransactionDTO>();
        }

        public async Task<TransactionDTO> Update(TransactionDTO transactionDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Transaction/Update", transactionDTO);

            return await response.Content.ReadFromJsonAsync<TransactionDTO>();
        }

        public async Task<int> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Transaction/Delete/{id}");

            return await response.Content.ReadFromJsonAsync<int>();
        }

    }
}

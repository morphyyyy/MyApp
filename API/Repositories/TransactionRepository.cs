using API.Data;
using API.Entities;
using API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MyAppDbContext _context;

        public TransactionRepository(MyAppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();

            return transactions;
        }
    }
}

using API.Data;
using API.Entities;
using API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using System.Net.Http;

namespace API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MyAppDbContext _context;

        public TransactionRepository(MyAppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> List()
        {
            var transactions = await _context.Transactions.ToListAsync();

            return transactions;
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> Update(Transaction transaction)
        {
            Transaction? oldData = _context.Transactions.Find(transaction.Id);
            if (oldData != null)
            {
                oldData.Description = transaction.Description;
                oldData.Balance = transaction.Balance;
                oldData.Date = transaction.Date;
                oldData.Amount = transaction.Amount;
                oldData.UpdatedDate = transaction.UpdatedDate;
            }

            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<int> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
                return 0;

            _context.Transactions.Remove(transaction);
            return await _context.SaveChangesAsync();
        }
    }
}

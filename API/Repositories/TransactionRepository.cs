﻿using API.Data;
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

    }
}

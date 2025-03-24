using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }

    }
}

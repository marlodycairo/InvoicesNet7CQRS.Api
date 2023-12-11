using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoicesNet7CQRS.Tests.Context
{
    public class InMemoryDbContext : DbContext, IDbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public int Save()
        {
            return SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }
    }
}

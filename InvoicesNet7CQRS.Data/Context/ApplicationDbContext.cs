using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoicesNet7CQRS.Data.Context
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public int Save()
        {
            return SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return SaveChangesAsync();
        }
    }
}

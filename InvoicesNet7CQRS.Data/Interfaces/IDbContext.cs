using InvoicesNet7CQRS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicesNet7CQRS.Data.Interfaces
{
    public interface IDbContext
    {
        public DbSet<User> Users { get; set; }

        public int Save();
        public Task<int> SaveAsync();
    }
}

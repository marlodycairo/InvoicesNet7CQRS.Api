using InvoicesNet7CQRS.Data.Context;
using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Tests.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvoicesNet7CQRS.Tests.Context
{
    public class TestDbContext : ITestDbContext
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public TestDbContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseTest")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var context = new ApplicationDbContext(_options))
            {
                context.Users.AddAsync(new User { Id = 1, FirstName = "Laura", LastName = "Duque", Email = "lauduque@yahua.net", Username = "laura", Pass = "1234" });

                context.Users.AddAsync(new User { Id = 2, FirstName = "Sergio", LastName = "Suarez", Email = "sergios@live.es", Username = "sergio", Pass = "12345" });

                context.SaveChangesAsync();
            }
        }

        public DbSet<User> Users { get; set; }

        public int Save()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                return context.SaveChanges();
            }
        }

        public Task<int> SaveAsync()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                return context.SaveChangesAsync();
            }
        }
    }
}

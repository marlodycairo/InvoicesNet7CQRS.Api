using Microsoft.EntityFrameworkCore;

namespace InvoicesNet7CQRS.Tests.Context
{
    public static class TestDbContext
    {
        public static DbContextOptions<InMemoryDbContext> Create()
        {

            return new DbContextOptionsBuilder<InMemoryDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseTest")
                .Options;

        }
    }
}

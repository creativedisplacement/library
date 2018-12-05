using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Application.Tests
{
    public class TestBase
    {
        public LibraryDbContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<LibraryDbContext>();
            if (useSqlLite)
            {
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new LibraryDbContext(builder.Options);
            if (useSqlLite)
            {
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
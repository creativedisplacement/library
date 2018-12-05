using Library.Persistence;
using System;

namespace Library.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public LibraryDbContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = LibraryContextFactory.Create();
        }

        public void Dispose()
        {
            LibraryContextFactory.Destroy(Context);
        }
    }
}

using Library.Application.Books.Queries.GetBooks;
using Library.Application.Tests.Infrastructure;
using Library.Common.Books.Queries.GetBooks;
using Library.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Queries
{

    public class GetBooksQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetBooksQueryHandlerTests()
        {
            _context = new QueryTestFixture().Context;
        }

        [Fact]
        public async Task Get_All_Books()
        {
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery(), CancellationToken.None);

            Assert.IsType<GetBooksModel>(result);
            Assert.Equal(_context.Books.Count(), result.Books.Count());
            Assert.Equal(_context.Books.OrderBy(b => b.Title).First().Title, result.Books.OrderBy(b => b.Title).First().Title);
        }

        [Fact]
        public async Task Get_Books_By_Title()
        {
            const string title = "Open";
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery{ Title = title }, CancellationToken.None);

            Assert.IsType<GetBooksModel>(result);
            Assert.Equal(_context.Books.First(b => b.Title == title).Title, result.Books.First().Title);
        }

        [Fact]
        public async Task Get_Books_By_Category()
        {
            var categoryId = _context.Categories.First().Id;
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { CategoryIds = new List<Guid> { categoryId } }, CancellationToken.None);

            Assert.IsType<GetBooksModel>(result);
            Assert.Equal(_context.BookCategories.Where(bc => bc.CategoryId == categoryId).Select(b => b.Book).OrderBy(b => b.Title).First().Title, result.Books.First().Title);
        }

        [Fact]
        public async Task Get_Books_By_Lender()
        {
            var lenderId = _context.Persons.First().Id;
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { LenderId = lenderId }, CancellationToken.None);

            Assert.IsType<GetBooksModel>(result);
            Assert.Equal(_context.Books.First(b => b.Lender.Id == lenderId).Title, result.Books.First().Title);
        }

        [Fact]
        public async Task Get_Books_By_Are_Available()
        {
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { IsAvailable = true }, CancellationToken.None);

            Assert.IsType<GetBooksModel>(result);
            Assert.Equal(_context.Books.First(b => b.IsAvailable).Title, result.Books.First().Title);
        }

        [Fact]
        public async Task Get_Books_By_Are_Not_Available()
        {
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { IsAvailable = false }, CancellationToken.None);

            Assert.IsType<GetBooksModel>(result);
            Assert.Equal(_context.Books.First(b => !b.IsAvailable).Title, result.Books.First().Title);
        }
    }
}
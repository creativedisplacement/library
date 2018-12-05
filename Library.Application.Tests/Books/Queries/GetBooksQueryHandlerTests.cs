using Library.Application.Books.Queries.GetBooks;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GetBooksModel = Library.Application.Books.Queries.GetBooks.GetBooksModel;

namespace Library.Application.Tests.Books.Queries
{
    [TestClass]
    public class GetBooksQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetBooksQueryHandlerTests() : this(new QueryTestFixture())
        {

        }

        public GetBooksQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [TestMethod]
        public async Task Get_All_Books()
        {
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery(), CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBooksModel));
            Assert.AreEqual(_context.Books.Count(), result.Books.Count());
            Assert.AreEqual(_context.Books.OrderBy(b => b.Title).First().Title, result.Books.OrderBy(b => b.Title).First().Title);
        }

        [TestMethod]
        public async Task Get_Books_By_Title()
        {
            const string title = "Open";
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery{ Title = title }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBooksModel));
            Assert.AreEqual(_context.Books.First(b => b.Title == title).Title, result.Books.First().Title);
        }

        [TestMethod]
        public async Task Get_Books_By_Category()
        {
            var categoryId = _context.Categories.First().Id;
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { CategoryIds = new List<Guid> { categoryId } }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBooksModel));
            Assert.AreEqual(_context.BookCategories.Where(bc => bc.CategoryId == categoryId).Select(b => b.Book).OrderBy(b => b.Title).First().Title, result.Books.First().Title);
        }

        [TestMethod]
        public async Task Get_Books_By_Lender()
        {
            var lenderId = _context.Persons.First().Id;
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { LenderId = lenderId }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBooksModel));
            Assert.AreEqual(_context.Books.First(b => b.Lender.Id == lenderId).Title, result.Books.First().Title);
        }

        [TestMethod]
        public async Task Get_Books_By_Are_Available()
        {
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { IsAvailable = true }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBooksModel));
            Assert.AreEqual(_context.Books.First(b => b.IsAvailable).Title, result.Books.First().Title);
        }

        [TestMethod]
        public async Task Get_Books_By_Are_Not_Available()
        {
            var queryHandler = new GetBooksQueryHandler(_context);
            var result = await queryHandler.Handle(new GetBooksQuery { IsAvailable = false }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBooksModel));
            Assert.AreEqual(_context.Books.First(b => !b.IsAvailable).Title, result.Books.First().Title);
        }
    }
}
using Library.Application.Categories.Commands.CreateCategory;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Tests.Categories.Commands
{
    [TestClass]
    public class CreateCategoryCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly CreateCategoryCommandHandler _commandHandler;

        public CreateCategoryCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new CreateCategoryCommandHandler(_context);
        }

        [TestMethod]
        public async Task Create_New_Category()
        {
            var command = new CreateCategoryCommand
            {
                Name = "Test1"
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == command.Name);

            Assert.AreEqual(command.Name, category.Name);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
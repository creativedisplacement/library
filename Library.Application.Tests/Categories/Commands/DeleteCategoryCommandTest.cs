using Library.Application.Categories.Commands.DeleteCategory;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Categories.Commands
{
    [TestClass]
    public class DeleteCategoryCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly DeleteCategoryCommandHandler _commandHandler;

        public DeleteCategoryCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new DeleteCategoryCommandHandler(_context);
        }

        [TestMethod]
        public async Task Delete_Category()
        {
            var command = new DeleteCategoryCommand
            {
                Id = (await _context.Categories.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.IsNull(await _context.Categories.FindAsync(command.Id));
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Categories.Add(new Category("Test1"));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
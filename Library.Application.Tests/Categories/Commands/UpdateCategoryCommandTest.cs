using Library.Application.Categories.Commands.UpdateCategory;
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
    public class UpdateCategoryCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly UpdateCategoryCommandHandler _commandHandler;

        public UpdateCategoryCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new UpdateCategoryCommandHandler(_context);
        }

        [TestMethod]
        public async Task Update_Category()
        {
            var command = new UpdateCategoryCommand
            {
                Id = (await _context.Categories.FirstOrDefaultAsync()).Id,
                Name = "Test2"
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.AreEqual(command.Name, (await _context.Categories.FindAsync(command.Id)).Name);
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
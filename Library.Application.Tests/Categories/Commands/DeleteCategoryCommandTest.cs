using FluentValidation.TestHelper;
using Library.Application.Categories.Commands.DeleteCategory;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{

    public class DeleteCategoryCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly DeleteCategoryCommandHandler _commandHandler;

        public DeleteCategoryCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new DeleteCategoryCommandHandler(_context);
        }

        [Fact]
        public async Task Delete_Category()
        {
            var command = new DeleteCategoryCommand
            {
                Id = (await _context.Categories.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.Null(await _context.Categories.FindAsync(command.Id));
        }

        [Fact]
        public void Delete_Category_With_No_Id_Throws_Exception()
        {
            var validator = new DeleteCategoryCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
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
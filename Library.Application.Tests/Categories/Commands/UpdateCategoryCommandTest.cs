using FluentValidation.TestHelper;
using Library.Application.Category.Commands.UpdateCategory;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{

    public class UpdateCategoryCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly UpdateCategoryCommandHandler _commandHandler;

        public UpdateCategoryCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new UpdateCategoryCommandHandler(_context);
        }

        [Fact]
        public async Task Update_Category()
        {
            var command = new UpdateCategoryCommand
            {
                Id = (await _context.Categories.FirstOrDefaultAsync()).Id,
                Name = "Test2"
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.Equal(command.Name, (await _context.Categories.FindAsync(command.Id)).Name);
        }

        [Fact]
        public void Update_Category_With_No_Id_Throws_Exception()
        {
            var validator = new UpdateCategoryCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Update_Category_With_No_Name_Throws_Exception()
        {
            var validator = new UpdateCategoryCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Categories.Add(new Domain.Entities.Category("Test1"));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
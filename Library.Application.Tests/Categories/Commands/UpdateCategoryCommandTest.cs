using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Library.Application.Category.Commands.UpdateCategory;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{
    public class UpdateCategoryCommandTest : TestBase
    {
        [Fact]
        public async Task Update_Category()
        {
            await using var context = GetContextWithData();
            var handler = new UpdateCategoryCommandHandler(context);
            var command = new UpdateCategoryCommand
            {
                Id = (await context.Categories.FirstOrDefaultAsync()).Id,
                Name = "Test2"
            };

            await handler.Handle(command, CancellationToken.None);
            Assert.Equal(command.Name, (await context.Categories.FindAsync(command.Id)).Name);
        }

        [Fact]
        public void Update_Category_With_No_Id_Throws_Exception()
        {
            using var context = GetContextWithData();
            var model = new UpdateCategoryCommand {Id = Guid.Empty};
            var validator = new UpdateCategoryCommandValidator(context);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Update_Category_With_No_Name_Throws_Exception()
        {
            using var context = GetContextWithData();
            var model = new UpdateCategoryCommand {Name = null};
            var validator = new UpdateCategoryCommandValidator(context);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Update_Category_With_Title_That_Already_Exists_Throws_Exception()
        {
            using var context = GetContextWithData();
            var category = context.Categories.FirstOrDefault(b => b.Name == "Action");

            if (category == null) return;
            category.UpdateCategory("Technical");

            var validator = new UpdateCategoryCommandValidator(context);
            var result = validator.TestValidate(new UpdateCategoryCommand
            {
                Id = category.Id,
                Name = category.Name
            });
            result.ShouldHaveValidationErrorFor(x => x);
        }
    }
}
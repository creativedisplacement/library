using FluentValidation.TestHelper;
using Library.Application.Category.Commands.CreateCategory;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{
    public class CreateCategoryCommandTest : TestBase
    {
        [Fact]
        public async Task Create_New_Category()
        {
            using (var context = GetContextWithData())
            {
                var handler = new CreateCategoryCommandHandler(context);
                var command = new CreateCategoryCommand
                {
                    Name = "Test1"
                };

                await handler.Handle(command, CancellationToken.None);
                var category = await context.Categories.SingleOrDefaultAsync(c => c.Name == command.Name);

                Assert.Equal(command.Name, category.Name);
            }
        }

        [Fact]
        public void Create_Category_With_No_Name_Throws_Exception()
        {
            var validator = new CreateCategoryCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }
    }
}
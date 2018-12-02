using MediatR;

namespace Library.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest
    {
        public string Name { get; set; }
    }
}
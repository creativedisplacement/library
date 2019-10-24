using Library.Common;
using Library.Common.Categories.Queries.GetCategory;
using MediatR;

namespace Library.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
    {
    }
}
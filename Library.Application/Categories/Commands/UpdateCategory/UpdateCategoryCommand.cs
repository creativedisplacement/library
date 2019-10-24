using Library.Common;
using Library.Common.Categories.Queries.GetCategory;
using MediatR;

namespace Library.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
    {
    }
}
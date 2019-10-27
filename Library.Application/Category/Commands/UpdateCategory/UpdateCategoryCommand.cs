using Library.Common;
using Library.Common.Category.Queries.GetCategory;
using MediatR;

namespace Library.Application.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
    {
    }
}
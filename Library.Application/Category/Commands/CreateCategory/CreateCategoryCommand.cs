using Library.Common;
using Library.Common.Category.Queries.GetCategory;
using MediatR;

namespace Library.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
    {
    }
}
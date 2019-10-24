using Library.Common;
using Library.Common.Categories.Queries.GetCategory;
using MediatR;

namespace Library.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : BaseItem, IRequest<GetCategoryModel>
    {
    }
}
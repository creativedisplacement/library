using Library.Common;
using Library.Common.Category.Queries.GetCategory;
using MediatR;

namespace Library.Application.Category.Queries.GetCategory
{
    public class GetCategoryQuery : BaseItem, IRequest<GetCategoryModel>
    {
    }
}
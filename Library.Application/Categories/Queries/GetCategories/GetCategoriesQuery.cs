using Library.Common.Categories.Queries.GetCategories;
using MediatR;

namespace Library.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<GetCategoriesModel>
    {
    }
}
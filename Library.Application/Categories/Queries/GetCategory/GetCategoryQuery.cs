using System;
using MediatR;

namespace Library.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryModel>
    {
        public Guid Id { get; set; }
    }
}
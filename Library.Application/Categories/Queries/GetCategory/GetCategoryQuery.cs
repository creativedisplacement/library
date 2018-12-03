using System;
using MediatR;

namespace Library.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<GetCategoryModel>
    {
        public Guid Id { get; set; }
    }
}
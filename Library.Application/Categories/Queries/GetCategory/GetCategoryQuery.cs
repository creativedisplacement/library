using Library.Common.Categories.Queries.GetCategory;
using MediatR;
using System;

namespace Library.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<GetCategoryModel>
    {
        public Guid Id { get; set; }
    }
}
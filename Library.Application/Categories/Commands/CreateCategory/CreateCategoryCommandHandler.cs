﻿using Library.Common.Categories.Queries.GetCategory;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : BaseCommandHandler, IRequestHandler<CreateCategoryCommand, GetCategoryModel>
    {
        private readonly LibraryDbContext _context;

        public CreateCategoryCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetCategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Name);
            SetDomainState(category);
            await _context.SaveChangesAsync(cancellationToken);

            return new GetCategoryModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
﻿using System.Linq;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryModel>
    {
        private readonly LibraryDbContext _context;

        public GetCategoryQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetCategoryModel> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories
                .Include(i => i.BookCategories)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            return new GetCategoryModel
            {
                Id = result.Id,
                Name = result.Name,
                Books = result.BookCategories.Select(b => new GetCategoryBookModel{ Id = b.BookId, Title = b.Book.Title, IsAvailable = b.Book.IsAvailable})
                    .ToList()
            };
        }
    }
}
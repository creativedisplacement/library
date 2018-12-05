﻿using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public UpdateCategoryCommandHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.UpdateCategory(request.Name);
            _context.Categories.Update(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
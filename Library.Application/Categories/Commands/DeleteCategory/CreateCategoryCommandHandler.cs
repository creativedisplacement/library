﻿using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly LibraryDbContext _context;

        public DeleteCategoryCommandHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            var categoryInUse = _context.Books.Any(b => b.Categories.Any(c => c.Id == request.Id));
            if (categoryInUse)
            {
                throw new DeleteFailureException(nameof(Category), request.Id, "There are existing books associated with this category.");
            }

            _context.Categories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
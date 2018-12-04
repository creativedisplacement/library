using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public CreateCategoryCommandHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Name);

            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
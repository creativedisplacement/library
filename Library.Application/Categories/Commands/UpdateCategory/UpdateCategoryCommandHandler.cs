using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Categories.Queries.GetCategory;

namespace Library.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : BaseCommandHandler, IRequestHandler<UpdateCategoryCommand, GetCategoryModel>
    {
        private readonly LibraryDbContext _context;

        public UpdateCategoryCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetCategoryModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.UpdateCategory(request.Name);
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
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
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result == null)
            {
                throw new NotFoundException(nameof(Category), request);
            }

            return new GetCategoryModel { Id = result.Id, Name = result.Name };
        }
    }
}

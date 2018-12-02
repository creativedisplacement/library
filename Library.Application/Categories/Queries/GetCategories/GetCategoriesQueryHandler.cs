using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesModel>
    {
        private readonly LibraryDbContext _context;

        public GetCategoriesQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriesModel> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            if (result == null)
            {
                throw new NotFoundException(nameof(Category), request);
            }

            return new CategoriesModel
            {
                Categories = result.Select(c => new CategoryModel { Id = c.Id, Name = c.Name }).AsEnumerable()
            };
        }
    }
}

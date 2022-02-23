using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.Exceptions;
using Library.Common.Models.Categories;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesModel>
{
    private readonly LibraryDbContext _context;

    public GetCategoriesQueryHandler(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<GetCategoriesModel> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        if (result == null)
        {
            throw new NotFoundException(nameof(Category), request);
        }

        return new GetCategoriesModel
        {
            Categories = result.Select(c => new CategoryModel {Id = c.Id, Name = c.Name}).AsEnumerable()
        };
    }
}
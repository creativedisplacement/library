using System;
using Library.Common.Models.Books;

namespace Library.Application.Books.Queries.GetBooks;

public class GetBooksQuery : BaseTitleItem, IRequest<GetBooksModel>
{
    public ICollection<Guid> CategoryIds { get; init; } = new List<Guid>();
    public Guid LenderId { get; init; }
    public bool? IsAvailable { get; init; }
}
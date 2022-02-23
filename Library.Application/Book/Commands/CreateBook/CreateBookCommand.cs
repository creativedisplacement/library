using Library.Common.Models.Book;

namespace Library.Application.Book.Commands.CreateBook;

public class CreateBookCommand : BaseTitleItem, IRequest<GetBookModel>
{
    public ICollection<CreateBookModelCategory> Categories { get; init; }
}
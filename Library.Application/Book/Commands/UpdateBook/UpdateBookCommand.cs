using Library.Common.Models.Book;

namespace Library.Application.Book.Commands.UpdateBook;

public class UpdateBookCommand : BaseTitleItem, IRequest<GetBookModel>
{
    public ICollection<GetBookModelCategory> Categories { get; init; }
}
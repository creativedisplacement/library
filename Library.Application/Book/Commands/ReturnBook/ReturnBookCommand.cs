using Library.Common.Models.Book;

namespace Library.Application.Book.Commands.ReturnBook;

public class ReturnBookCommand : BaseItem, IRequest<ReturnBookModel>
{
}
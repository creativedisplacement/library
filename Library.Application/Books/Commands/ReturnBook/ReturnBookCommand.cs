using Library.Common;
using Library.Common.Book.Queries.GetBook;
using MediatR;

namespace Library.Application.Books.Commands.ReturnBook
{
    public class ReturnBookCommand : BaseItem, IRequest<GetBookModel>
    {
    }
}
using Library.Common;
using Library.Common.Book.Commands.ReturnBook;
using MediatR;

namespace Library.Application.Book.Commands.ReturnBook
{
    public class ReturnBookCommand : BaseItem, IRequest<ReturnBookModel>
    {
    }
}
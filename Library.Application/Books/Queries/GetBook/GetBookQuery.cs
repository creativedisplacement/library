using Library.Common;
using Library.Common.Book.Queries.GetBook;
using MediatR;

namespace Library.Application.Books.Queries.GetBook
{
    public class GetBookQuery : BaseTitleItem, IRequest<GetBookModel>
    {
    }
}
using Library.Common;
using Library.Common.Book.Queries.GetBook;
using MediatR;

namespace Library.Application.Book.Queries.GetBook
{
    public class GetBookQuery : BaseTitleItem, IRequest<GetBookModel>
    {
    }
}
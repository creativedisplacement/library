using Library.Common.Models.Book;

namespace Library.Application.Book.Queries.GetBook;

public class GetBookQuery : BaseTitleItem, IRequest<GetBookModel>
{
}
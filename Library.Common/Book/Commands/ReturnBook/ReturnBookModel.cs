using Library.Common.Book.Queries.GetBook;
using Library.Common.People.Queries.GetPerson;

namespace Library.Common.Book.Commands.ReturnBook
{
    public class ReturnBookModel : GetBookModel
    {
        public GetPersonModel Lender { get; set; }
    }
}

using Library.Common.Book.Queries.GetBook;
using Library.Common.People.Queries.GetPerson;

namespace Library.Common.Book.Commands.LendBook
{
    public class LendBookModel : GetBookModel
    {
        public GetPersonModel Lender { get; set; }
    }
}

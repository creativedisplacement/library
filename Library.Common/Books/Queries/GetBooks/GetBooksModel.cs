using Library.Common.People.Queries.GetPerson;
using System.Collections.Generic;

namespace Library.Common.Books.Queries.GetBooks
{
    public class GetBooksModel
    {
        public IEnumerable<GetBookModel> Books { get; set; }
    }

    public class GetBookModel : BaseTitleItem
    {
        public IEnumerable<GetBookModelCategory> Categories { get; set; } = new List<GetBookModelCategory>();
        public GetPersonBookModel Lender { get; set; }
    }

    public class GetBookModelCategory : BaseNameItem
    {
    }
}
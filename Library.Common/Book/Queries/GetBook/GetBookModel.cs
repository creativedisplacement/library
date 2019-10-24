using System.Collections.Generic;

namespace Library.Common.Book.Queries.GetBook
{
    public class GetBookModel : BaseTitleItem
    {
        public virtual ICollection<GetBookModelCategory> Categories { get; set; }
    }

    public class GetBookModelCategory : BaseNameItem
    { 
    }
}
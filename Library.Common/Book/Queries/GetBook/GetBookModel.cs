using System.Collections.Generic;

namespace Library.Common.Book.Queries.GetBook
{
    public class GetBookModel : BaseModelTitleItem
    {
        public virtual ICollection<GetBookModelCategory> Categories { get; set; } = new List<GetBookModelCategory>();
    }

    public class GetBookModelCategory : BaseModelNameItem
    { 
    }
}
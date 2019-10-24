using System.Collections.Generic;

namespace Library.Common.Book.Commands.UpdateBook
{
    public class UpdateBookModel : BaseTitleItem
    {
        public virtual ICollection<UpdateBookModelCategory> Categories { get; set; } = new List<UpdateBookModelCategory>();
    }

    public class UpdateBookModelCategory : BaseNameItem
    {
    }
}
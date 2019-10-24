using System.Collections.Generic;

namespace Library.Common.Book.Commands.UpdateBook
{
    public class UpdateBookModel : BaseModelTitleItem
    {
        public virtual ICollection<UpdateBookModelCategory> Categories { get; set; } = new List<UpdateBookModelCategory>();
    }

    public class UpdateBookModelCategory : BaseModelNameItem
    {
    }
}
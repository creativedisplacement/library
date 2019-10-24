using System.Collections.Generic;

namespace Library.Common.Book.Commands.CreateBook
{
    public class CreateBookModel : BaseModelTitleItem
    {
        public virtual ICollection<CreateBookModelCategory> Categories { get; set; } = new List<CreateBookModelCategory>();
    }

    public class CreateBookModelCategory : BaseModelNameItem
    {
    }
}
using System;
using System.Collections.Generic;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<UpdateBookModelCategory> Categories { get; set; } = new List<UpdateBookModelCategory>();
    }

    public class UpdateBookModelCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

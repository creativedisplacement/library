using System;
using System.Collections.Generic;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<CreateBookModelCategory> Categories { get; set; } = new List<CreateBookModelCategory>();
    }

    public class CreateBookModelCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
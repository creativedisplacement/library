using System;
using System.Collections.Generic;

namespace Library.Application.Books.Queries.GetBook
{
    public class GetBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<GetBookModelCategory> Categories { get; set; } = new List<GetBookModelCategory>();
    }

    public class GetBookModelCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
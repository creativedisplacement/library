using Library.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksModel
    {
        public IEnumerable<GetBookModel> Books { get; set; }
    }

    public class GetBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<GetBookModelCategory> Categories { get; set; } = new List<GetBookModelCategory>();
        public Person Lender { get; set; }
    }

    public class GetBookModelCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
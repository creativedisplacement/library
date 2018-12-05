using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Domain.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
            
        }

        public Book(string title)
        {
            Title = title;
        }

        public Book(Guid id, string title, ICollection<BookCategory> categories)
        {
            Id = id;
            Title = title;
            BookCategories = categories;
        }

        public Book(string title, ICollection<BookCategory> categories)
        {
            Title = title;
            BookCategories = categories;
        }

        public Book(Guid id, string title, ICollection<BookCategory> categories, Person lender)
        {
            Id = id;
            Title = title;
            BookCategories = categories;
            Lender = lender;
        }

        public Book(string title, ICollection<BookCategory> categories, Person lender)
        {
            Title = title;
            BookCategories = categories;
            Lender = lender;
        }

        public string Title { get; private set; }
        public ICollection<BookCategory> BookCategories { get;  private set; } = new List<BookCategory>();
        public Person Lender { get; private set; }

        public bool IsAvailable => Lender == null;

        public void UpdateBook(string title, ICollection<BookCategory> categories)
        {
            Title = title;
            BookCategories = categories;
        }

        public void RemoveBook()
        {

        }

        public void LendBook(Person lender)
        {
            Lender = lender;
        }

        public void ReturnBook()
        {
            Lender = null;
        }
    }
}
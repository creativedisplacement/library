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

        public Book(string title, ICollection<Category> categories)
        {
            Title = title;
            BookCategories = categories.Select(c => new BookCategory{ CategoryId = c.Id, BookId = Id}).ToList();
        }

        public Book(string title, ICollection<Category> categories, Person lender)
        {
            Title = title;
            BookCategories = categories.Select(c => new BookCategory { CategoryId = c.Id, BookId = Id }).ToList();
            Lender = lender;
        }

        public string Title { get; private set; }
        public ICollection<BookCategory> BookCategories { get;  set; } = new List<BookCategory>();
        public Person Lender { get; private set; }

        public bool IsAvailable => Lender == null;

        public void UpdateBook(string title, List<Category> categories)
        {
            Title = title;
            BookCategories = categories.Select(c => new BookCategory { CategoryId = c.Id, BookId = Id }).ToList();
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
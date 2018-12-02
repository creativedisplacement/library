using System.Collections.Generic;

namespace Library.Domain.Entities
{
    public class Book : IEntity
    {
        public Book()
        {
            
        }

        public Book(string title, ICollection<Category> categories)
        {
            Title = title;
            Categories = categories;
        }

        public Book(string title, ICollection<Category> categories, Person lender)
        {
            Title = title;
            Categories = categories;
            Lender = lender;
        }

        public string Title { get; private set; }
        public virtual ICollection<Category> Categories { get; private set; } = new List<Category>();
        public virtual Person Lender { get; private set; }

        public bool IsAvailable => Lender == null;

        public void UpdateBook(string title, List<Category> categories)
        {
            Title = title;
            Categories = categories;
        }

        public void RemoveBook()
        {

        }

        public void LendBookTo(Person lender)
        {
            Lender = lender;
        }

        public void BookReturned()
        {
            Lender = null;
        }
    }
}
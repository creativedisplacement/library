using System.Collections.Generic;

namespace Library.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

        public void UpdateCategory(string name)
        {
            Name = name;
        }

        public void RemoveCategory()
        {

        }
    }
}
namespace Library.Domain.Entities
{
    public class Category : IEntity
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void UpdateCategory(string name)
        {
            Name = name;
        }

        public void RemoveCategory()
        {

        }
    }
}
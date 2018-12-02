using Library.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.Persistence
{
    public class LibraryInitialiser
    {

        public static void Initialise(LibraryDbContext context)
        {
            var initialiser = new LibraryInitialiser();
            initialiser.SeedEverything(context);
        }

        public void SeedEverything(LibraryDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
            {
                return; // Db has been seeded
            }

            SeedBooks(context);
            SeedCategories(context);
            SeedPeople(context);
        }

        public void SeedBooks(LibraryDbContext context)
        {
            var books = new[]
            {
                new Book("Docker on Windows", new List<Category>(){ new Category("Technical")}),
                new Book("Open", new List<Category>(){ new Category("Biographical") }),
                new Book("This is going to hurt", new List<Category>(){ new Category("Biographical"), new Category("Humour") }),
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }


        public void SeedCategories(LibraryDbContext context)
        {
            var categories = new[]
            {
                new Category("Humour"),
                new Category("Drama"),
                new Category("Action"),
                new Category("Thriller"),
                new Category("Biographical"),
                new Category("Technical"),
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public void SeedPeople(LibraryDbContext context)
        {
            var people = new[]
            {
                new Person("Victor","victor@shodimeji.com", true),
                new Person("Tunde","tunde@ayoola.com", false),
            };
            context.Persons.AddRange(people);
            context.SaveChanges();
        }
    }
}
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Application.Tests.Infrastructure
{
    public class LibraryContextFactory
    {
        public static LibraryDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new LibraryDbContext(options);

            context.Database.EnsureCreated();

            SeedCategories(context);
            SeedPeople(context);
            SeedBooks(context);
            SeedBookCategories(context);

            return context;
        }

        public static void SeedBookCategories(LibraryDbContext context)
        {
            var categories = context.Categories;
            var books = context.Books;

            context.BookCategories.AddRange(
            new BookCategory { Book = books.First(), Category = categories.Skip(5).Take(1).First() },
            new BookCategory { Book = books.Skip(1).Take(1).First(), Category = categories.Skip(1).Take(1).First() },
            new BookCategory { Book = books.Skip(1).Take(1).First(), Category = categories.Skip(4).Take(1).First() },
            new BookCategory { Book = books.Skip(2).Take(1).First(), Category = categories.Skip(4).Take(1).First() },
            new BookCategory { Book = books.Skip(2).Take(1).First(), Category = categories.Skip(1).Take(1).First() },
            new BookCategory { Book = books.Skip(2).Take(1).First(), Category = categories.First() });
            context.SaveChanges();
        }

        public static void SeedBooks(LibraryDbContext context)
        {
            var lender = context.Persons.First();
            var books = new[]
            {
                new Book("Docker on Windows", new List<BookCategory>(), lender),
                new Book("Open"),
                new Book("This is going to hurt")
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }

        public static void SeedCategories(LibraryDbContext context)
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

        public static void SeedPeople(LibraryDbContext context)
        {
            var people = new[]
            {
                new Person("Victor","victor@shodimeji.com", true),
                new Person("Tunde","tunde@ayoola.com", false),
            };
            context.Persons.AddRange(people);
            context.SaveChanges();
        }

        public static void Destroy(LibraryDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
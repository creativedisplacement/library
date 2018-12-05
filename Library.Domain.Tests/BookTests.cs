using Library.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Domain.Tests
{
    [TestClass]
    public class BookTests
    {
        private Guid _id;
        private string _title;
        private ICollection<BookCategory> _categories;
        private Person _lender;

        [TestInitialize]
        public void Initialise()
        {
            _id = Guid.NewGuid();
            _title = "Book1";
            _categories = new List<BookCategory>{ new BookCategory{ BookId = _id, CategoryId = Guid.NewGuid()},
                new BookCategory { BookId = _id, CategoryId = Guid.NewGuid() } };
            _lender = new Person("John", "john@test.com", true);
        }

        [TestMethod]
        public void Create_Book_With_Title_And_Categories()
        {
            var book = new Book(_id, _title, _categories);

            Assert.AreEqual(_id, book.Id);
            Assert.AreEqual(_title, book.Title); 
            CollectionAssert.AreEqual(_categories.ToList(), book.BookCategories.ToList());
            Assert.IsTrue(book.IsAvailable);
        }

        [TestMethod]
        public void Create_Book_With_Title_Categories_And_Lender()
        {
            var book = new Book(_id, _title, _categories, _lender);

            Assert.AreEqual(_id, book.Id);
            Assert.AreEqual(_title, book.Title);
            CollectionAssert.AreEqual(_categories.ToList(), book.BookCategories.ToList());
            Assert.AreEqual(_lender, book.Lender);
            Assert.IsFalse(book.IsAvailable);
        }

        [TestMethod]
        public void Check_Book_Is_Available()
        {
            var book = new Book(_title, _categories);
            Assert.IsTrue(book.IsAvailable);
        }

        [TestMethod]
        public void Check_Book_Is_Not_Available()
        {
            var book = new Book(_title, _categories, _lender);
            Assert.IsFalse(book.IsAvailable);
        }

        [TestMethod]
        public void Update_Book_With_Title_And_Categories()
        {
            const string newTitle = "new title";
            var newCategories = new List<BookCategory>{ new BookCategory{ BookId = _id, CategoryId = Guid.NewGuid()}};
            var book = new Book(_id, _title, _categories);

            
            book.UpdateBook(newTitle, newCategories);

                    Assert.AreEqual(_id, book.Id);
            Assert.AreEqual(newTitle, book.Title);
            CollectionAssert.AreEqual(newCategories, book.BookCategories.ToList());

        }

        [TestMethod]
        public void Remove_Book()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Check_Lend_Book_To()
        {
            var book = new Book(_title, _categories);
            book.LendBook(_lender);
            Assert.AreEqual(_lender, book.Lender);
        }

        [TestMethod]
        public void Book_Is_Returned()
        {
            var book = new Book(_title, _categories, _lender);
            book.ReturnBook();
            Assert.AreEqual(null, book.Lender);
        }
    }
}
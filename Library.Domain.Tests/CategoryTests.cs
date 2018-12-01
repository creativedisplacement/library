using Library.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Library.Domain.Tests
{
    [TestClass]
    public class CategoryTests
    {
        private string _categoryName;

        [TestInitialize]
        public void Initialise()
        {
            _categoryName = "new";
        }

        [TestMethod]
        public void Create_Category()
        {
            Assert.AreEqual(_categoryName, new Category(_categoryName).Name);
        }

        [TestMethod]
        public void Update_Category()
        {
            const string newCategoryName = "old";
            var category = new Category(_categoryName);
            category.UpdateCategory(newCategoryName);
            Assert.AreEqual(newCategoryName, category.Name);
        }

        [TestMethod]
        public void Remove_Category()
        {
            throw new NotImplementedException();
        }
    }
}

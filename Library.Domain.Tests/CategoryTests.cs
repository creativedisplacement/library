using Library.Domain.Entities;
using System;
using Xunit;

namespace Library.Domain.Tests
{
    public class CategoryTests
    {
        private readonly string _categoryName;

        public CategoryTests()
        {
            _categoryName = "new";
        }

        [Fact]
        public void Create_Category()
        {
            Assert.Equal(_categoryName, new Category(_categoryName).Name);
        }

        [Fact]
        public void Update_Category()
        {
            const string newCategoryName = "old";
            var category = new Category(_categoryName);
            category.UpdateCategory(newCategoryName);
            Assert.Equal(newCategoryName, category.Name);
        }

        [Fact]
        public void Remove_Category()
        {
            throw new NotImplementedException();
        }
    }
}
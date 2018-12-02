using System;
using System.Collections.Generic;

namespace Library.Application.Categories.Queries.GetCategories
{
    public class CategoriesModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
    }

    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<GetPersonBookModel> Books { get; set; }
    }

    public class GetPersonBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
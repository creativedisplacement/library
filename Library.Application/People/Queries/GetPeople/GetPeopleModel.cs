using System;
using System.Collections.Generic;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleModel
    {
        public IEnumerable<GetPersonModel> People { get; set; }
    }

    public class GetPersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
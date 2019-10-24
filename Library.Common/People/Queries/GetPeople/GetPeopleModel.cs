using System.Collections.Generic;
using Library.Common.People.Queries.GetPerson;

namespace Library.Common.People.Queries.GetPeople
{
    public class GetPeopleModel
    {
        public IEnumerable<GetPersonModel> People { get; set; }
    }
}
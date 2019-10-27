using Library.Common.People.Queries.GetPerson;
using System.Collections.Generic;

namespace Library.Common.People.Queries.GetPeople
{
    public class GetPeopleModel
    {
        public IEnumerable<GetPersonModel> People { get; set; }
    }
}
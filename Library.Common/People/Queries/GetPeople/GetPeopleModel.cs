using System.Collections.Generic;

namespace Library.Common.People.Queries.GetPeople
{
    public class GetPeopleModel
    {
        public IEnumerable<GetPersonModel> People { get; set; }
    }

    public class GetPersonModel : BasePersonItem
    {
        public new bool? IsAdmin { get; set; }
    }
}
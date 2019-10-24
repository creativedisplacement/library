using System.Collections.Generic;

namespace Library.Common.People.Queries.GetPerson
{
    public class GetPersonModel : BaseModelPersonItem
    {
        public ICollection<GetPersonBookModel> Books { get; set; }
    }

    public class GetPersonBookModel : BaseModelTitleItem
    {
    }
}
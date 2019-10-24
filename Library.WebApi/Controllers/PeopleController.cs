using Library.Application.People.Queries.GetPeople;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class PeopleController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string name, string email, bool? isAdmin)
        {
            try
            {
                var people = await Mediator.Send(new GetPeopleQuery
                {
                    Name = name, 
                    Email = email, 
                    IsAdmin = isAdmin
                });

                return new ObjectResult(people);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
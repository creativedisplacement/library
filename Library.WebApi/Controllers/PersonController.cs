using Library.Application.People.Commands.CreatePerson;
using Library.Application.People.Commands.DeletePerson;
using Library.Application.People.Commands.UpdatePerson;
using Library.Application.People.Queries.GetPeople;
using Library.Application.People.Queries.GetPerson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GetPersonModel = Library.Application.People.Queries.GetPeople.GetPersonModel;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController :  BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetPeopleModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPeopleModel>> GetAll(string name, string email, bool? isAdmin)
        {
            return Ok(await Mediator.Send(new GetPeopleQuery{ Name = name, Email = email, IsAdmin = isAdmin}));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPersonModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPersonModel>> GetCategory(Guid id)
        {
            return Ok(await Mediator.Send(new GetPersonQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreatePersonCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdatePersonCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeletePersonCommand { Id = id });
            return NoContent();
        }
    }
}
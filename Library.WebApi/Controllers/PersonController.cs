using Library.Application.People.Commands.CreatePerson;
using Library.Application.People.Commands.DeletePerson;
using Library.Application.People.Commands.UpdatePerson;
using Library.Application.People.Queries.GetPerson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class PersonController :  BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            try
            {
                var person = await Mediator.Send(new GetPersonQuery {Id = id});
                return new ObjectResult(person);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePersonCommand command)
        {
            try
            {
                var person = await Mediator.Send(command);
                return CreatedAtRoute("GetPerson", new { id = person.Id }, person);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdatePersonCommand command)
        {
            try
            {
                var person = await Mediator.Send(command);

                return new ObjectResult(person);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await Mediator.Send(new DeletePersonCommand { Id = id });
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
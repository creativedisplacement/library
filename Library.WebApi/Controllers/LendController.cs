using Library.Application.Books.Commands.LendBook;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class LendController : BaseController
    {
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> LendBook(Guid id, [FromBody]LendBookCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
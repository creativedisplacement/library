using Library.Application.Books.Commands.LendBook;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class LendController : BaseController
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> LendBook(Guid id, [FromBody]LendBookCommand command)
        {
            try
            {
                var lendBook = await Mediator.Send(command);
                return new ObjectResult(lendBook);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
using Library.Application.Books.Commands.ReturnBook;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class ReturnController : BaseController
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> LendBook(Guid id, [FromBody]ReturnBookCommand command)
        {
            try
            {
                var returnedBook = await Mediator.Send(command);
                return new ObjectResult(returnedBook);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
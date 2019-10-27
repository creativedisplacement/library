using Library.Application.Book.Commands.CreateBook;
using Library.Application.Book.Commands.DeleteBook;
using Library.Application.Book.Commands.UpdateBook;
using Library.Application.Book.Queries.GetBook;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class BookController : BaseController
    {
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await Mediator.Send(new GetBookQuery { Id = id });
            if (book == null)
            {
                return new NoContentResult();
            }
            return new ObjectResult(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBookCommand command)
        {
            var book = await Mediator.Send(command);
            return CreatedAtRoute("GetBook", new {id = book.Id}, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateBookCommand command)
        {
            try
            {
                var book = await Mediator.Send(command);
                return new ObjectResult(book);
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
                await Mediator.Send(new DeleteBookCommand { Id = id });
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
using Library.Application.Books.Commands.CreateBook;
using Library.Application.Books.Commands.DeleteBook;
using Library.Application.Books.Commands.UpdateBook;
using Library.Application.Books.Queries.GetBook;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class BookController : BaseController
    {
        [HttpGet("{id}", Name = "test")]
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
            return CreatedAtRoute("test", new {id = book.Id}, book);
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
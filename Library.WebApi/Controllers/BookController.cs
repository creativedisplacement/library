using Library.Application.Books.Commands.CreateBook;
using Library.Application.Books.Commands.DeleteBook;
using Library.Application.Books.Commands.UpdateBook;
using Library.Application.Books.Queries.GetBook;
using Library.Application.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Library.Application.Books.Commands.LendBook;
using Library.Application.Books.Commands.ReturnBook;
using Library.Domain.Entities;
using GetBookModel = Library.Application.Books.Queries.GetBook.GetBookModel;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<GetBooksModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(string title, List<Guid>categoryIds, Guid lenderId, bool? isAvailable)
        {
            return Ok(await Mediator.Send(new GetBooksQuery{ Title = title, CategoryIds = categoryIds, LenderId = lenderId, IsAvailable = isAvailable}));
        }

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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateBookCommand command)
        {
            try
            {
                return new ObjectResult(await Mediator.Send(command));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPut("lend/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> LendBook(Guid id, [FromBody]LendBookCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("return/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> LendBook(Guid id, [FromBody]ReturnBookCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteBookCommand { Id = id });
            return NoContent();
        }
    }
}
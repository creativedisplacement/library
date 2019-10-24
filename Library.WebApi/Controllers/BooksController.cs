using Library.Application.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class BooksController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string title, List<Guid> categoryIds, Guid lenderId, bool? isAvailable)
        {
            try
            {
                var books = await Mediator.Send(new GetBooksQuery
                {
                    Title = title,
                    CategoryIds = categoryIds,
                    LenderId = lenderId,
                    IsAvailable = isAvailable
                });

                return new ObjectResult(books);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
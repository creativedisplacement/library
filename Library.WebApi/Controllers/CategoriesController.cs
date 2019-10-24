using Library.Application.Categories.Queries.GetCategories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await Mediator.Send(new GetCategoriesQuery());
                return new ObjectResult(categories);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
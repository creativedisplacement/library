using Library.Application.Category.Commands.CreateCategory;
using Library.Application.Category.Commands.DeleteCategory;
using Library.Application.Category.Commands.UpdateCategory;
using Library.Application.Category.Queries.GetCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoryController : BaseController
    {
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                var category = await Mediator.Send(new GetCategoryQuery {Id = id});
                return new ObjectResult(category);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommand command)
        {
            try
            {
                var category = await Mediator.Send(command);
                return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateCategoryCommand command)
        {
            try
            {
                var category = await Mediator.Send(command);
                return new ObjectResult(category);
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
                await Mediator.Send(new DeleteCategoryCommand { Id = id });
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
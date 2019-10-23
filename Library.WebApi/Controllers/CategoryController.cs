﻿using Library.Application.Categories.Commands.CreateCategory;
using Library.Application.Categories.Commands.DeleteCategory;
using Library.Application.Categories.Commands.UpdateCategory;
using Library.Application.Categories.Queries.GetCategories;
using Library.Application.Categories.Queries.GetCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetCategoriesModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetCategoriesQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCategoryModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateCategoryCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return NoContent();
        }
    }
}
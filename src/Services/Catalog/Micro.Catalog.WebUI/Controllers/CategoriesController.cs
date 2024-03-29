using Micro.Catalog.Application.Features.Categories.Commands.AddProductToCategory;
using Micro.Catalog.Application.Features.Categories.Commands.CreateCategory;
using Micro.Catalog.Application.Features.Categories.Commands.DeleteCategory;
using Micro.Catalog.Application.Features.Categories.Commands.RemoveProductFromCategory;
using Micro.Catalog.Application.Features.Categories.Commands.UpdateCategory;
using Micro.Catalog.Application.Features.Categories.Queries;
using Micro.Catalog.Application.Features.Categories.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Catalog.WebUI.Controllers;

public class CategoriesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryBriefDto>>> GetCategoriesWithPagination([FromQuery] GetCategoriesWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute] GetCategoryByIdQuery query)
    {
        return await Mediator.Send(query);
    }


    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{id}/products")]
    public async Task<ActionResult> AddProduct(string id, [FromBody] AddProductToCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpDelete("{id}/products")]
    public async Task<ActionResult> RemoveProduct(string id, [FromBody] RemoveProductFromCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteCategoryCommand { Id = id });

        return NoContent();
    }
}
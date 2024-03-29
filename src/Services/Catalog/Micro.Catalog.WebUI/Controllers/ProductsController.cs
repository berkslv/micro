using Micro.Catalog.Application.Features.Products.Commands.CreateProduct;
using Micro.Catalog.Application.Features.Products.Commands.DeleteProduct;
using Micro.Catalog.Application.Features.Products.Commands.UpdateProduct;
using Micro.Catalog.Application.Features.Products.Queries;
using Micro.Catalog.Application.Features.Products.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Catalog.WebUI.Controllers;

public class ProductsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductBriefDto>>> GetProductsWithPagination([FromQuery] GetProductsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] GetProductByIdQuery query)
    {
        return await Mediator.Send(query);
    }


    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateProductCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdateProductCommand command)
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
        await Mediator.Send(new DeleteProductCommand { Id = id });

        return NoContent();
    }
}
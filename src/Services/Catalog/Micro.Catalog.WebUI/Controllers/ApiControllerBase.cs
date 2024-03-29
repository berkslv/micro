using MediatR;
using Micro.Catalog.WebUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Catalog.WebUI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
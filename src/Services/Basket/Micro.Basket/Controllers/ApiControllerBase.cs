using Micro.Basket.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Basket.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    
}
using MediatR.Pipeline;
using Micro.Catalog.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Micro.Catalog.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Micro Request: {Name} {@UserId} {@Request}",
            requestName, _currentUserService.UserId, request);
    }
}
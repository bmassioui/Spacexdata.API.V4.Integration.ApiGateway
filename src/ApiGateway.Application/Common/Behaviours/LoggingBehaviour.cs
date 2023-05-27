using MediatR;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Application.Common.Behaviours;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Starting request: {@Name}, {@Request}, {@LogAt}", requestName, request, DateTime.UtcNow);

        var result = await next();

        _logger.LogInformation("Completed request: {@Name}, {@Request}, {@LogAt}", DateTime.UtcNow, requestName, request);

        return result;
    }
}

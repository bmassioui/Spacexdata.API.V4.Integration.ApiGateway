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

        _logger.LogInformation("{@LogAt} - Starting request: {@Name} {@Request}", DateTime.UtcNow, requestName, request);

        var result = await next();

        _logger.LogInformation("{@LogAt} - Completed request: {@Name} {@Request}", DateTime.UtcNow, requestName, request);

        return result;
    }
}

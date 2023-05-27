using ApiGateway.Application.Common.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ApiGateway.Application.Common.Behaviours;

public sealed class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;

    public PerformanceBehaviour(ILogger<TRequest> logger) => (_logger, _timer) = (logger, new Stopwatch());

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds < (ushort)PerformanceElapsedMillisecond.LongRunning) return response;

        var requestName = typeof(TRequest).Name;

        _logger.LogWarning("Web Api gateway performance status: Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsedMilliseconds, request);

        return response;
    }
}

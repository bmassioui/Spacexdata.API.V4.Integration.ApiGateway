﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Application.Common.Behaviours;

public sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(exception, "Unhandled Exception for Request: {@Name}, {@Request}, {@LogAt}", DateTime.UtcNow, requestName, request);

            throw;
        }
    }
}

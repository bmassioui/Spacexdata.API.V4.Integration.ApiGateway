namespace ApiGateway.WebApi.Common;

public static class Constants
{
    public const string ApiVersion = "4.0";
    public const string FixedWindowLimiterPolicyName = "WebApiFixedWindowLimiterPolicy";
    public const string FixedWindowLimiterForRejectedRequest = "You have reached the maximum number of allowed requests for the IP address";
    public const string HealthCheckName = "WebApiHealth";
    public const string MapHealthChecksPattern = "/ApiGatewayWebApiHealt";
    public const string WebApiCorsPolicyNameConfigurationKey = "WebApiCorsConfig:PolicyName";
    public const string WebApiCorsPolicyNameConfigurationKeyNotFoundExceptionMessage = "Cors configuration could not be found.";
}

namespace ApiGateway.Infrastructure.Common;

public static class Constants
{
    public const string HttpClientNameForSpaceXWebApi = "SpaceXWebApiClient";
    public const ushort AddHttpClientTimeOutInSeconds = 30;
    public const string SpaceXWebApiBaseUrlConfigurationKey = "SpaceXWebApi:V4BaseUrl";
    public const int RetryPolicyCount = 3;
    public const int RetryPolicySleepDuration = 2;
    public const int CircuitBreakerPolicyNumberOfAllowedFailedAttemptsBeforeBreaking = 3;
    public const int CircuitBreakerPolicyDurationOfBreak = 5;
}

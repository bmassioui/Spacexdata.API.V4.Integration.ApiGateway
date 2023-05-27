namespace ApiGateway.Infrastructure.Common;

public static class Constants
{
    public const string HttpClientNameForSpaceXWebApi = "SpaceXWebApiClient";
    public const ushort AddHttpClientTimeOutInSeconds = 30;
    public const string SpaceXWebApiBaseUrlConfigurationKey = "SpaceXWebApi:BaseUrl";
    public const int RetryPolicyCount = 3;
    public const int RetryPolicySleepDuration = 2;
    public const int CircuitBreakerPolicyNumberOfAllowedFailedAttemptsBeforeBreaking = 3;
    public const int CircuitBreakerPolicyDurationOfBreak = 5;
    public const string ElasticsearchNodeUriConfigurationKey = "Elasticsearch:NodeUri";
    public const string ElasticsearchIndexFormat = "SpacexdataApiIntegrationApiGateway-log-{0:yyyy.MM.dd}";
}

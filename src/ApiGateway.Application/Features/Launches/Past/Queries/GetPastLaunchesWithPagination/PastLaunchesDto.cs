namespace ApiGateway.Application.Features.Launches.Past.Queries.GetPastLaunchesWithPagination;

public class PastLaunchesDto
{
    public Doc[] Docs { get; set; } = null!;
}

public class Doc
{
    public Links? Links { get; set; }
    public bool? Success { get; set; }
    public string? Details { get; set; }
    public int Flight_number { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? Date_utc { get; set; }
    public string Id { get; set; } = null!;
}

public class Links
{
    public Patch? Patch { get; set; }
    public string? Webcast { get; set; }
}

public class Patch
{
    public string? Small { get; set; }
    public string? Large { get; set; }
}

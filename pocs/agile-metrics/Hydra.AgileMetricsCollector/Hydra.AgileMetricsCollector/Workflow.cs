namespace Hydra.AgileMetricsCollector;

public sealed record Workflow
{
    public long Id { get; set; }

    public string Name { get; init; } = string.Empty;

    public string Repository { get; init; } = string.Empty;
}

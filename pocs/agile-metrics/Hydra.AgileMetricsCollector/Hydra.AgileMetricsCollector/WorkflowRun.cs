namespace Hydra.AgileMetricsCollector;

public record WorkflowRun
{
    public long Id { get; init; }

    public Workflow Workflow { get; init; } = new Workflow();

    public DateTime CreatedUtc { get; init; }

    public DateTime? UpdatedUtc { get; init; }

    public string Status { get; init; } = string.Empty;

    public string Conclusion { get; init; } = string.Empty;

    public string ActorName { get; init; } = string.Empty;

    public string Url { get; init; } = string.Empty;
}

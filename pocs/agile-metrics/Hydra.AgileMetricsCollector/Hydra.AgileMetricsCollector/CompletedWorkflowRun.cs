namespace Hydra.AgileMetricsCollector;

public record CompletedWorkflowRun
{
    public long Id { get; init; }

    public DateTime CreatedUtc { get; init; }

    public DateTime CompletedUtc { get; init; }

    public long WorkflowId { get; init; }

    public string WorkflowName { get; init; } = string.Empty;

    public string Repository { get; init; } = string.Empty;

    public string PushedToMainBy { get; init; } = string.Empty;

    public string CompletedBy { get; init; } = string.Empty;

    public string CompletedWorkflowRunUrl { get; init; } = string.Empty;

    public double DaysElapsedFromCommitToRelease
    {
        get
        {
            var elapsed = CompletedUtc - CreatedUtc;

            return elapsed.TotalDays;
        }
    }
}

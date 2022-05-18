namespace Hydra.AgileMetricsCollector;

public interface IGithubMetricsCollector
{
    Task<int> GetTotalWorkflowRuns(string repository);

    Task<List<WorkflowRun>> GetWorkflowRuns(string repository, int page, int size = 100);
}

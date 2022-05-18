using Hydra.AgileMetricsCollector;
using System.Text.Json;

var githubToken = "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";

var metricsCollector = new GithubMetricsCollector(githubToken);
var elasticsearchClient = new ElasticsearchClient("http://localhost:9200");

var knownWorkflowsJson = File.ReadAllText("known-workflows.json");
var knownWorkflows = JsonSerializer.Deserialize<List<Workflow>>(knownWorkflowsJson) ?? new List<Workflow>();

var workflowRuns = new List<WorkflowRun>();

var totalWorkflowRuns = await metricsCollector.GetTotalWorkflowRuns("nucleus-schedule-planner");

var totalPages = Math.Ceiling(totalWorkflowRuns / 100.0);

for (var page = 1; page <= totalPages; page++)
{
    var pagedWorkflowRuns = await metricsCollector.GetWorkflowRuns("nucleus-schedule-planner", page);

    if (pagedWorkflowRuns.Count == 0)
    {
        continue;
    }

    workflowRuns.AddRange(pagedWorkflowRuns);
}

var knownWorkflowRunGroups = workflowRuns.Where(wfr => knownWorkflows.Any(wf => wf.Id == wfr.Workflow.Id)).GroupBy(wfr => wfr.Workflow.Id);

var completedWorkflowRuns = new List<CompletedWorkflowRun>();

foreach (var workflowRunGroup in knownWorkflowRunGroups)
{
    WorkflowRun firstRun = null!;

    foreach (var workflowRun in workflowRunGroup.OrderBy(wfr => wfr.CreatedUtc))
    {
        if (firstRun is null)
        {
            firstRun = workflowRun;
            continue;
        }

        if (workflowRun.Status is "completed" && workflowRun.Conclusion is "success")
        {
            var completedWorkflowRun = new CompletedWorkflowRun
            {
                Id = workflowRun.Id,
                CompletedUtc = workflowRun.UpdatedUtc.GetValueOrDefault(),
                CompletedWorkflowRunUrl = workflowRun.Url.ToString(),
                CreatedUtc = firstRun.CreatedUtc,
                PushedToMainBy = firstRun.ActorName,
                WorkflowName = workflowRun.Workflow.Name,
                Repository = workflowRun.Workflow.Repository,
                WorkflowId = workflowRun.Workflow.Id,
                CompletedBy = workflowRun.ActorName
            };

            completedWorkflowRuns.Add(completedWorkflowRun);

            await elasticsearchClient.IndexAsync(completedWorkflowRun, "completed-workflows", completedWorkflowRun.Id.ToString());

            firstRun = null!;

            continue;
        }
    }
}

foreach (var workflow in completedWorkflowRuns.GroupBy(wfr => wfr.WorkflowId))
{
    var totalElapsedTimeToRelease = workflow.Sum(cwfr => cwfr.DaysElapsedFromCommitToRelease);
    var avgElapsedTimeToRelease = totalElapsedTimeToRelease / completedWorkflowRuns.Count;

    Console.WriteLine($"[{workflow.FirstOrDefault()!.WorkflowName}] Average time in days from first commit in main to release: {avgElapsedTimeToRelease}");
}

Console.ReadLine();
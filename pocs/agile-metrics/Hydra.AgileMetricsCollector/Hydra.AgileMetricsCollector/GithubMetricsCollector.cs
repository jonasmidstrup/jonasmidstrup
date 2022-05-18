using RestSharp;
using System.Globalization;
using System.Text.Json;

namespace Hydra.AgileMetricsCollector;

public class GithubMetricsCollector : IGithubMetricsCollector
{
    private const string RepositoryOwner = "Maersk-Global";

    private readonly RestClient _restClient;

    public GithubMetricsCollector(string githubToken)
    {
        _restClient = CreateRestClient(githubToken);
    }

    private RestClient CreateRestClient(string githubToken)
    {
        var restClient = new RestClient("https://api.github.com");

        restClient.AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
        restClient.AddDefaultHeader(KnownHeaders.Authorization, $"token {githubToken}");

        return restClient;
    }

    public async Task<int> GetTotalWorkflowRuns(string repository)
    {
        var requestUri = "/repos/{owner}/{repository}/actions/runs";
        var request = new RestRequest(requestUri, Method.Get);

        request.AddUrlSegment("owner", RepositoryOwner, false);
        request.AddUrlSegment("repository", repository, false);

        request.AddQueryParameter("branch", "main");

        var response = await _restClient.ExecuteAsync(request);

        if (!response.IsSuccessful || response.Content is null)
        {
            throw new Exception("Failed to load Github data");
        }

        var responseContent = JsonSerializer.Deserialize<JsonElement>(response.Content);

        var totalCount = responseContent.GetProperty("total_count").GetInt32();

        return totalCount;
    }

    public async Task<List<WorkflowRun>> GetWorkflowRuns(string repository, int page, int size = 100)
    {
        var requestUri = "/repos/{owner}/{repository}/actions/runs";
        var request = new RestRequest(requestUri, Method.Get);

        request.AddUrlSegment("owner", "Maersk-Global", false);
        request.AddUrlSegment("repository", repository, false);

        request.AddQueryParameter("per_page", size);
        request.AddQueryParameter("page", page);
        request.AddQueryParameter("branch", "main");

        var response = await _restClient.ExecuteAsync(request);

        if (!response.IsSuccessful || response.Content is null)
        {
            throw new Exception("Failed to load Github data");
        }

        var responseContent = JsonSerializer.Deserialize<JsonElement>(response.Content);

        var workflowRuns = new List<WorkflowRun>();

        foreach (var workflowRun in responseContent.GetProperty("workflow_runs").EnumerateArray())
        {
            var run = new WorkflowRun
            {
                Id = workflowRun.GetProperty("id").GetInt64(),
                Workflow = new Workflow
                {
                    Id = workflowRun.GetProperty("workflow_id").GetInt32(),
                    Name = workflowRun.GetProperty("name").GetString(),
                    Repository = workflowRun.GetProperty("repository").GetProperty("name").GetString()
                },
                CreatedUtc = DateTime.Parse(
                    workflowRun.GetProperty("created_at").GetString(),
                    CultureInfo.InvariantCulture),
                UpdatedUtc = DateTime.Parse(
                    workflowRun.GetProperty("updated_at").GetString(),
                    CultureInfo.InvariantCulture),
                Conclusion = workflowRun.GetProperty("conclusion").GetString(),
                Status = workflowRun.GetProperty("status").GetString(),
                ActorName = workflowRun.GetProperty("actor").GetProperty("login").GetString(),
                Url = workflowRun.GetProperty("html_url").GetString()
            };

            workflowRuns.Add(run);
        }

        return workflowRuns;
    }
}

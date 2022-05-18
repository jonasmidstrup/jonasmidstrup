using RestSharp;
using System.Text.Json;

namespace Hydra.AgileMetricsCollector;

public sealed class ElasticsearchClient : IElasticsearchClient
{
    private readonly RestClient _restClient;

    public ElasticsearchClient(string elasticsearchBaseUri)
    {
        _restClient = new RestClient(elasticsearchBaseUri);
    }

    public async Task IndexAsync(string document, string indexName, string? id = null)
    {
        if (string.IsNullOrWhiteSpace(document))
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (string.IsNullOrWhiteSpace(indexName))
        {
            throw new ArgumentNullException(nameof(indexName));
        }

        RestRequest request;
        if (string.IsNullOrWhiteSpace(id))
        {
            var uri = $"/{indexName}/_doc/";
            request = new RestRequest(uri, Method.Post);
        }
        else
        {
            var uri = $"/{indexName}/_create/{id}";
            request = new RestRequest(uri, Method.Put);
        }

        request.AddStringBody(document, DataFormat.Json);

        var response = await _restClient.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception(response.ErrorMessage, response.ErrorException);
        }
    }

    public async Task IndexAsync<T>(T document, string indexName, string? id = null)
    {
        var jsonDocument = JsonSerializer.Serialize(document);
        await IndexAsync(jsonDocument, indexName, id);
    }
}

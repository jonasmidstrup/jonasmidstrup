
namespace Hydra.AgileMetricsCollector
{
    public interface IElasticsearchClient
    {
        Task IndexAsync(string document, string indexName, string? id = null);

        Task IndexAsync<T>(T document, string indexName, string? id = null);
    }
}
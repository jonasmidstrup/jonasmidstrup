using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System.Net.WebSockets;

namespace NetworkSchedulesGraph;

public class GremlinService : IDisposable
{
    private readonly GremlinClient _client;

    private readonly ConnectionPoolSettings _connectionPoolSettings = new ConnectionPoolSettings()
    {
        MaxInProcessPerConnection = 10,
        PoolSize = 30,
        ReconnectionAttempts = 3,
        ReconnectionBaseDelay = TimeSpan.FromMilliseconds(500)
    };

    private readonly Action<ClientWebSocketOptions> _webSocketConfiguration =
        new Action<ClientWebSocketOptions>(options =>
        {
            options.KeepAliveInterval = TimeSpan.FromSeconds(10);
        });

    public GremlinService(
        string host,
        string primaryKey,
        string database,
        string container,
        int port = 443)
    {
        string containerLink = "/dbs/" + database + "/colls/" + container;

        Console.WriteLine($"Connecting to: host: {host}, port: {port}, container: {containerLink}, ssl: true");

        var gremlinServer =
            new GremlinServer(
                host,
                port,
                enableSsl: true,
                username: containerLink,
                password: primaryKey);

        _client =
            new GremlinClient(
                gremlinServer,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType,
                _connectionPoolSettings,
                _webSocketConfiguration);
    }

    public void Dispose() => _client.Dispose();

    public async Task RunQueriesAsync(IEnumerable<ProformaSchedule> proformaSchedules)
    {
        await _client.SubmitWithSingleResultAsync<object>("g.V().drop()");

        var edgeQueries = new List<string>();

        foreach (var proforma in proformaSchedules)
        {
            var gremlinQuery =
                string.Format(
                    "g.addV('proforma').property('id', '{0}').property('scenarioName', '{1}').property('scenarioAssumption', '{2}').property('serviceCode', '{3}').property('rotationName', '{4}').property('serviceName', '{5}').property('status', '{6}')",
                    proforma.ProformaId,
                    proforma.ScenarioName,
                    proforma.ScenarioAssumption,
                    proforma.ServiceCode,
                    proforma.RotationName,
                    proforma.ServiceName,
                    proforma.ProformaStatus);

            Console.WriteLine(string.Format("Running this query: {0}", gremlinQuery));

            try
            {
                var result = await _client.SubmitWithSingleResultAsync<object>(gremlinQuery);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            if (proforma.PreviousProformaId is not null && proforma.PreviousProformaId > 0)
            {
                var edgeGremlinQuery =
                    string.Format("g.V('{0}').addE('isChildOf').to(g.V('{1}'))",
                    proforma.ChildProformaId,
                    proforma.ParentProformaId);

                edgeQueries.Add(edgeGremlinQuery);
            }
        }

        for (int i = 0; i < edgeQueries.Count; i++)
        {
            var edgeQuery = edgeQueries[i];

            try
            {
                var result = await _client.SubmitWithSingleResultAsync<object>(edgeQuery);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}

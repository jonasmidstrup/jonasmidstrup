using NetworkSchedulesGraph;

var connectionString =
    "Server=tcp:nucleus-scheduleplanner-sql-dev.database.windows.net,1433;Initial Catalog=SchedulePlanner;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";

using (var gremlinService =
    new GremlinService(
        "maersknetworkgraph.gremlin.cosmos.azure.com",
        "GZw9V3FcPQqdhVz7banlt5VHY48vlhMX8qSRSoTOPz6ZJDsTibhNLgX0cXYf6avu6TFaaf4mQ5SWACDbj2M0NQ==",
        "network-schedule-drafts-db",
        "network-schedule-drafts-graph"))
using (var proformaDataService = new ProformaDataService(connectionString))
{
    var gremlinQueries = proformaDataService.GetProformaSchedules();

    await gremlinService.RunQueriesAsync(gremlinQueries);
}

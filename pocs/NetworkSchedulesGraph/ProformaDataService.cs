using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace NetworkSchedulesGraph;

public class ProformaDataService : IDisposable
{
    private readonly SqlConnection _connection;

    private readonly Regex _specialCharacterRegex =
        new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public ProformaDataService(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public void Dispose() => _connection.Dispose();

    public IEnumerable<ProformaSchedule> GetProformaSchedules()
    {
        _connection.Open();

        var sql = @"SELECT pm.[ProformaID]
                  ,pm.[ScenarioName]
                  ,pm.[ScenarioAssumption]
                  ,pm.[ServiceCode]
                  ,pm.[RotationName]
                  ,pm.[ServiceName]
                  ,ps.ProformaStatus
                  ,pm.[PreviousProformaId]
              FROM [ddnd].[ProformaMain] pm
              LEFT JOIN ddnd.ProformaStatus ps ON ps.ProformaStatusId = pm.ProformaStatusID";

        using (var command = new SqlCommand(sql, _connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var proforma =
                        new ProformaSchedule(
                            reader.GetInt32(0),
                            RemoveSpecialCharacters(reader.GetString(1)),
                            RemoveSpecialCharacters(reader.GetString(2)),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.SafeGetString(6),
                            reader.SafeGetInt32(7));

                    yield return proforma;
                }
            }
        }
    }

    private string RemoveSpecialCharacters(string input)
    {
        return _specialCharacterRegex.Replace(input, string.Empty);
    }
}

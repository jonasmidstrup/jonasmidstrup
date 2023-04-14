using Microsoft.Data.SqlClient;

namespace NetworkSchedulesGraph;

public static class SqlDataReaderExtensions
{
    public static string? SafeGetString(this SqlDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
        {
            return reader.GetString(colIndex);
        }

        return null;
    }

    public static int? SafeGetInt32(this SqlDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
        {
            return reader.GetInt32(colIndex);
        }

        return null;
    }
}

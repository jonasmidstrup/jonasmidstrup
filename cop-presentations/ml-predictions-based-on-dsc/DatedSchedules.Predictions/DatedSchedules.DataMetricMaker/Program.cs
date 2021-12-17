using CsvHelper;
using CsvHelper.Configuration;
using DatedSchedules.Predictions.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using NodaTime;
using System.Globalization;

var timeZoneConverter = new TimeZoneConverter(DateTimeZoneProviders.Tzdb);

var influxDBClient = InfluxDBClientFactory.Create(
    "http://localhost:8086",
    "VzhI_NOGueC2RpeXIXV6SwW2wmwy5twhgh1S2aU4upO9a5Y2vZ3_vphUxtDZfnCZ8XvDY9UM9DHvrfNAb73H-g==");

using var writeApi = influxDBClient.GetWriteApi();

var files = Directory
            .EnumerateFiles(
                @"C:\Temp\GSIS_v5_data\V5_DATA_NEW_BATCH",
                "*.csv",
                SearchOption.AllDirectories)
            .Select(CsvFile.Create)
            .OrderBy(df => df.Year)
            .ThenBy(df => df.Month);

var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
csvConfiguration.Delimiter = ",";
csvConfiguration.MissingFieldFound = null;
csvConfiguration.IgnoreBlankLines = true;

foreach (var file in files)
{
    Console.WriteLine($"File: {file.Name}");

    using (var reader = new StreamReader(file.Path))
    using (var csv = new CsvReader(reader, csvConfiguration))
    {
        var datedScheduleRecords = csv.GetRecords<DatedSchedule>();

        foreach (var record in datedScheduleRecords)
        {
            if (record.ArrivalStatus == "ACTUALISED")
            {
                var tz = timeZoneConverter.ConvertToDateTimeZoneFromLocalTimeZoneName(
                    record.LocationTimeZoneName,
                    record.TerminalCode);

                if (tz is not null)
                {
                    var localProformaArrivalEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ProformaArrival, tz);
                    var localActualArrivalEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ActualArrival, tz);
                    var actualizedArrivalDifference = localActualArrivalEpoch - localProformaArrivalEpoch;

                    var arrivalPoint = PointData.Measurement("dated_schedule_differences")
                            .Tag("vesselCode", record.VesselCode?.Trim())
                            .Tag("vesselOperatorCode", record.VesselOperatorCode?.Trim())
                            .Tag("arrivalServiceCode", record.ArrivalServiceCode?.Trim())
                            .Tag("previousTerminalCode", record.PreviousTerminalCode?.Trim())
                            .Tag("currentTerminalCode", record.TerminalCode?.Trim())
                            .Tag("measure", "arrival")
                            .Field("difference", Convert.ToSingle(actualizedArrivalDifference.TotalSeconds))
                            .Timestamp(localActualArrivalEpoch, WritePrecision.Ns);

                    var localProformaDepartureEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ProformaDeparture, tz);
                    var localActualDepartureEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ActualDeparture, tz);
                    var actualizedDepartureDifference = localActualDepartureEpoch - localProformaDepartureEpoch;

                    var departurePoint = PointData.Measurement("dated_schedule_differences")
                            .Tag("vesselCode", record.VesselCode?.Trim())
                            .Tag("vesselOperatorCode", record.VesselOperatorCode?.Trim())
                            .Tag("arrivalServiceCode", record.ArrivalServiceCode?.Trim())
                            .Tag("previousTerminalCode", record.PreviousTerminalCode?.Trim())
                            .Tag("currentTerminalCode", record.TerminalCode?.Trim())
                            .Tag("measure", "departure")
                            .Field("difference", Convert.ToSingle(actualizedArrivalDifference.TotalSeconds))
                            .Timestamp(localActualDepartureEpoch, WritePrecision.Ns);

                    writeApi.WritePoints("dsc", "teamhydra", arrivalPoint, departurePoint);
                }
            }
        }
    }
}

Console.WriteLine("All done!");

Console.ReadLine();

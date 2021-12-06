using CsvHelper;
using CsvHelper.Configuration;
using DatedSchedules.DataMetricMaker;
using DatedSchedules.Predictions.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using NodaTime;
using System.Globalization;

var timeZoneConverter = new TimeZoneConverter(DateTimeZoneProviders.Tzdb);

var influxDBClient = InfluxDBClientFactory.Create(
    "http://localhost:8086",
    "8Fr0a7i5niGc6dQTHeMiyasHPeHT974a-y57Nbt3wSgwXdH7UFzUG9ptYCvqy-YfmRH1m9E1C8d5QZ2e0VCAQg==");

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
                    //var localScheduledArrivalEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ScheduledArrival, tz);
                    var localActualArrivalEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ActualArrival, tz);
                    var actualizedArrivalDifference = localActualArrivalEpoch - localProformaArrivalEpoch;

                    var arrivalPoint = PointData.Measurement("dated_schedule_differences")
                            .Tag("vesselCode", record.VesselCode?.Trim())
                            .Tag("vesselOperatorCode", record.VesselOperatorCode?.Trim())
                            .Tag("arrivalServiceCode", record.ArrivalServiceCode?.Trim())
                            .Tag("previousTerminalCode", record.PreviousTerminalCode?.Trim())
                            .Tag("currentTerminalCode", record.TerminalCode?.Trim())
                            .Tag("measure", "arrival")
                            .Field("difference", actualizedArrivalDifference.TotalSeconds)
                            .Timestamp(localActualArrivalEpoch, WritePrecision.Ns);

                    var localProformaDepartureEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ProformaDeparture, tz);
                    //var localScheduledDepartureEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ScheduledDeparture, tz);
                    var localActualDepartureEpoch = timeZoneConverter.ConvertToDateTimeOffset(record.ActualDeparture, tz);
                    var actualizedDepartureDifference = localActualDepartureEpoch - localProformaDepartureEpoch;

                    var departurePoint = PointData.Measurement("dated_schedule_differences")
                            .Tag("vesselCode", record.VesselCode?.Trim())
                            .Tag("vesselOperatorCode", record.VesselOperatorCode?.Trim())
                            .Tag("arrivalServiceCode", record.ArrivalServiceCode?.Trim())
                            .Tag("previousTerminalCode", record.PreviousTerminalCode?.Trim())
                            .Tag("currentTerminalCode", record.TerminalCode?.Trim())
                            .Tag("measure", "departure")
                            .Field("difference", actualizedArrivalDifference.TotalSeconds)
                            .Timestamp(localActualDepartureEpoch, WritePrecision.Ns);

                    writeApi.WritePoints("dsc", "teamhydra", arrivalPoint, departurePoint);
                }
            }
        }
    }
}

Console.WriteLine("All done!");

Console.ReadLine();

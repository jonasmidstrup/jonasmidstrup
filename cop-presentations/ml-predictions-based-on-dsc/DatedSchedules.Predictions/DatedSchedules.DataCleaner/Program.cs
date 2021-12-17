﻿using CsvHelper;
using CsvHelper.Configuration;
using DatedSchedules.Predictions.Models;
using NodaTime; 
using System.Globalization;

var timeZoneConverter = new TimeZoneConverter(DateTimeZoneProviders.Tzdb);

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
    using (var reader = new StreamReader(file.Path))
    using (var csvReader = new CsvReader(reader, csvConfiguration))
    using (var writer = new StreamWriter(@"C:\Temp\GSIS_sanitized_data\" + file.Name + ".csv"))
    using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        Console.WriteLine($"File: {file.Name}");

        var datedScheduleRecords = csvReader.GetRecords<DatedSchedule>();

        csvWriter.WriteHeader<SanitizedDatedSchedule>();
        csvWriter.NextRecord();

        foreach (var record in datedScheduleRecords)
        {
            if (record.ArrivalStatus == "ACTUALISED")
            {
                var tz = timeZoneConverter.ConvertToDateTimeZoneFromLocalTimeZoneName(
                    record.LocationTimeZoneName,
                    record.TerminalCode);

                if (tz is not null)
                {
                    var localProformaArrival = timeZoneConverter.ConvertToDateTimeOffset(record.ProformaArrival, tz);
                    var localActualArrival = timeZoneConverter.ConvertToDateTimeOffset(record.ActualArrival, tz);
                    var actualizedArrivalDifference = localActualArrival - localProformaArrival;

                    var localProformaDeparture = timeZoneConverter.ConvertToDateTimeOffset(record.ProformaDeparture, tz);
                    var localActualDeparture = timeZoneConverter.ConvertToDateTimeOffset(record.ActualDeparture, tz);
                    var actualizedDepartureDifference = localActualDeparture - localProformaDeparture;

                    var sanitizedDsc = new SanitizedDatedSchedule
                    {
                        ActualArrival = localActualArrival.ToString("o"),
                        ActualDeparture = localActualDeparture.ToString("o"),
                        ActualizedArrivalDifference = Convert.ToSingle(actualizedArrivalDifference.TotalSeconds),
                        ActualizedDepartureDifference = Convert.ToSingle(actualizedDepartureDifference.TotalSeconds),
                        ArrivalServiceCode = record.ArrivalServiceCode?.Trim(),
                        PreviousTerminalCode = record.PreviousTerminalCode?.Trim(),
                        ProformaArrival = localProformaArrival.ToString("o"),
                        ProformaDeparture = localProformaDeparture.ToString("o"),
                        TerminalCode = record.TerminalCode?.Trim(),
                        VesselCode = record.VesselCode?.Trim()
                    };

                    csvWriter.WriteRecord(sanitizedDsc);
                    csvWriter.NextRecord();
                }
            }
        }
    }
}

Console.WriteLine("All done!");

Console.ReadLine();

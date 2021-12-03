using DatedSchedules.Predictions.Models;
using Microsoft.ML;
using NodaTime;
using NodaTime.Text;
using System;
using System.IO;
using System.Linq;

namespace DatedSchedules.Predictions
{
    internal class Program
    {
        private static readonly ITimeZoneConverter timeZoneConverter = new TimeZoneConverter(DateTimeZoneProviders.Tzdb);

        public static void Main(string[] args)
        {
            var mlContext = new MLContext();

            var textLoader = mlContext.Data.CreateTextLoader<DatedScheduleData>(
                separatorChar: ',',
                hasHeader: true,
                allowQuoting: true,
                trimWhitespace: true);

            var csvFiles = Directory.GetFiles(@"C:\Temp\GSIS_v5_data\V5_DATA_NEW_BATCH", "*.csv", SearchOption.AllDirectories);
            
            var data = textLoader.Load(csvFiles);

            Action<DatedScheduleData, DateDifferenceCustomMapping> diffMapping = (input, output) =>
            {
                if (input.ArrivalStatus == "ACTUALISED")
                {
                    var tz = timeZoneConverter.ConvertToDateTimeZoneFromLocalTimeZoneName(
                        input.LOC_TZNAME,
                        input.TerminalCode);

                    if (tz is not null)
                    {
                        output.LocalProformaArrivalEpoch = ConvertToDateTimeOffset(input.ProformaArrival, tz).ToUnixTimeSeconds();
                        output.LocalProformaDepartureEpoch = ConvertToDateTimeOffset(input.ProformaDeparture, tz).ToUnixTimeSeconds();
                        output.LocalScheduledArrivalEpoch = ConvertToDateTimeOffset(input.ScheduledArrival, tz).ToUnixTimeSeconds();
                        output.LocalScheduledDepartureEpoch = ConvertToDateTimeOffset(input.ScheduledDeparture, tz).ToUnixTimeSeconds();
                        output.LocalActualArrivalEpoch = ConvertToDateTimeOffset(input.ActualArrival, tz).ToUnixTimeSeconds();
                        output.LocalActualDepartureEpoch = ConvertToDateTimeOffset(input.ActualDeparture, tz).ToUnixTimeSeconds();
                        output.ActualizedArrivalDifference = output.LocalActualArrivalEpoch - output.LocalProformaArrivalEpoch;
                        output.ActualizedDepartureDifference = output.LocalActualDepartureEpoch - output.LocalProformaDepartureEpoch;
                    }
                }
            };

            var omittedColumns = new[]
            {
                "ScheduleEntryKey",
                "ProformaArrival",
                "ProformaDeparture",
                "ScheduledArrival",
                "ScheduledDeparture",
                "ActualArrival",
                "ActualDeparture",
                "LocationTimeZoneName"
            };

            var categoricalColumnPairs =
                data.Schema
                    .Select(column => column.Name)
                    .Where(columnName => !omittedColumns.Contains(columnName))
                    .Select(columnName => new InputOutputColumnPair("Categorical" + columnName, columnName))
                    .ToArray();

            var featuresColumnNames =
                categoricalColumnPairs
                    .Select(pair => pair.OutputColumnName)
                    .ToList();

            featuresColumnNames.Add("ActualizedArrivalDifference");
            //featuresColumnNames.Add("ActualizedDepartureDifference");

            var pipeline = mlContext.Transforms.CustomMapping(diffMapping, contractName: null)
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(categoricalColumnPairs))
                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    featuresColumnNames.ToArray())
                    //.Append(mlContext.Transforms.NormalizeMinMax("Features"))
                    )
                .Append(mlContext.Transforms.CopyColumns("Label", "ActualizedArrivalDifference"))
                .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(data);

            var engine = mlContext.Model.CreatePredictionEngine<DatedScheduleData, DatedSchedulePrediction>(model);

            var currentDatedSchedule = new DatedScheduleData
            {
                RotationId = "121902",
                IMONumber = "9443463",
                VesselCode = "E8U",
                ArrivalVoyage = "074E",
                DepartureVoyage = "074E",
                ArrivalServiceCode = "MW1",
                ArrivalTransportMode = "MVS",
                DepartureServiceCode = "MW1",
                DepartureTransportMode = "MVS",
                PrevCityCode = "GHTMA",
                PrevTerminalCode = "GHTMAMP",
                prevCityGeoCode = "12LVNL5YRODQ7",
                PrevTerminalGeoCode = "3QD3J3O73O2AH",
                PrevArrivalVoyage = "073W",
                PrevDepartureVoyage = "074E",
                CityCode = "ZACPT",
                TerminalCode = "ZACPT04",
                CityGeoCode = "1YMUUYV0PBYWV",
                TerminalGeoCode = "1V2PP78R82YDB",
                NextCityCode = "ZAPLZ",
                NextTerminalCode = "ZAPLZ02",
                ProformaArrival = "2018-03-29T23:30:00",
                ScheduledArrival = "2018-03-30T05:00:00",
                LOC_TZNAME = "Africa/Johannesburg"
            };

            var prediction = engine.Predict(currentDatedSchedule);

            var predictedActualArrival = GetPredictedActualArrival(
                prediction.PredictedArrivalDifference,
                currentDatedSchedule.TerminalCode,
                currentDatedSchedule.ProformaArrival,
                currentDatedSchedule.LOC_TZNAME);
        }

        private static DateTimeOffset GetPredictedActualArrival(
            float predictedActualArrivalDifference,
            string terminalCode,
            string localProformaArrivalTimestamp,
            string localTimeZoneName)
        {
            var tz = timeZoneConverter.ConvertToDateTimeZoneFromLocalTimeZoneName(
                localTimeZoneName,
                terminalCode);

            var localProformaArrival = ConvertToDateTimeOffset(localProformaArrivalTimestamp, tz);

            var diff = TimeSpan.FromSeconds(predictedActualArrivalDifference);
            return localProformaArrival.Add(diff);
        }

        private static DateTimeOffset ConvertToDateTimeOffset(string localTimestamp, DateTimeZone dateTimeZone)
        {
            const string CsvTimestampFormat = "yyyy-MM-ddTHH:mm:ss";

            if (string.IsNullOrWhiteSpace(localTimestamp) || localTimestamp == "1900-01-01T00:00:00")
            {
                return DateTimeOffset.MinValue;
            }

            var pattern = LocalDateTimePattern.CreateWithInvariantCulture(CsvTimestampFormat);
            var parseResult = pattern.Parse(localTimestamp);

            if (parseResult.Success)
            {
                var localDateTime = parseResult.Value;

                var zoneDateTime = localDateTime.InZoneLeniently(dateTimeZone);
                var localTimeWithOffset = zoneDateTime.ToDateTimeOffset();

                return localTimeWithOffset;
            }
            else
            {
                return DateTimeOffset.MinValue;
            }
        }
    }
}

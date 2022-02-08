namespace DatedSchedules.Predictions.Models
{
    using NodaTime;
    using System;

    public interface ITimeZoneConverter
    {
        DateTimeOffset ConvertToDateTimeOffset(string localTimestamp, DateTimeZone dateTimeZone);

        DateTimeZone? ConvertToDateTimeZoneFromLocalTimeZoneName(
            string locationTimeZoneName,
            string terminalCode);
    }
}

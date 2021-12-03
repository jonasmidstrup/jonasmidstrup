namespace DatedSchedules.Predictions.Models
{
    using NodaTime;

    public interface ITimeZoneConverter
    {
        DateTimeZone? ConvertToDateTimeZoneFromLocalTimeZoneName(
            string locationTimeZoneName,
            string terminalCode);
    }
}

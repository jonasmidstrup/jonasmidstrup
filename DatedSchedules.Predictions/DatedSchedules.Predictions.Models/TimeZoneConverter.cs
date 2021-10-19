namespace DatedSchedules.Predictions.Models
{
    using System;
    using NodaTime;

    public class TimeZoneConverter : ITimeZoneConverter
    {
        private readonly IDateTimeZoneProvider _dateTimeZoneProvider;

        public TimeZoneConverter(IDateTimeZoneProvider dateTimeZoneProvider)
        {
            _dateTimeZoneProvider = dateTimeZoneProvider ?? throw new ArgumentNullException(nameof(dateTimeZoneProvider));
        }

        public DateTimeZone? ConvertToDateTimeZoneFromLocalTimeZoneName(
            string locationTimeZoneName,
            string terminalCode)
        {
            var effectiveLocalTimeZoneName = locationTimeZoneName;
            if (string.IsNullOrWhiteSpace(effectiveLocalTimeZoneName))
            {
                effectiveLocalTimeZoneName = PatchAndMapLocalTimeZoneNameFromTerminalCode(terminalCode);

                if (string.IsNullOrWhiteSpace(effectiveLocalTimeZoneName))
                {
                    return null;
                }
            }

            var dateTimeZone = _dateTimeZoneProvider.GetZoneOrNull(effectiveLocalTimeZoneName);

            if (dateTimeZone is null)
            {
                effectiveLocalTimeZoneName = PatchAndMapLocalTimeZoneNameFromTerminalCode(terminalCode);

                if (string.IsNullOrWhiteSpace(effectiveLocalTimeZoneName))
                {
                    return null;
                }
                else
                {
                    dateTimeZone = _dateTimeZoneProvider.GetZoneOrNull(effectiveLocalTimeZoneName);
                }
            }

            return dateTimeZone;
        }

        private string PatchAndMapLocalTimeZoneNameFromTerminalCode(string terminalCode)
        {
            if (string.IsNullOrWhiteSpace(terminalCode))
            {
                return string.Empty;
            }

            if (terminalCode.Equals("CNFOOJY", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("CNSGHY1", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("CNSGHY3", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("CNSHECT", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("CNTSTQQ", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("CNXIMSY", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("CNYATCT", StringComparison.OrdinalIgnoreCase))
            {
                return "Asia/Shanghai";
            }

            if (terminalCode.Equals("DKGSITM", StringComparison.OrdinalIgnoreCase))
            {
                return "Europe/Copenhagen";
            }

            if (terminalCode.Equals("ESALR04", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("ESVGOTM", StringComparison.OrdinalIgnoreCase))
            {
                return "Europe/Madrid";
            }

            if (terminalCode.Equals("INJHTTM", StringComparison.OrdinalIgnoreCase))
            {
                return "Asia/Kolkata";
            }

            if (terminalCode.Equals("KRKWYTM", StringComparison.OrdinalIgnoreCase))
            {
                return "Asia/Seoul";
            }

            if (terminalCode.Equals("MAPTM01", StringComparison.OrdinalIgnoreCase))
            {
                return "Africa/Casablanca";
            }

            if (terminalCode.Equals("NOAUSTM", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("NOHLDTM", StringComparison.OrdinalIgnoreCase))
            {
                return "Europe/Oslo";
            }

            if (terminalCode.Equals("RERUN30P", StringComparison.OrdinalIgnoreCase))
            {
                return "Indian/Reunion";
            }

            if (terminalCode.Equals("RUO8JTM", StringComparison.OrdinalIgnoreCase))
            {
                return "Asia/Sakhalin";
            }

            if (terminalCode.Equals("TWKAOOD", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("TWKAOTM", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("TWTPEOD", StringComparison.OrdinalIgnoreCase))
            {
                return "Asia/Taipei";
            }

            if (terminalCode.Equals("USHOUBP", StringComparison.OrdinalIgnoreCase))
            {
                return "America/Chicago";
            }

            if (terminalCode.Equals("USILMCT", StringComparison.OrdinalIgnoreCase) ||
                terminalCode.Equals("USJAXBL", StringComparison.OrdinalIgnoreCase))
            {
                return "America/New_York";
            }

            return string.Empty;
        }
    }
}

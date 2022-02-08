namespace DatedSchedules.Predictions.Models
{
    using CsvHelper.Configuration.Attributes;

    public record DatedSchedule
    {
        [Name("LAST_UPDATED_TS")]
        public string LastUpdatedTimestamp { get; set; }

        public string ScheduleEntryKey { get; init; }

        public string RotationId { get; init; }

        public string RotationName { get; init; }

        public string RotationVersion { get; init; }

        [Name("IMONumber")]
        public string ImoNumber { get; init; }

        public string VesselCode { get; init; }

        public string VesselName { get; init; }

        public string VesselOperatorCode { get; init; }

        public string VesselFlag { get; init; }

        public string VesselCallSign { get; init; }

        public string CityCode { get; init; }

        public string TerminalCode { get; init; }

        public string CityName { get; init; }

        public string TerminalName { get; init; }

        public string CityGeoCode { get; init; }

        public string TerminalGeoCode { get; init; }

        public string ProformaArrival { get; init; }

        public string ProformaDeparture { get; init; }

        public string ScheduledArrival { get; init; }

        public string ScheduledDeparture { get; init; }

        public string ActualArrival { get; init; }

        public string ActualDeparture { get; init; }

        [Name("LOC_TZNAME")]
        public string LocationTimeZoneName { get; init; }

        public string ArrivalAtPilotStation { get; init; }

        public string FirstPilotOnBoard { get; init; }

        public string PilotOff { get; init; }

        public string DummyCall { get; init; }

        public string DepartureStatus { get; init; }

        public string ArrivalStatus { get; init; }

        public string OmitReason { get; init; }

        public string ArrivalVoyage { get; init; }

        public string ArrivalVoyageDirection { get; init; }

        public string DepartureVoyage { get; init; }

        public string DepartureVoyageDirection { get; init; }

        public string DepartureTransportMode { get; init; }

        [Name("NOTE")]
        public string Note { get; init; }

        public string DepartureServiceCode { get; init; }

        public string DepartureServiceName { get; init; }

        public string UpdatedBy { get; init; }

        public string ArrivalServiceCode { get; init; }

        public string ArrivalServiceName { get; init; }

        public string ArrivalTransportMode { get; init; }

        [Name("prevScheduleEntryKey")]
        public string PreviousScheduleEntryKey { get; init; }

        [Name("PrevDepartureVoyage")]
        public string PreviousDepartureVoyage { get; init; }

        [Name("PrevArrivalVoyage")]
        public string PreviousArrivalVoyage { get; init; }

        [Name("PrevCityCode")]
        public string PreviousCityCode { get; init; }

        [Name("PrevTerminalCode")]
        public string PreviousTerminalCode { get; init; }

        [Name("PrevCityName")]
        public string PreviousCityName { get; init; }

        [Name("PrevTerminalName")]
        public string PreviousTerminalName { get; init; }

        [Name("PrevTerminalGeoCode")]
        public string PreviousTerminalGeoCode { get; init; }

        [Name("prevCityGeoCode")]
        public string PreviousCityGeoCode { get; init; }

        public string NextScheduleEntryKey { get; init; }

        public string NextDepartureVoyage { get; init; }

        public string NextArrivalVoyage { get; init; }

        public string NextCityCode { get; init; }

        public string NextTerminalCode { get; init; }

        public string NextCityName { get; init; }

        public string NextTerminalName { get; init; }

        public string NextTerminalGeoCode { get; init; }

        public string NextCityGeoCode { get; init; }
    }
}
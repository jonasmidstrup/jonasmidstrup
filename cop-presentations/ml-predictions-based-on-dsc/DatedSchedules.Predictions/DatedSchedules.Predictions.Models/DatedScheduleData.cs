using CsvHelper.Configuration.Attributes;
using Microsoft.ML.Data;

namespace DatedSchedules.Predictions.Models
{
    public record DatedScheduleData
    {
        [LoadColumn(0)]
        public string RotationId { get; set; }

        //[LoadColumn(1)]
        //public string RotationName { get; set; }

        //[LoadColumn(2)]
        //public string RotationVersion { get; set; }

        //[LoadColumn(3)]
        //public string ScheduleEntryKey { get; set; }

        [LoadColumn(4)]
        [ColumnName("ImoNumber")]
        public string IMONumber { get; set; }

        //[LoadColumn(5)]
        //public string VesselName { get; set; }

        //[LoadColumn(6)]
        //public string VesselOperatorCode { get; set; }

        //[LoadColumn(7)]
        //public string VesselFlag { get; set; }

        //[LoadColumn(8)]
        //public string VesselCallSign { get; set; }

        [LoadColumn(9)]
        public string VesselCode { get; set; }

        [LoadColumn(10)]
        public string ArrivalVoyage { get; set; }

        //[LoadColumn(11)]
        //public string ArrivalVoyageDirection { get; set; }

        [LoadColumn(12)]
        public string DepartureVoyage { get; set; }

        //[LoadColumn(13)]
        //public string DepartureVoyageDirection { get; set; }

        [LoadColumn(14)]
        public string ArrivalServiceCode { get; set; }

        //[LoadColumn(15)]
        //public string ArrivalServiceName { get; set; }

        [LoadColumn(16)]
        public string ArrivalTransportMode { get; set; }

        [LoadColumn(17)]
        public string DepartureServiceCode { get; set; }

        //[LoadColumn(18)]
        //public string DepartureServiceName { get; set; }

        [LoadColumn(19)]
        public string DepartureTransportMode { get; set; }

        [LoadColumn(20)]
        [ColumnName("PreviousCityCode")]
        public string PrevCityCode { get; set; }

        [LoadColumn(21)]
        [ColumnName("PreviousTerminalCode")]
        public string PrevTerminalCode { get; set; }

        //[LoadColumn(22)]
        //[ColumnName("PreviousCityName")]
        //public string PrevCityName { get; set; }

        //[LoadColumn(23)]
        //[ColumnName("PreviousTerminalName")]
        //public string PrevTerminalName { get; set; }

        [LoadColumn(24)]
        [ColumnName("PreviousCityGeoCode")]
        public string prevCityGeoCode { get; set; }

        [LoadColumn(25)]
        [ColumnName("PreviousTerminalGeoCode")]
        public string PrevTerminalGeoCode { get; set; }

        //[LoadColumn(26)]
        //[ColumnName("PreviousScheduleEntryKey")]
        //public string prevScheduleEntryKey { get; set; }

        [LoadColumn(27)]
        [ColumnName("PreviousArrivalVoyage")]
        public string PrevArrivalVoyage { get; set; }

        [LoadColumn(28)]
        [ColumnName("PreviousDepartureVoyage")]
        public string PrevDepartureVoyage { get; set; }

        [LoadColumn(29)]
        public string CityCode { get; set; }

        [LoadColumn(30)]
        public string TerminalCode { get; set; }

        //[LoadColumn(31)]
        //public string CityName { get; set; }

        //[LoadColumn(32)]
        //public string TerminalName { get; set; }

        [LoadColumn(33)]
        public string CityGeoCode { get; set; }

        [LoadColumn(34)]
        public string TerminalGeoCode { get; set; }

        [LoadColumn(35)]
        public string NextCityCode { get; set; }

        [LoadColumn(36)]
        public string NextTerminalCode { get; set; }

        //[LoadColumn(37)]
        //public string NextCityName { get; set; }

        //[LoadColumn(38)]
        //public string NextTerminalName { get; set; }

        [LoadColumn(39)]
        public string NextCityGeoCode { get; set; }

        [LoadColumn(40)]
        public string NextTerminalGeoCode { get; set; }

        //[LoadColumn(41)]
        //public string NextScheduleEntryKey { get; set; }

        [LoadColumn(42)]
        public string NextArrivalVoyage { get; set; }

        [LoadColumn(43)]
        public string NextDepartureVoyage { get; set; }

        [LoadColumn(44)]
        public string ArrivalStatus { get; set; }

        [LoadColumn(45)]
        public string DepartureStatus { get; set; }

        [LoadColumn(49)]
        public string ProformaArrival { get; set; }

        [LoadColumn(50)]
        public string ProformaDeparture { get; set; }

        [LoadColumn(51)]
        public string ScheduledArrival { get; set; }

        [LoadColumn(52)]
        public string ScheduledDeparture { get; set; }

        //[LoadColumn(53)]
        //public string DummyCall { get; set; }

        //[LoadColumn(54)]
        //public string OmitReason { get; set; }

        [LoadColumn(55)]
        public string ActualArrival { get; set; }

        [LoadColumn(56)]
        public string ActualDeparture { get; set; }

        //[LoadColumn(57)]
        //public string ArrivalAtPilotStation { get; set; }

        //[LoadColumn(58)]
        //public string FirstPilotOnBoard { get; set; }

        //[LoadColumn(59)]
        //public string PilotOff { get; set; }

        //[LoadColumn(60)]
        //[ColumnName("Note")]
        //public string NOTE { get; set; }

        //[LoadColumn(61)]
        //public string UpdatedBy { get; set; }

        [LoadColumn(62)]
        [ColumnName("LocationTimeZoneName")]
        public string LOC_TZNAME { get; set; }

        //[LoadColumn(63)]
        //[ColumnName("LastUpdatedTimestamp")]
        //public string LAST_UPDATED_TS { get; set; }
    }
}

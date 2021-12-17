using Microsoft.ML.Data;

namespace DatedSchedules.Predictions.Models
{
    public class SanitizedDatedSchedule
    {
        [LoadColumn(0)]
        public string VesselCode { get; set; }

        [LoadColumn(1)]
        public string ArrivalServiceCode { get; set; }

        [LoadColumn(2)]
        public string PreviousTerminalCode { get; set; }

        [LoadColumn(3)]
        public string TerminalCode { get; set; }

        [LoadColumn(4)]
        public string ProformaArrival { get; set; }

        [LoadColumn(5)]
        public string ProformaDeparture { get; set; }

        [LoadColumn(6)]
        public string ActualArrival { get; set; }

        [LoadColumn(7)]
        public string ActualDeparture { get; set; }

        [LoadColumn(8)]
        public float ActualizedArrivalDifference { get; set; }

        [LoadColumn(9)]
        public float ActualizedDepartureDifference { get; set; }
    }
}

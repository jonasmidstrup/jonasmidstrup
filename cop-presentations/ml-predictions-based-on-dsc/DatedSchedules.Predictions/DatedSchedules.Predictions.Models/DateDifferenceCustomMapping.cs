using Microsoft.ML.Data;

namespace DatedSchedules.Predictions.Models
{
    public class DateDifferenceCustomMapping
    {
        public long LocalProformaArrivalEpoch { get; set; }

        public long LocalProformaDepartureEpoch { get; set; }

        public long LocalScheduledArrivalEpoch { get; set; }

        public long LocalScheduledDepartureEpoch { get; set; }

        public long LocalActualArrivalEpoch { get; set; }

        public long LocalActualDepartureEpoch { get; set; }

        public float ActualizedArrivalDifference { get; set; }

        public float ActualizedDepartureDifference { get; set; }
    }
}

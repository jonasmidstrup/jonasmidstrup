using Microsoft.ML.Data;

namespace DatedSchedules.Predictions.Models
{
    public class DatedSchedulePrediction
    {
        [ColumnName("Score")]
        public float PredictedArrivalDifference { get; set; }
    }
}

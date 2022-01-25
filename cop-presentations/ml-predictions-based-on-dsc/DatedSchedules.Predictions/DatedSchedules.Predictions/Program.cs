using DatedSchedules.Predictions.Models;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;

namespace DatedSchedules.Predictions
{
    internal class Program
    {
        // https://www.red-gate.com/simple-talk/development/data-science-development/insurance-price-prediction-using-machine-learning-ml-net/
        // https://github.com/dotnet/machinelearning/blob/main/docs/samples/Microsoft.ML.Samples/Dynamic/Trainers/Regression/FastForest.cs
        // https://aws.amazon.com/blogs/machine-learning/using-machine-learning-to-predict-vessel-time-of-arrival-with-amazon-sagemaker/
        public static void Main(string[] args)
        {
            var mlContext = new MLContext();

            var textLoader = mlContext.Data.CreateTextLoader<SanitizedDatedSchedule>(
                separatorChar: ',',
                hasHeader: true,
                allowQuoting: true,
                trimWhitespace: true);

            var csvFiles = Directory.GetFiles(
                @"C:\Temp\GSIS_sanitized_data",
                "*.csv",
                SearchOption.AllDirectories);
            
            var data = textLoader.Load(csvFiles);

            //GetBestModel(mlContext, data);

            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(new[]
                {
                    new InputOutputColumnPair("CategoricalVesselCode", "VesselCode"),
                    new InputOutputColumnPair("CategoricalArrivalServiceCode", "ArrivalServiceCode"),
                    new InputOutputColumnPair("CategoricalPreviousTerminalCode", "PreviousTerminalCode"),
                    new InputOutputColumnPair("CategoricalTerminalCode", "TerminalCode"),
                    new InputOutputColumnPair("CategoricalMinimumCruisingSpeed", "MinimumCruisingSpeed"),
                    new InputOutputColumnPair("CategoricalMaximumCruisingSpeed", "MaximumCruisingSpeed"),
                })
                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    new[]
                    {
                        "CategoricalVesselCode",
                        "CategoricalArrivalServiceCode",
                        "CategoricalPreviousTerminalCode",
                        "CategoricalTerminalCode",
                        "CategoricalMinimumCruisingSpeed",
                        "CategoricalMaximumCruisingSpeed",
                        "ActualizedArrivalDifference"
                    }));

            var trainingPipeline = dataProcessPipeline.Append(
                mlContext.Regression.Trainers.FastForest(labelColumnName: "ActualizedArrivalDifference"));

            var model = trainingPipeline.Fit(data);

            var crossValidationResults = mlContext.Regression.CrossValidate(
                data,
                trainingPipeline,
                numberOfFolds: 100,
                labelColumnName: "ActualizedArrivalDifference");

            var bestMetric = crossValidationResults.OrderByDescending(cvr => cvr.Metrics.RSquared).FirstOrDefault();
            Console.WriteLine($"Best metric when validating: {bestMetric.Metrics.RSquared}");
            
            var engine = mlContext.Model.CreatePredictionEngine<SanitizedDatedSchedule, DatedSchedulePrediction>(model);

            var exampleDatedSchedule = new SanitizedDatedSchedule
            {
                VesselCode = "1QM",
                ArrivalServiceCode = "432",
                TerminalCode = "EGSUCCN",
                PreviousTerminalCode = "MYTPPTM",
                ProformaArrival = "2021-01-20T06:20:00.0000000+02:00"
            };

            var prediction = engine.Predict(exampleDatedSchedule);

            var predictedActualArrival = GetPredictedActualArrival(
                prediction.PredictedArrivalDifference,
                DateTimeOffset.Parse(exampleDatedSchedule.ProformaArrival));
        }

        private static void GetBestModel(MLContext mLContext, IDataView dataView)
        {
            var settings = new RegressionExperimentSettings { };

            var experiment = mLContext.Auto().CreateRegressionExperiment(settings);

            var progress = new Progress<RunDetail<RegressionMetrics>>(p =>
            {
                if (p.ValidationMetrics is not null)
                {
                    Console.Write($"Current result: {p.TrainerName}");
                    Console.Write($"      {p.ValidationMetrics.RSquared}");
                    Console.Write($"      {p.ValidationMetrics.MeanAbsoluteError}");
                    Console.WriteLine();
                }
            });

            var result = experiment.Execute(
                dataView,
                labelColumnName: "ActualizedArrivalDifference",
                progressHandler: progress);
        }

        private static DateTimeOffset GetPredictedActualArrival(
            float predictedActualArrivalDifference,
            DateTimeOffset localProformaArrivalTimestamp)
        {
            var diff = TimeSpan.FromSeconds(predictedActualArrivalDifference);
            return localProformaArrivalTimestamp.Add(diff);
        }
    }
}

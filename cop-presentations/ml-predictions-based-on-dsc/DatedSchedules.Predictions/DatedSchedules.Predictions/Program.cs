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
                })
                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    new[]
                    {
                        "CategoricalVesselCode",
                        "CategoricalArrivalServiceCode",
                        "CategoricalPreviousTerminalCode",
                        "CategoricalTerminalCode",
                        "ActualizedArrivalDifference"
                    }));

            var trainer = dataProcessPipeline
                .Append(mlContext.Regression.Trainers.LightGbm(labelColumnName: "ActualizedArrivalDifference"));

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            var model = trainingPipeline.Fit(data);

            var crossValidationResults = mlContext.Regression.CrossValidate(
                data,
                trainingPipeline,
                numberOfFolds: 5,
                labelColumnName: "ActualizedArrivalDifference");
            
            var engine = mlContext.Model.CreatePredictionEngine<SanitizedDatedSchedule, DatedSchedulePrediction>(model);

            var currentDatedSchedule = new SanitizedDatedSchedule
            {
                VesselCode = "2AM",
                ArrivalServiceCode = "432",
                TerminalCode = "EGSUCCN",
                PreviousTerminalCode = "MYTPPTM",
                ProformaArrival = "2022-01-20T06:20:00.0000000+02:00"
            };

            var prediction = engine.Predict(currentDatedSchedule);

            var predictedActualArrival = GetPredictedActualArrival(
                prediction.PredictedArrivalDifference,
                DateTimeOffset.Parse(currentDatedSchedule.ProformaArrival));
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

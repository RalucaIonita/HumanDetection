using Microsoft.ML;
using Microsoft.ML.Vision;
using System;

namespace HumanDetectionModel
{
    class Program
    {
        static void Main(string[] args)
        {
            var mlContext = new MLContext();

            var imageData = mlContext.Data.LoadFromEnumerable(DataLoader.Data);
            var shuffledData = mlContext.Data.ShuffleRows(imageData);

            var preprocessingPipeline = mlContext.Transforms.Conversion.MapKeyToValue(
                    inputColumnName: "Label",
                    outputColumnName: "LabelAsKey")
                .Append(mlContext.Transforms.LoadRawImageBytes(
                    outputColumnName: "Image",
                    imageFolder: @"C:\Users\raluc\source\repos\HumanDetection\data",
                    inputColumnName: "ImagePath"
                    ));

            var preProcessedData = preprocessingPipeline
                .Fit(shuffledData)
                .Transform(shuffledData);

            //split
            var trainSplit = mlContext.Data.TrainTestSplit(preProcessedData, 0.3);
            var validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);

            var trainData = trainSplit.TrainSet;
            var validationData = validationTestSplit.TrainSet;
            var testData = validationTestSplit.TestSet;

            var classifierOptions = new ImageClassificationTrainer.Options()
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                ValidationSet = validationData,
                Arch = ImageClassificationTrainer.Architecture.ResnetV2101,
                MetricsCallback = (metrics) => Console.WriteLine(metrics),
                TestOnTrainSet = false,
                ReuseTrainSetBottleneckCachedValues = true,
                ReuseValidationSetBottleneckCachedValues = true
            };

            var trainingPipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(classifierOptions)
    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            ITransformer trainedModel = trainingPipeline.Fit(trainData);


            Console.WriteLine("Hello World!");
        }
    }
}

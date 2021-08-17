using Microsoft.ML;
using NeuralNetwork_1.Models;
using System.Collections.Generic;
using Microsoft.ML.Vision;
using System;
using static Microsoft.ML.DataOperationsCatalog;

namespace NeuralNetwork_1
{
    public class Model
    {
        public MLContext MLContext { get; set; }
        public List<ModelInput> Input { get; set; }
        public List<ModelOutput> Output { get; set; }
        private IDataView Data { get; set; }
        private IDataView ShuffledData { get; set; }
        public IDataView TrainData { get; set; }
        public IDataView ValidationData { get; set; }
        public IDataView TestData { get; set; }
        public ImageClassificationTrainer.Options ClassifierOptions { get; set; }
        public ITransformer TrainedModel { get; set; }

        public Model(List<ModelInput> input)
        {
            Input = input;
            MLContext = new MLContext();
            Data = MLContext.Data.LoadFromEnumerable(Input);
            ShuffledData = MLContext.Data.ShuffleRows(Data);
        }

        //public void SplitData(double fraction)
        //{
        //    var trainTestData = MLContext.Data.TrainTestSplit(ShuffledData, fraction);
        //    TrainData = trainTestData.TrainSet;
        //    var validationTestData = MLContext.Data.TrainTestSplit(trainTestData.TestSet);
        //    ValidationData = validationTestData.TrainSet;
        //    TestData = validationTestData.TestSet;
        //}

        public void BuildTrainer()
        {
            var preprocessingPipeline = MLContext.Transforms.Conversion
                .MapValueToKey(
                inputColumnName: "Label",
                outputColumnName: "LabelAsKey")
                .Append(MLContext.Transforms.LoadRawImageBytes(outputColumnName: "Image",
                                                               imageFolder: @"C:\Users\raluc\source\repos\HumanDetection\data",
                                                               inputColumnName: "ImagePath"));
            IDataView preProcessedData = preprocessingPipeline
                    .Fit(ShuffledData)
                    .Transform(ShuffledData);

            TrainTestData trainSplit = MLContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
            TrainTestData validationTestSplit = MLContext.Data.TrainTestSplit(trainSplit.TestSet);

            TrainData = trainSplit.TrainSet;
            ValidationData = validationTestSplit.TrainSet;
            TestData = validationTestSplit.TestSet;
            //var preprocessingPipeline = mlContext.Transforms.Conversion
            //.Append(MLContext.Transforms.LoadRawImageBytes(
            //    outputColumnName: "Image",
            //    imageFolder: assetsRelativePath,
            //    inputColumnName: "ImagePath"));

            //IDataView preProcessedData = preprocessingPipeline
            //        .Fit(shuffledData)
            //        .Transform(shuffledData);
            ClassifierOptions =  new ImageClassificationTrainer.Options()
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                ValidationSet = ValidationData,
                Arch = ImageClassificationTrainer.Architecture.ResnetV2101,
                MetricsCallback = (metrics) => Console.WriteLine(metrics),
                TestOnTrainSet = false,
                ReuseTrainSetBottleneckCachedValues = true,
                ReuseValidationSetBottleneckCachedValues = true
            };
        }

        public void Train()
        {
            var trainingPipeline = MLContext.MulticlassClassification.Trainers.ImageClassification(ClassifierOptions)
    .Append(MLContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            TrainedModel = trainingPipeline.Fit(TrainData);
        }

        public void OutputPrediction(ModelOutput prediction)
        {
            string imageName = prediction.ImagePath;
            Console.WriteLine($"Image: {imageName} | Actual Value: {prediction.Label} | Predicted Value: {prediction.PredictedLabel}");
        }

    }
}

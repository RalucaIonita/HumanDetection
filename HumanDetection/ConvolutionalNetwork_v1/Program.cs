using Microsoft.ML;
using NeuralNetwork_1;
using NeuralNetwork_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace NeuralNetwork_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\raluc\source\repos\HumanDetection\data";
            var inputData = Helper.LoadData(path);

            var model = new Model(inputData);
            //model.SplitData(0.3);
            model.BuildTrainer();
            model.Train();
            var predictions = ClassifyImages(model.MLContext, model.TestData, model.TrainedModel);
            foreach (var prediction in predictions)
            {
                model.OutputPrediction(prediction);
            }


        }

        public static IEnumerable<ModelOutput> ClassifyImages(MLContext mlContext, IDataView data, ITransformer trainedModel)
        {
            var predictionData = trainedModel.Transform(data);
            var predictions = mlContext.Data.CreateEnumerable<ModelOutput>(predictionData, true);
            Console.WriteLine("Classifying multiple images");
            return predictions;
            
        }
    }
}

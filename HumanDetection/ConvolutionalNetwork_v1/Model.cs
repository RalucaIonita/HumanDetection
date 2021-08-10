using Microsoft.ML;
using NeuralNetwork_1.Models;
using System.Collections.Generic;

namespace NeuralNetwork_1
{
    public class Model
    {
        public MLContext MLContext { get; set; }
        public List<ModelInput> Input { get; set; }
        public List<ModelOutput> Output { get; set; }
        private IDataView Data { get; set; }
        private IDataView ShuffledData { get; set; }

        public Model(List<ModelInput> input)
        {
            Input = input;
            MLContext = new MLContext();
            Data = MLContext.Data.LoadFromEnumerable(Input);
            ShuffledData = MLContext.Data.ShuffleRows(Data);
        }


    }
}

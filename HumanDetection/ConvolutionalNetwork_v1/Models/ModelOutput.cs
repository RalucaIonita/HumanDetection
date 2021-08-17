using System;

namespace NeuralNetwork_1.Models
{
    public class ModelOutput
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Label { get; set; }
        public bool PredictedLabel { get; set; }
        public string ImagePath { get; set; }
    }
}

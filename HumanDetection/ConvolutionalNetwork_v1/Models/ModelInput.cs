using System;

namespace NeuralNetwork_1.Models
{
    public class ModelInput
    {
        //public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public int Label { get; set; } //true -> is human; false -> not human
        public int LabelAsKey { get; set; }
        public string ImagePath { get; set; }
    }
}

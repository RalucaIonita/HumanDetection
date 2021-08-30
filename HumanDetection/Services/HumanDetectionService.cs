using HumanDetectionML.Model;
using System;
using System.IO;
using System.Linq;

namespace Services
{
    public interface IHumanDetectionService
    {
        bool RecognizeHuman(byte[] content, string name);
    }
    public class HumanDetectionService : IHumanDetectionService
    {
        public bool RecognizeHuman(byte[] content, string name)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, name);
            File.WriteAllBytes(filePath, content);

            var modelInput = new ModelInput
            {
                ImageSource = filePath
            };
            var result = ConsumeModel.Predict(modelInput);
            Console.WriteLine(path);
            Console.WriteLine($"Predicted {result.Prediction}");
            result.Score.ToList().ForEach(r => Console.WriteLine($"{r} "));

            var chanceToBeHuman = result.Score[0];
            var chanceToNotBeHuman = result.Score[1];

            if (chanceToBeHuman > chanceToNotBeHuman)
                return true;
            return false;
        }
    }
}

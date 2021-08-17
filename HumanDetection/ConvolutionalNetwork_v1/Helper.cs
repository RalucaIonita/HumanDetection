using NeuralNetwork_1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetwork_1
{
    public static class Helper
    {
        public static List<ModelInput> LoadData(string path)
        {
            var entries = new List<ModelInput>();

            var directories = Directory.GetDirectories(path).ToList();
            var humansPath = Path.Combine(path, directories[1]);
            var notHumansPath = Path.Combine(path, directories[0]);
            Console.WriteLine($"Humans path: {humansPath}");
            Console.WriteLine($"Not humans path: {notHumansPath}");

            var humans = Directory.GetFiles(humansPath).ToList();
            var notHumans = Directory.GetFiles(notHumansPath).ToList();

            foreach(var human in humans)
            {
                var newEntry = new ModelInput
                {
                    Content = File.ReadAllBytes(human),
                    Label = 1,
                    ImagePath = human
                };
                entries.Add(newEntry);
            }

            foreach (var notHuman in notHumans)
            {
                var newEntry = new ModelInput
                {
                    Content = File.ReadAllBytes(notHuman),
                    Label = 0,
                };
                entries.Add(newEntry);
            }

            return entries;
        }
    }
}

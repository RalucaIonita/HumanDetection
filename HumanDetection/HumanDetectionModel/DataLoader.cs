using HumanDetectionModel.Entities;
using System.Collections.Generic;
using System.IO;

namespace HumanDetectionModel
{
    public static class DataLoader
    {
        public static List<ImageData> Data { get; set; }

        static DataLoader()
        {
            Data = new List<ImageData>();
            //load data
            var dataDirectory = @"C:\Users\raluc\source\repos\HumanDetection\data";
            var humanData = @"C:\Users\raluc\source\repos\HumanDetection\data\1";
            var notHumanData = @"C:\Users\raluc\source\repos\HumanDetection\data\0";

            var humanFiles = Directory.GetFiles(humanData);
            var notHumanFiles = Directory.GetFiles(notHumanData);

            foreach (var human in humanFiles)
            {
                var data = new ImageData
                {
                    ImagePath = Path.Combine(humanData, human),
                    Label = "human"
                };
                Data.Add(data);
            }

            foreach (var notHuman in notHumanFiles)
            {
                var data = new ImageData
                {
                    ImagePath = Path.Combine(humanData, notHuman),
                    Label = "not-human"
                };
                Data.Add(data);
            }
        }
    }
}

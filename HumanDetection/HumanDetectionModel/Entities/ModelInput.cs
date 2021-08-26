namespace HumanDetectionModel.Entities
{
    public class ModelInput : ImageData
    {
        public int LabelAsKey { get; set; }
        public byte[] Image { get; set; }
    }
}

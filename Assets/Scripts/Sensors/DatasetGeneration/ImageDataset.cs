using System.IO;

namespace Assets.Scripts.Sensors.DatasetGeneration
{
    public sealed class ImageDataset
    {
        private readonly string _pathToDirectory;

        public ImageDataset(string topic)
        {
            _pathToDirectory = DirectoryInitializer.Initialize(topic, "Dataset");
        }

        public void AddImage(uint seq, byte[] data)
        {
            string pathToFile = Path.Combine(_pathToDirectory, $"{seq}.jpg");
            File.WriteAllBytes(pathToFile, data);
        }
    }
}

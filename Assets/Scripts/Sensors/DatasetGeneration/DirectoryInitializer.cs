using System;
using System.IO;

namespace Assets.Scripts.Sensors.DatasetGeneration
{
    public static class DirectoryInitializer
    {
        public static string Initialize(string topic)
        {
            string pathToDirectory = Environment.CurrentDirectory;
            pathToDirectory = Path.Combine(pathToDirectory, "Dataset");

            string[] folders = topic.Split('/', '\\');
            foreach (string folder in folders)
            {
                pathToDirectory = Path.Combine(pathToDirectory, folder);

                if (Directory.Exists(pathToDirectory))
                {
                    continue;
                }

                Directory.CreateDirectory(pathToDirectory);
            }

            return pathToDirectory;
        }
    }
}

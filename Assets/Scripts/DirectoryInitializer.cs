using System;
using System.IO;

namespace Assets.Scripts
{
    public static class DirectoryInitializer
    {
        public static string Initialize(string name, string root)
        {
            string pathToDirectory = Environment.CurrentDirectory;
            pathToDirectory = Path.Combine(pathToDirectory, root);

            string[] folders = name.Split('/', '\\');
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

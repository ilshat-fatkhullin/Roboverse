using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public class SpawnAreaStorage : ISpawnAreaStorage
    {
        private readonly ISpawnArea _spawnArea;

        private readonly string _directory;

        public SpawnAreaStorage(ISpawnArea spawnArea)
        {
            _spawnArea = spawnArea;

            Scene scene = SceneManager.GetActiveScene();
            _directory = DirectoryInitializer.Initialize(scene.name, "SpawnAreas");
        }

        public void Load()
        {
            string path = EditorUtility.OpenFilePanel("Load spawn area", _directory, "sa");
            
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            foreach (SpawnBox box in _spawnArea.Boxes)
            {
                _spawnArea.RemoveBox(box);
            }

            using MemoryStream stream = new(File.ReadAllBytes(path));
            using BinaryReader reader = new(stream);

            _spawnArea.Origin = new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            _spawnArea.Step = reader.ReadSingle();

            uint count = reader.ReadUInt32();

            for (int i = 0; i < count; i++)
            {
                SpawnBox box = new(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                _spawnArea.AddBox(box);
            }
        }

        public void Save()
        {
            string path = EditorUtility.SaveFilePanel("Save spawn area", _directory, "SpawnArea", "sa");

            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            
            using FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
            using BinaryWriter writer = new(stream);

            writer.Write(_spawnArea.Origin.x);
            writer.Write(_spawnArea.Origin.y);
            writer.Write(_spawnArea.Origin.z);

            writer.Write(_spawnArea.Step);

            writer.Write(_spawnArea.Boxes.Count);

            foreach (SpawnBox box in _spawnArea.Boxes)
            {
                writer.Write(box.X);
                writer.Write(box.Y);
                writer.Write(box.Z);
            }
        }
    }
}
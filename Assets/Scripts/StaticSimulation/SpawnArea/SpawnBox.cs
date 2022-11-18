using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public class SpawnBox
    {
        public readonly int X;

        public readonly int Y;

        public readonly int Z;

        public SpawnBox(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public SpawnBox(SpawnBox box)
        {
            X = box.X;
            Y = box.Y;
            Z = box.Z;
        }

        public Vector3 GetPosition(float step) => 
            new Vector3(
                step * (X + 0.5f),
                step * (Y + 0.5f),
                step * (Z + 0.5f));

        public Vector3 CreateSpawnPosition(float step)
        {
            float x = step * (X + Random.Range(0f, 1f));
            float y = step * (Y + Random.Range(0f, 1f));
            float z = step * (Z + Random.Range(0f, 1f));

            return new Vector3(x, y, z);
        }

        public IReadOnlyCollection<SpawnBox> GetNeighboringBoxes() =>
            new List<SpawnBox>
            {
                new(X + 1, Y, Z),
                new(X - 1, Y, Z),
                new(X, Y + 1, Z),
                new(X, Y - 1, Z),
                new(X, Y, Z + 1),
                new(X, Y, Z - 1),
            };

        public override string ToString()
        {
            return $"(X = {X}, Y = {Y}, Z = {Z})";
        }
    }

    public sealed class SpawnBoxEquialityComparer : IEqualityComparer<SpawnBox>
    {
        public bool Equals(SpawnBox x, SpawnBox y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null && y != null)
            {
                return false;
            }

            if (x != null && y == null)
            {
                return false;
            }

            return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
        }

        public int GetHashCode(SpawnBox obj)
        {
            return obj.X.GetHashCode() + obj.Y.GetHashCode() + obj.Z.GetHashCode();
        }
    }
}
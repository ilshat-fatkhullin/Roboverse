using System.Collections.Generic;

namespace Assets.Scripts.Settings
{
    public interface ISettings
    {
        public IReadOnlyCollection<FieldInfo<float>> FloatFields { get; }
        public IReadOnlyCollection<FieldInfo<int>> IntFields { get; }
        public IReadOnlyCollection<FieldInfo<string>> StringFields { get; }
    }
}

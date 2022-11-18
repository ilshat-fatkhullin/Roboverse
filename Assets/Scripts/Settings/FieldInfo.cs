using System;

namespace Assets.Scripts.Settings
{
    public sealed class FieldInfo<T>
    {
        public readonly string Name;

        public readonly Func<T> Get;

        public readonly Action<T> Set;     
        
        public FieldInfo(string name, Func<T> get, Action<T> set)
        {
            Name = name;
            Get = get;
            Set = set;
        }
    }
}
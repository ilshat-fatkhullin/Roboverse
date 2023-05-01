using System;

namespace Assets.Scripts.Common
{
    public static class Throws
    {
        public static T IfNull<T>(this T value, string name)
        {
            if (value == null) 
            {
                throw new ArgumentNullException($"{name} is null");
            }

            return value;
        }
    }
}

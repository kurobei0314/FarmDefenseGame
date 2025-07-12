using System;

namespace Extension
{
    public static class EnumHelper
    {
        public static int GetMinIndexEnum<T>() where T : Enum
        {
            var minIndex = 99999;
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (minIndex >= (int)value) minIndex = (int)value;
            }
            return minIndex;
        }

        public static int GetMaxIndexEnum<T>() where T : Enum
        {
            var maxIndex = 0;
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (maxIndex < (int)value) maxIndex = (int)value;
            }
            return maxIndex;
        }
    }
}

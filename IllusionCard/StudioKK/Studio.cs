using System;
using System.Collections.Generic;

namespace StudioKK
{
    public static class Studio
    {
        private static HashSet<int> hashIndex = new HashSet<int>();

        public static int GetNewIndex()
        {
            for (int index = 0; MathfEx.RangeEqualOn(0, index, int.MaxValue); ++index)
            {
                if (!hashIndex.Contains(index))
                {
                    hashIndex.Add(index);
                    return index;
                }
            }
            return -1;
        }

        public static int CheckNewIndex()
        {
            for (int index = -1; MathfEx.RangeEqualOn<int>(0, index, int.MaxValue); ++index)
            {
                if (!hashIndex.Contains(index))
                    return index;
            }
            return -1;
        }

        public static bool SetNewIndex(int _index)
        {
            return hashIndex.Add(_index);
        }

        public static void DeleteIndex(int _index)
        {
            hashIndex.Remove(_index);
        }
    }
}

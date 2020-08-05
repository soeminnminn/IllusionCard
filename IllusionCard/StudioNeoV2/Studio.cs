using System;
using System.Collections.Generic;

namespace StudioNeoV2
{
    public class Studio
    {
        private static HashSet<int> hashIndex = new HashSet<int>();

        public int cameraCount { get; private set; } = 0;

        public static Studio Instance
        {
            get => new Studio();
        }

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
            for (int index = -1; MathfEx.RangeEqualOn(0, index, int.MaxValue); ++index)
            {
                if (!hashIndex.Contains(index))
                    return index;
            }
            return -1;
        }

        public static int SetNewIndex(int _index)
        {
            if(hashIndex.Add(_index))
            {
                return _index;
            }
            return -1;
        }

        public static void DeleteIndex(int _index)
        {
            hashIndex.Remove(_index);
        }
    }
}

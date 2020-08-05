using System;

namespace StudioPH
{
    public static class Studio
    {
        private static int newIndex = 1;

        public static int GetNewIndex()
        {
            return newIndex++;
        }
    }
}

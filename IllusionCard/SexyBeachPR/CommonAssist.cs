using System;
using System.Text;

namespace SexyBeachPR
{
    public static class CommonAssist
    {
        public static string GetDateTimeString(DateTime time)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(time.Year.ToString("0000"));
            stringBuilder.Append("_");
            stringBuilder.Append(time.Month.ToString("00"));
            stringBuilder.Append(time.Day.ToString("00"));
            stringBuilder.Append("_");
            stringBuilder.Append(time.Hour.ToString("00"));
            stringBuilder.Append("_");
            stringBuilder.Append(time.Minute.ToString("00"));
            stringBuilder.Append("_");
            stringBuilder.Append(time.Second.ToString("00"));
            stringBuilder.Append("_");
            stringBuilder.Append(time.Millisecond.ToString("000"));
            return stringBuilder.ToString();
        }
    }
}

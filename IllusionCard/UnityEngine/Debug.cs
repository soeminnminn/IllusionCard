using System;

namespace UnityEngine
{
    public static class Debug
    {
        public static void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public static void LogWarning(string warning)
        {
            System.Diagnostics.Debug.WriteLine(warning);
        }

        internal static void LogError(string message)
        {
            System.Diagnostics.Debug.Fail(message);
        }

        internal static void LogError(Exception exception)
        {
            System.Diagnostics.Debug.Fail(exception.Message, exception.StackTrace);
        }

        internal static void Assert(bool condition)
        {
            System.Diagnostics.Debug.Assert(condition);
        }

        internal static void Assert(bool condition, string message)
        {
            System.Diagnostics.Debug.Assert(condition, message);
        }
    }
}

using System;
using Newtonsoft.Json;

namespace UnityEngine
{
    public static class JsonUtility
    {
        public static string ToJson(object obj) { return ToJson(obj, false); }

        public static string ToJson(object obj, bool prettyPrint)
        {
            if (obj == null)
                return "";

            return JsonConvert.SerializeObject(obj, prettyPrint ? Formatting.Indented : Formatting.None);
        }

        public static T FromJson<T>(string json) { return (T)FromJson(json, typeof(T)); }

        public static object FromJson(string json, Type type)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            if (type == null)
                throw new ArgumentNullException("type");

            return JsonConvert.DeserializeObject(json, type);
        }
    }
}

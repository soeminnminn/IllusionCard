// Decompiled with JetBrains decompiler
// Type: DeepCopy.DeepCopyHelper
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCopy
{
    public static class DeepCopyHelper
    {
        public static T DeepCopy<T>(T target)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                binaryFormatter.Serialize(memoryStream, target);
                memoryStream.Position = 0L;
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }
}

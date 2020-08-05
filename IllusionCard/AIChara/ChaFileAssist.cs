﻿using System;
using System.IO;
using MessagePack;

namespace AIChara
{
    public class ChaFileAssist
    {
        public void SaveFileAssist<T>(string path, T info)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    byte[] buffer = MessagePackSerializer.Serialize(info);
                    binaryWriter.Write(buffer);
                }
            }
        }

        public void LoadFileAssist<T>(string path, out T info)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    byte[] bytes = binaryReader.ReadBytes((int)fileStream.Length);
                    info = MessagePackSerializer.Deserialize<T>(bytes);
                }
            }
        }

        public void LoadFileAssist<T>(byte[] bytes, out T info)
        {
            info = MessagePackSerializer.Deserialize<T>(bytes);
        }
    }
}

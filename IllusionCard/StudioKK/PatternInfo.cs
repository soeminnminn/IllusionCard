using System;
using System.IO;
using UnityEngine;

namespace StudioKK
{
    public class PatternInfo
    {
        public int key = 0;
        public string filePath = string.Empty;
        public bool clamp = true;
        public Vector4 uv = new Vector4(0.0f, 0.0f, 1f, 1f);
        public float rot;

        public PatternInfo()
        {
            
        }

        public void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write(this.key);
            _writer.Write(this.filePath);
            _writer.Write(this.clamp);
            _writer.Write(JsonUtility.ToJson(this.uv));
            _writer.Write(this.rot);
        }

        public void Load(BinaryReader _reader, Version _version)
        {
            this.key = _reader.ReadInt32();
            this.filePath = _reader.ReadString();
            this.clamp = _reader.ReadBoolean();
            this.uv = JsonUtility.FromJson<Vector4>(_reader.ReadString());
            this.rot = _reader.ReadSingle();
        }
    }
}

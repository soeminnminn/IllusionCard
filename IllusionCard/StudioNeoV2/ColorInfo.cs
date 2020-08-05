using System;
using System.IO;
using UnityEngine;

namespace StudioNeoV2
{
    public class ColorInfo
    {
        public Color mainColor = Color.white;
        public PatternInfo pattern = new PatternInfo();
        public float metallic;
        public float glossiness;

        public virtual void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write(JsonUtility.ToJson(mainColor));
            _writer.Write(this.metallic);
            _writer.Write(this.glossiness);
            this.pattern.Save(_writer, _version);
        }

        public virtual void Load(BinaryReader _reader, Version _version)
        {
            this.mainColor = JsonUtility.FromJson<Color>(_reader.ReadString());
            this.metallic = _reader.ReadSingle();
            this.glossiness = _reader.ReadSingle();
            this.pattern.Load(_reader, _version);
        }
    }
}

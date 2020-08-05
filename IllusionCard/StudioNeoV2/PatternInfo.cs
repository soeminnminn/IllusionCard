using System;
using System.IO;
using UnityEngine;

namespace StudioNeoV2
{
    public class PatternInfo
    {
        public int key = 0;
        public string filePath = string.Empty;
        public Color color = Color.white;
        public bool clamp = true;
        public Vector4 uv = new Vector4(0.0f, 0.0f, 1f, 1f);
        public float rot;

        public float ut
        {
            get
            {
                return this.uv.z;
            }
            set
            {
                this.uv.z = value;
            }
        }

        public float vt
        {
            get
            {
                return this.uv.w;
            }
            set
            {
                this.uv.w = value;
            }
        }

        public float us
        {
            get
            {
                return this.uv.x;
            }
            set
            {
                this.uv.x = value;
            }
        }

        public float vs
        {
            get
            {
                return this.uv.y;
            }
            set
            {
                this.uv.y = value;
            }
        }

        public string name
        {
            get
            {
                return !string.IsNullOrEmpty(this.filePath) ? Path.GetFileNameWithoutExtension(this.filePath) : "なし";
            }
        }

        public PatternInfo()
        {
            this.filePath = "";
            this.key = -1;
        }

        public void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write(JsonUtility.ToJson((object)this.color));
            _writer.Write(this.key);
            _writer.Write(this.filePath);
            _writer.Write(this.clamp);
            _writer.Write(JsonUtility.ToJson((object)this.uv));
            _writer.Write(this.rot);
        }

        public void Load(BinaryReader _reader, Version _version)
        {
            this.color = JsonUtility.FromJson<Color>(_reader.ReadString());
            this.key = _reader.ReadInt32();
            this.filePath = _reader.ReadString();
            this.clamp = _reader.ReadBoolean();
            this.uv = JsonUtility.FromJson<Vector4>(_reader.ReadString());
            this.rot = _reader.ReadSingle();
        }
    }
}

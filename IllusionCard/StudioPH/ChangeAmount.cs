using System;
using System.IO;
using UnityEngine;

namespace StudioPH
{
    public class ChangeAmount
    {
        protected Vector3 pos = Vector3.zero;
        protected Vector3 rot = Vector3.zero;
        protected Vector3 scale = Vector3.one;

        public ChangeAmount()
        {
            this.pos = Vector3.zero;
            this.rot = Vector3.zero;
            this.scale = Vector3.one;
        }

        public void Save(BinaryWriter _writer)
        {
            _writer.Write(JsonUtility.ToJson(this.pos));
            _writer.Write(JsonUtility.ToJson(this.rot));
            _writer.Write(JsonUtility.ToJson(this.scale));
        }

        public void Load(BinaryReader _reader)
        {
            this.pos = JsonUtility.FromJson<Vector3>(_reader.ReadString());
            this.rot = JsonUtility.FromJson<Vector3>(_reader.ReadString());
            this.scale = JsonUtility.FromJson<Vector3>(_reader.ReadString());
        }

        public ChangeAmount Clone()
        {
            return new ChangeAmount()
            {
                pos = new Vector3(this.pos.x, this.pos.y, this.pos.z),
                rot = new Vector3(this.rot.x, this.rot.y, this.rot.z),
                scale = new Vector3(this.scale.x, this.scale.y, this.scale.z)
            };
        }

        public void Copy(ChangeAmount _source, bool _pos = true, bool _rot = true, bool _scale = true)
        {
            if (_pos)
                this.pos = _source.pos;
            if (_rot)
                this.rot = _source.rot;
            if (!_scale)
                return;
            this.scale = _source.scale;
        }

        public void Reset()
        {
            this.pos = Vector3.zero;
            this.rot = Vector3.zero;
            this.scale = Vector3.one;
        }
    }
}

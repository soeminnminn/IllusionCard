using System;
using System.IO;
using UnityEngine;

namespace StudioKK
{
    public class ChangeAmount
    {
        public Vector3 pos = Vector3.zero;
        public Vector3 rot = Vector3.zero;
        public Vector3 scale = Vector3.one;

        public ChangeAmount()
        {
            this.pos = Vector3.zero;
            this.rot = Vector3.zero;
            this.scale = Vector3.one;
        }

        public void Save(BinaryWriter _writer)
        {
            _writer.Write(this.pos.x);
            _writer.Write(this.pos.y);
            _writer.Write(this.pos.z);
            _writer.Write(this.rot.x);
            _writer.Write(this.rot.y);
            _writer.Write(this.rot.z);
            _writer.Write(this.scale.x);
            _writer.Write(this.scale.y);
            _writer.Write(this.scale.z);
        }

        public void Load(BinaryReader _reader)
        {
            Vector3 pos = this.pos;
            pos.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
            this.pos = pos;
            Vector3 rot = this.rot;
            rot.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
            this.rot = rot;
            Vector3 scale = this.scale;
            scale.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
            this.scale = scale;
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

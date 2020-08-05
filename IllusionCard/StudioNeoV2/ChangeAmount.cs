using System;
using System.IO;
using UnityEngine;

namespace StudioNeoV2
{
    public class ChangeAmount
    {
        public Vector3 pos = Vector3.zero;
        public Vector3 rot = Vector3.zero;
        public Vector3 scale = Vector3.one;

        public Vector3 defRot { get; set; } = Vector3.zero;

        public ChangeAmount()
        {
            this.pos = Vector3.zero;
            this.rot = Vector3.zero;
            this.scale = Vector3.one;
        }

        public ChangeAmount(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            this.pos = pos;
            this.rot = rot;
            this.scale = scale;
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
            this.pos.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
            this.rot.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
            this.scale.Set(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
        }

        public ChangeAmount Clone()
        {
            return new ChangeAmount(this.pos, this.rot, this.scale);
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

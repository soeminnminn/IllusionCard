using System;
using System.IO;
using UnityEngine;

namespace StudioPH
{
    public class ChangeAmount
    {
        protected Vector3 m_Pos = Vector3.zero;
        protected Vector3 m_Rot = Vector3.zero;
        protected Vector3 m_Scale = Vector3.one;
        public Action onChangePos;
        public Action onChangeRot;
        public Action<Vector3> onChangeScale;

        public ChangeAmount()
        {
            this.m_Pos = Vector3.zero;
            this.m_Rot = Vector3.zero;
            this.m_Scale = Vector3.one;
        }

        public Vector3 pos
        {
            get
            {
                return this.m_Pos;
            }
            set
            {
                this.m_Pos = value;
            }
        }

        public Vector3 rot
        {
            get
            {
                return this.m_Rot;
            }
            set
            {
                this.m_Rot = value;
            }
        }

        public Vector3 scale
        {
            get
            {
                return this.m_Scale;
            }
            set
            {
                this.m_Scale = value;
            }
        }

        public void Save(BinaryWriter _writer)
        {
            _writer.Write(JsonUtility.ToJson(this.m_Pos));
            _writer.Write(JsonUtility.ToJson(this.m_Rot));
            _writer.Write(JsonUtility.ToJson(this.m_Scale));
        }

        public void Load(BinaryReader _reader)
        {
            this.m_Pos = JsonUtility.FromJson<Vector3>(_reader.ReadString());
            this.m_Rot = JsonUtility.FromJson<Vector3>(_reader.ReadString());
            this.m_Scale = JsonUtility.FromJson<Vector3>(_reader.ReadString());
        }

        public ChangeAmount Clone()
        {
            return new ChangeAmount()
            {
                pos = new Vector3(this.m_Pos.x, this.m_Pos.y, this.m_Pos.z),
                rot = new Vector3(this.m_Rot.x, this.m_Rot.y, this.m_Rot.z),
                scale = new Vector3(this.m_Scale.x, this.m_Scale.y, this.m_Scale.z)
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

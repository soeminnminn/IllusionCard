using System;
using System.IO;
using UnityEngine;

namespace StudioPH
{
    public class CameraControl
    {
        public class CameraData
        {
            private const int ver = 2;
            public Vector3 pos = Vector3.zero;
            public Vector3 rotate = Vector3.zero;
            public Vector3 distance = Vector3.zero;
            public float parse = 23f;

            public CameraData()
            {
            }

            public CameraData(CameraData _src)
            {
                this.Copy(_src);
            }

            public void Set(Vector3 _pos, Vector3 _rotate, Vector3 _distance, float _parse)
            {
                this.pos = _pos;
                this.rotate = _rotate;
                this.distance = _distance;
                this.parse = _parse;
            }

            public void Save(BinaryWriter _writer)
            {
                _writer.Write(2);
                _writer.Write(this.pos.x);
                _writer.Write(this.pos.y);
                _writer.Write(this.pos.z);
                _writer.Write(this.rotate.x);
                _writer.Write(this.rotate.y);
                _writer.Write(this.rotate.z);
                _writer.Write(this.distance.x);
                _writer.Write(this.distance.y);
                _writer.Write(this.distance.z);
                _writer.Write(this.parse);
            }

            public void Load(BinaryReader _reader)
            {
                int version = _reader.ReadInt32();
                this.pos.x = _reader.ReadSingle();
                this.pos.y = _reader.ReadSingle();
                this.pos.z = _reader.ReadSingle();
                this.rotate.x = _reader.ReadSingle();
                this.rotate.y = _reader.ReadSingle();
                this.rotate.z = _reader.ReadSingle();
                if (version == 1)
                {
                    double _ = _reader.ReadSingle();
                }
                else
                {
                    this.distance.x = _reader.ReadSingle();
                    this.distance.y = _reader.ReadSingle();
                    this.distance.z = _reader.ReadSingle();
                }
                this.parse = _reader.ReadSingle();
            }

            public void Copy(CameraData _src)
            {
                this.pos = _src.pos;
                this.rotate = _src.rotate;
                this.distance = _src.distance;
                this.parse = _src.parse;
            }
        }

        public enum Config
        {
            MoveXZ,
            Rotation,
            Translation,
            MoveXY,
        }
    }
}

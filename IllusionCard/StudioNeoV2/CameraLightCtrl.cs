using System;
using System.IO;
using UnityEngine;

namespace StudioNeoV2
{
    public enum LightType
    {
        Spot = 0,
        Directional = 1,
        Point = 2,
        Area = 3,
        Rectangle = 3,
        Disc = 4,
    }

    public class CameraLightCtrl
    {
        public class LightInfo
        {
            public Color color = Color.white;
            public float intensity = 1f;
            public float[] rot = new float[2];
            public bool shadow = true;

            public virtual void Init()
            {
                this.color = Utility.ConvertColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);
                this.intensity = 1f;
                this.rot[0] = 0.0f;
                this.rot[1] = 0.0f;
                this.shadow = true;
            }

            public virtual void Save(BinaryWriter _writer, Version _version)
            {
                _writer.Write(JsonUtility.ToJson(color));
                _writer.Write(this.intensity);
                _writer.Write(this.rot[0]);
                _writer.Write(this.rot[1]);
                _writer.Write(this.shadow);
            }

            public virtual void Load(BinaryReader _reader, Version _version)
            {
                this.color = JsonUtility.FromJson<Color>(_reader.ReadString());
                this.intensity = _reader.ReadSingle();
                this.rot[0] = _reader.ReadSingle();
                this.rot[1] = _reader.ReadSingle();
                this.shadow = _reader.ReadBoolean();
            }
        }

        public class MapLightInfo : LightInfo
        {
            public LightType type = LightType.Directional;

            public override void Init()
            {
                base.Init();
                this.type = LightType.Directional;
            }

            public override void Save(BinaryWriter _writer, Version _version)
            {
                base.Save(_writer, _version);
                _writer.Write((int)this.type);
            }

            public override void Load(BinaryReader _reader, Version _version)
            {
                base.Load(_reader, _version);
                this.type = (LightType)_reader.ReadInt32();
            }
        }
    }
}

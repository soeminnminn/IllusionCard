// Decompiled with JetBrains decompiler
// Type: StudioHS.OILightInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using UnityEngine;

namespace StudioHS
{
    public class OILightInfo : ObjectInfo
    {
        public Color color;
        public float intensity;
        public float range;
        public float spotAngle;
        public bool shadow;
        public bool enable;
        public bool drawTarget;

        public OILightInfo(int _no, int _key)
          : base(_key)
        {
            this.no = _no;
            this.color = Color.white;
            this.intensity = 1f;
            this.range = 10f;
            this.spotAngle = 30f;
            this.shadow = true;
            this.enable = true;
            this.drawTarget = true;
        }

        public override int kind
        {
            get
            {
                return 2;
            }
        }

        public int no { get; private set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.no);
            Utility.SaveColor(_writer, this.color);
            _writer.Write(this.intensity);
            _writer.Write(this.range);
            _writer.Write(this.spotAngle);
            _writer.Write(this.shadow);
            _writer.Write(this.enable);
            _writer.Write(this.drawTarget);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.no = _reader.ReadInt32();
            this.color = Utility.LoadColor(_reader);
            this.intensity = _reader.ReadSingle();
            this.range = _reader.ReadSingle();
            this.spotAngle = _reader.ReadSingle();
            this.shadow = _reader.ReadBoolean();
            this.enable = _reader.ReadBoolean();
            this.drawTarget = _reader.ReadBoolean();
        }
    }
}

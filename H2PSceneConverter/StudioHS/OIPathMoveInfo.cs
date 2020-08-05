// Decompiled with JetBrains decompiler
// Type: StudioHS.OIPathMoveInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace StudioHS
{
    public class OIPathMoveInfo : ObjectInfo
    {
        public string name = string.Empty;

        public OIPathMoveInfo(int _key)
          : base(_key)
        {
            this.name = "経路";
            this.points = new List<OIPointInfo>();
            this.child = new List<ObjectInfo>();
        }

        public override int kind
        {
            get
            {
                return 4;
            }
        }

        public List<OIPointInfo> points { get; private set; }

        public List<ObjectInfo> child { get; private set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.name);
            int count1 = this.points.Count;
            _writer.Write(count1);
            for (int index = 0; index < count1; ++index)
                this.points[index].Save(_writer, _version);
            int count2 = this.child.Count;
            _writer.Write(count2);
            for (int index = 0; index < count2; ++index)
                this.child[index].Save(_writer, _version);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.name = _reader.ReadString();
            int num = _reader.ReadInt32();
            for (int index = 0; index < num; ++index)
            {
                OIPointInfo oiPointInfo = new OIPointInfo(-1);
                oiPointInfo.Load(_reader, _version, _import, true);
                this.points.Add(oiPointInfo);
            }
            ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
        }
    }
}

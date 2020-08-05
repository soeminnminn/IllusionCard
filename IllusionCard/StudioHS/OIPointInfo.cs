// Decompiled with JetBrains decompiler
// Type: StudioHS.OIPointInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace StudioHS
{
    public class OIPointInfo : ObjectInfo
    {
        public OIPointInfo(int _key = -1)
          : base(_key)
        {
        }

        public override int kind
        {
            get
            {
                return -1;
            }
        }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write(this.dicKey);
            this.changeAmount.Save(_writer);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, false);
        }
    }
}

// Decompiled with JetBrains decompiler
// Type: StudioHS.OIFolderInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace StudioHS
{
    public class OIFolderInfo : ObjectInfo
    {
        public string name = string.Empty;

        public OIFolderInfo(int _key)
          : base(_key)
        {
            this.name = "フォルダー";
            this.child = new List<ObjectInfo>();
        }

        public override int kind
        {
            get
            {
                return 3;
            }
        }

        public List<ObjectInfo> child { get; private set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.name);
            int count = this.child.Count;
            _writer.Write(count);
            for (int index = 0; index < count; ++index)
                this.child[index].Save(_writer, _version);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.name = _reader.ReadString();
            ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace StudioPH
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

using System;
using System.IO;

namespace StudioKK
{
    public class OICameraInfo : ObjectInfo
    {
        public string name = string.Empty;
        public bool active;

        public OICameraInfo(int _key)
          : base(_key)
        {
            this.name = "カメラ";
            this.active = false;
        }

        public override int kind
        {
            get
            {
                return 5;
            }
        }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.name);
            _writer.Write(this.active);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.name = _reader.ReadString();
            this.active = _reader.ReadBoolean();
        }
    }
}

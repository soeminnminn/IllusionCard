using System;
using System.IO;

namespace StudioNeoV2
{
    public class OICameraInfo : ObjectInfo
    {
        public string name = "";
        public bool active;

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

        public OICameraInfo(int _key)
          : base(_key)
        {
            this.name = string.Format("カメラ{0}", Studio.Instance.cameraCount);
            this.active = false;
        }
    }
}

using System;
using System.IO;

namespace StudioKK
{
    public class OIRoutePointAidInfo : ObjectInfo
    {
        public bool isInit;

        public OIRoutePointAidInfo(int _key)
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
            _writer.Write(this.isInit);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, false);
            this.isInit = _reader.ReadBoolean();
        }
    }
}

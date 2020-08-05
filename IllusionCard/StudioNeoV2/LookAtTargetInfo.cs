using System;
using System.IO;

namespace StudioNeoV2
{
    public class LookAtTargetInfo : ObjectInfo
    {
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

        public LookAtTargetInfo(int _key)
          : base(_key)
        {
        }
    }
}

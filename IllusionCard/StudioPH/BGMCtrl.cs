using System;
using System.IO;

namespace StudioPH
{
    public class BGMCtrl
    {
        public Repeat repeat = Repeat.All;
        public int no;
        public bool play;

        public bool isPause { get; private set; }

        public void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write((int)this.repeat);
            _writer.Write(this.no);
            _writer.Write(this.play);
        }

        public void Load(BinaryReader _reader, Version _version)
        {
            this.repeat = (Repeat)_reader.ReadInt32();
            this.no = _reader.ReadInt32();
            this.play = _reader.ReadBoolean();
        }

        public enum Repeat
        {
            None,
            All,
        }
    }
}

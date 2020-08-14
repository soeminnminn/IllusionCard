using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioKK
{
    public class ENVCtrl
    {
        public BGMCtrl.Repeat repeat = BGMCtrl.Repeat.All;
        public int no;
        public bool play;

        public void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write((int)this.repeat);
            _writer.Write(this.no);
            _writer.Write(this.play);
        }

        public void Load(BinaryReader _reader, Version _version)
        {
            this.repeat = (BGMCtrl.Repeat)_reader.ReadInt32();
            this.no = _reader.ReadInt32();
            this.play = _reader.ReadBoolean();
        }
    }
}

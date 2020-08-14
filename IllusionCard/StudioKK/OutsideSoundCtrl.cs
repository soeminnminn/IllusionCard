using System;
using System.IO;

namespace StudioKK
{
    public class OutsideSoundCtrl
    {
        public BGMCtrl.Repeat repeat = BGMCtrl.Repeat.All;
        public string fileName = string.Empty;
        public const string dataPath = "audio";
        public bool play;

        public void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write((int)this.repeat);
            _writer.Write(this.fileName);
            _writer.Write(this.play);
        }

        public void Load(BinaryReader _reader, Version _version)
        {
            this.repeat = (BGMCtrl.Repeat)_reader.ReadInt32();
            this.fileName = _reader.ReadString();
            this.play = _reader.ReadBoolean();
        }
    }
}

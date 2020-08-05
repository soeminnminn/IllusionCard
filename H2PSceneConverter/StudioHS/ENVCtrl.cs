// Decompiled with JetBrains decompiler
// Type: StudioHS.ENVCtrl
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace StudioHS
{
    public class ENVCtrl
    {
        public BGMCtrl.Repeat repeat = BGMCtrl.Repeat.All;
        public int no;
        public bool play = false;

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

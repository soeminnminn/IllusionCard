// Decompiled with JetBrains decompiler
// Type: StudioHS.OutsideSoundCtrl
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace StudioHS
{
    public class OutsideSoundCtrl
    {
        public const string dataPath = "audio";
        public BGMCtrl.Repeat repeat = BGMCtrl.Repeat.All;
        public string fileName = string.Empty;
        public bool play = false;

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

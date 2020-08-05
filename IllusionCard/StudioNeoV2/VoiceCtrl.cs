using System;
using System.Collections.Generic;
using System.IO;

namespace StudioNeoV2
{
    public class VoiceCtrl
    {
        public List<VoiceCtrl.VoiceInfo> list = new List<VoiceCtrl.VoiceInfo>();
        public int index = -1;
        public const string savePath = "studio/voicelist";
        public const string saveExtension = ".dat";
        public const string saveIdentifyingCode = "【voice】";
        public VoiceCtrl.Repeat repeat;

        public void Save(BinaryWriter _writer, Version _version)
        {
            int count = this.list.Count;
            _writer.Write(count);
            for (int index = 0; index < count; ++index)
            {
                VoiceCtrl.VoiceInfo voiceInfo = this.list[index];
                _writer.Write(voiceInfo.group);
                _writer.Write(voiceInfo.category);
                _writer.Write(voiceInfo.no);
            }
            _writer.Write((int)this.repeat);
        }

        public void Load(BinaryReader _reader, Version _version)
        {
            int num = _reader.ReadInt32();
            for (int index = 0; index < num; ++index)
            {
                int _group = _reader.ReadInt32();
                int _category = _reader.ReadInt32();
                int _no = _reader.ReadInt32();
                this.list.Add(new VoiceCtrl.VoiceInfo(_group, _category, _no));
            }
            this.repeat = (VoiceCtrl.Repeat)_reader.ReadInt32();
        }

        public class VoiceInfo
        {
            public int group { get; private set; }

            public int category { get; private set; }

            public int no { get; private set; }

            public VoiceInfo(int _group, int _category, int _no)
            {
                this.group = _group;
                this.category = _category;
                this.no = _no;
            }
        }

        public enum Repeat
        {
            None,
            All,
            Select,
        }
    }
}

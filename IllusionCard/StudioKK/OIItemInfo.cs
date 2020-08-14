using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioKK
{
    public class OIItemInfo : ObjectInfo
    {
        public float animeSpeed = 1f;
        public float alpha = 1f;
        public float lineWidth = 1f;
        public bool enableDynamicBone = true;
        public Color[] color;
        public PatternInfo[] pattern;
        public Color lineColor;
        public Color emissionColor;
        public float emissionPower;
        public float lightCancel;
        public PatternInfo panel;
        public bool enableFK;
        public Dictionary<string, OIBoneInfo> bones;
        public float animeNormalizedTime;

        public OIItemInfo(int _group, int _category, int _no, int _key)
          : base(_key)
        {
            this.group = _group;
            this.category = _category;
            this.no = _no;
            this.child = new List<ObjectInfo>();
            this.color = new Color[8]
            {
                Color.white,
                Color.white,
                Color.white,
                Color.white,
                Color.white,
                Color.white,
                Color.white,
                Color.white
            };
            this.lineColor = Utility.ConvertColor(128, 128, 128);
            this.emissionColor = Utility.ConvertColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);
            this.pattern = new PatternInfo[3];
            for (int index = 0; index < 3; ++index)
                this.pattern[index] = new PatternInfo();
            this.panel = new PatternInfo();
            this.bones = new Dictionary<string, OIBoneInfo>();
            this.animeNormalizedTime = 0.0f;
        }

        public override int kind
        {
            get
            {
                return 1;
            }
        }

        public int group { get; private set; }

        public int category { get; private set; }

        public int no { get; private set; }

        public List<ObjectInfo> child { get; private set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.group);
            _writer.Write(this.category);
            _writer.Write(this.no);
            _writer.Write(this.animeSpeed);
            for (int index = 0; index < 8; ++index)
                _writer.Write(JsonUtility.ToJson(this.color[index]));
            for (int index = 0; index < 3; ++index)
                this.pattern[index].Save(_writer, _version);
            _writer.Write(this.alpha);
            _writer.Write(JsonUtility.ToJson(lineColor));
            _writer.Write(this.lineWidth);
            _writer.Write(JsonUtility.ToJson(emissionColor));
            _writer.Write(this.emissionPower);
            _writer.Write(this.lightCancel);
            this.panel.Save(_writer, _version);
            _writer.Write(this.enableFK);
            _writer.Write(this.bones.Count);
            foreach (KeyValuePair<string, OIBoneInfo> bone in this.bones)
            {
                _writer.Write(bone.Key);
                bone.Value.Save(_writer, _version);
            }
            _writer.Write(this.enableDynamicBone);
            _writer.Write(this.animeNormalizedTime);
            int count = this.child.Count;
            _writer.Write(count);
            for (int index = 0; index < count; ++index)
                this.child[index].Save(_writer, _version);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.group = _reader.ReadInt32();
            this.category = _reader.ReadInt32();
            this.no = _reader.ReadInt32();
            this.animeSpeed = _reader.ReadSingle();
            if (_version.CompareTo(new Version(0, 0, 3)) >= 0)
            {
                for (int index = 0; index < 8; ++index)
                    this.color[index] = JsonUtility.FromJson<Color>(_reader.ReadString());
            }
            else
            {
                for (int index = 0; index < 7; ++index)
                    this.color[index] = JsonUtility.FromJson<Color>(_reader.ReadString());
            }
            for (int index = 0; index < 3; ++index)
                this.pattern[index].Load(_reader, _version);
            this.alpha = _reader.ReadSingle();
            if (_version.CompareTo(new Version(0, 0, 4)) >= 0)
            {
                this.lineColor = JsonUtility.FromJson<Color>(_reader.ReadString());
                this.lineWidth = _reader.ReadSingle();
            }
            if (_version.CompareTo(new Version(0, 0, 7)) >= 0)
            {
                this.emissionColor = JsonUtility.FromJson<Color>(_reader.ReadString());
                this.emissionPower = _reader.ReadSingle();
                this.lightCancel = _reader.ReadSingle();
            }
            if (_version.CompareTo(new Version(0, 0, 6)) >= 0)
                this.panel.Load(_reader, _version);
            this.enableFK = _reader.ReadBoolean();
            int num = _reader.ReadInt32();
            for (int index1 = 0; index1 < num; ++index1)
            {
                string index2 = _reader.ReadString();
                this.bones[index2] = new OIBoneInfo(_import ? Studio.GetNewIndex() : -1);
                this.bones[index2].Load(_reader, _version, _import, true);
            }
            if (_version.CompareTo(new Version(1, 0, 1)) >= 0)
                this.enableDynamicBone = _reader.ReadBoolean();
            this.animeNormalizedTime = _reader.ReadSingle();
            ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
        }
    }
}

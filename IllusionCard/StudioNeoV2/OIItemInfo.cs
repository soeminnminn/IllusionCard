using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioNeoV2
{
    public class OIItemInfo : ObjectInfo
    {
        public float animeSpeed = 1f;
        public float alpha = 1f;
        public bool enableDynamicBone = true;
        public int animePattern;
        public ColorInfo[] colors;
        public Color emissionColor;
        public float emissionPower;
        public float lightCancel;
        public PatternInfo panel;
        public bool enableFK;
        public Dictionary<string, OIBoneInfo> bones;
        public List<bool> option;
        public float animeNormalizedTime;
        private Color32 emissionColor32;
        private float emissionColorIntensity;

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

        public Color EmissionColor
        {
            get
            {
                this.DecomposeHDRColor(this.emissionColor, out this.emissionColor32, out this.emissionColorIntensity);
                return (Color)this.emissionColor32;
            }
            set
            {
                this.emissionColor = Mathf.Approximately(this.emissionColorIntensity, 0.0f) ? value : value * Mathf.Pow(2f, this.emissionColorIntensity);
            }
        }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.group);
            _writer.Write(this.category);
            _writer.Write(this.no);
            _writer.Write(this.animePattern);
            _writer.Write(this.animeSpeed);
            for (int index = 0; index < 4; ++index)
                this.colors[index].Save(_writer, _version);
            _writer.Write(this.alpha);
            _writer.Write(JsonUtility.ToJson((object)this.emissionColor));
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
            _writer.Write(this.option.Count);
            foreach (bool flag in this.option)
                _writer.Write(flag);
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
            if (_version.CompareTo(new Version(1, 0, 1)) >= 0)
                this.animePattern = _reader.ReadInt32();
            this.animeSpeed = _reader.ReadSingle();
            for (int index = 0; index < 4; ++index)
                this.colors[index].Load(_reader, _version);
            this.alpha = _reader.ReadSingle();
            this.emissionColor = JsonUtility.FromJson<Color>(_reader.ReadString());
            this.emissionPower = _reader.ReadSingle();
            this.lightCancel = _reader.ReadSingle();
            this.panel.Load(_reader, _version);
            if (_version.CompareTo(new Version(1, 1, 0)) <= 0 && !string.IsNullOrEmpty(this.panel.filePath))
                this.panel.filePath = BackgroundListAssist.GetFilePath(this.panel.filePath, false);
            this.enableFK = _reader.ReadBoolean();
            int num1 = _reader.ReadInt32();
            for (int index1 = 0; index1 < num1; ++index1)
            {
                string index2 = _reader.ReadString();
                this.bones[index2] = new OIBoneInfo(!_import ? -1 : Studio.GetNewIndex());
                this.bones[index2].Load(_reader, _version, _import, true);
            }
            this.enableDynamicBone = _reader.ReadBoolean();
            int num2 = _reader.ReadInt32();
            for (int index = 0; index < num2; ++index)
                this.option.Add(_reader.ReadBoolean());
            this.animeNormalizedTime = _reader.ReadSingle();
            ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
        }

        public OIItemInfo(int _group, int _category, int _no, int _key)
          : base(_key)
        {
            this.group = _group;
            this.category = _category;
            this.no = _no;
            this.child = new List<ObjectInfo>();
            this.colors = new ColorInfo[4]
            {
                new ColorInfo(),
                new ColorInfo(),
                new ColorInfo(),
                new ColorInfo()
            };
            this.emissionColor = Utility.ConvertColor((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
            this.panel = new PatternInfo();
            this.bones = new Dictionary<string, OIBoneInfo>();
            this.option = new List<bool>();
            this.animeNormalizedTime = 0.0f;
        }

        internal void DecomposeHDRColor(Color _colorHDR, out Color32 _baseColor, out float _intensity)
        {
            _baseColor = (Color32)Color.black;
            float maxColorComponent = _colorHDR.maxColorComponent;
            byte val1 = 191;
            if (Mathf.Approximately(maxColorComponent, 0.0f) || (double)maxColorComponent <= 1.0 && (double)maxColorComponent > 0.00392156885936856)
            {
                _intensity = 0.0f;
                _baseColor.r = (byte)Mathf.RoundToInt(_colorHDR.r * (float)byte.MaxValue);
                _baseColor.g = (byte)Mathf.RoundToInt(_colorHDR.g * (float)byte.MaxValue);
                _baseColor.b = (byte)Mathf.RoundToInt(_colorHDR.b * (float)byte.MaxValue);
            }
            else
            {
                float num = (float)val1 / maxColorComponent;
                _intensity = Mathf.Log((float)byte.MaxValue / num) / Mathf.Log(2f);
                _baseColor.r = Math.Min(val1, (byte)Mathf.CeilToInt(num * _colorHDR.r));
                _baseColor.g = Math.Min(val1, (byte)Mathf.CeilToInt(num * _colorHDR.g));
                _baseColor.b = Math.Min(val1, (byte)Mathf.CeilToInt(num * _colorHDR.b));
            }
        }
    }
}

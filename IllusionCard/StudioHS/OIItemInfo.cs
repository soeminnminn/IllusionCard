﻿// Decompiled with JetBrains decompiler
// Type: StudioHS.OIItemInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioHS
{
    public class OIItemInfo : ObjectInfo
    {
        public float animeSpeed = 1f;
        public HSColorSet color = new HSColorSet();
        public HSColorSet color2 = new HSColorSet();
        public bool enableFK;
        public Dictionary<string, OIBoneInfo> bones;
        public float animeNormalizedTime;

        public OIItemInfo(int _no, int _key)
          : base(_key)
        {
            this.no = _no;
            this.child = new List<ObjectInfo>();
            this.color.SetDiffuseRGB(Color.white);
            this.color.specularIntensity = 0.4f;
            this.color.specularSharpness = 0.55f;
            this.color2.SetDiffuseRGB(Color.white);
            this.color2.specularSharpness = 0.55f;
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

        public int no { get; private set; }

        public List<ObjectInfo> child { get; private set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.no);
            _writer.Write(this.animeSpeed);
            _writer.Write(1);
            this.color.Save(_writer);
            this.color2.Save(_writer);
            _writer.Write(this.enableFK);
            _writer.Write(this.bones.Count);
            foreach (KeyValuePair<string, OIBoneInfo> bone in this.bones)
            {
                _writer.Write(bone.Key);
                bone.Value.Save(_writer, _version);
            }
            _writer.Write(this.animeNormalizedTime);
            int count = this.child.Count;
            _writer.Write(count);
            for (int index = 0; index < count; ++index)
                this.child[index].Save(_writer, _version);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.no = _reader.ReadInt32();
            this.animeSpeed = _reader.ReadSingle();
            int version = _reader.ReadInt32();
            this.color.Load(_reader, version);
            this.color2.Load(_reader, version);
            if (_version.CompareTo(new Version(1, 0, 4)) >= 0)
            {
                this.enableFK = _reader.ReadBoolean();
                int num = _reader.ReadInt32();
                for (int index1 = 0; index1 < num; ++index1)
                {
                    string index2 = _reader.ReadString();
                    this.bones[index2] = new OIBoneInfo(-1);
                    this.bones[index2].Load(_reader, _version, _import, true);
                }
            }
            if (_version.CompareTo(new Version(0, 1, 6)) >= 0)
                this.animeNormalizedTime = _reader.ReadSingle();
            ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
        }
    }
}

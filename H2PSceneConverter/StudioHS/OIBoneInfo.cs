// Decompiled with JetBrains decompiler
// Type: StudioHS.OIBoneInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace StudioHS
{
    public class OIBoneInfo : ObjectInfo
    {
        public OIBoneInfo(int _key)
          : base(_key)
        {
            this.group = 0;
            this.level = 0;
        }

        public override int kind
        {
            get
            {
                return -1;
            }
        }

        public OIBoneInfo.BoneGroup group { get; set; }

        public int level { get; set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write(this.dicKey);
            this.changeAmount.Save(_writer);
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, false);
        }

        public enum BoneGroup
        {
            Body = 1,
            RightLeg = 2,
            LeftLeg = 4,
            RightArm = 8,
            LeftArm = 16, // 0x00000010
            RightHand = 32, // 0x00000020
            LeftHand = 64, // 0x00000040
            Hair = 128, // 0x00000080
            Neck = 256, // 0x00000100
            Breast = 512, // 0x00000200
            Skirt = 1024, // 0x00000400
        }
    }
}

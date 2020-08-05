// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoParameter
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public abstract class CharFileInfoParameter : BlockControlBase
    {
        public int parameterLoadVersion;

        public CharFileInfoParameter()
          : base("パラメータ情報", 5)
        {
            this.MemberInitialize();
        }

        protected void MemberInitialize()
        {
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(memoryStream))
                {
                    this.SaveSub(bw);
                    return memoryStream.ToArray();
                }
            }
        }

        public override bool LoadBytes(byte[] data, int parameterVer)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(memoryStream))
                    return this.LoadSub(br, parameterVer);
            }
        }

        protected abstract bool SaveSub(BinaryWriter bw);

        protected abstract bool LoadSub(BinaryReader br, int statusVer);
    }
}

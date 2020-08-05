// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoCoordinate
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public abstract class CharFileInfoCoordinate : BlockControlBase
    {
        protected Dictionary<CharDefine.CoordinateType, CharFileInfoClothes> dictClothesInfo = new Dictionary<CharDefine.CoordinateType, CharFileInfoClothes>();
        public int coordinateLoadVersion;

        public CharFileInfoCoordinate()
          : base("コーディネート情報", 1)
        {
        }

        public bool SetInfo(CharDefine.CoordinateType type, CharFileInfoClothes info)
        {
            return this.dictClothesInfo[type].Copy(info);
        }

        public CharFileInfoClothes GetInfo(CharDefine.CoordinateType type)
        {
            CharFileInfoClothes charFileInfoClothes = null;
            this.dictClothesInfo.TryGetValue(type, out charFileInfoClothes);
            return charFileInfoClothes;
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

        public override bool LoadBytes(byte[] data, int coordinateVer)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(memoryStream))
                    return this.LoadSub(br, coordinateVer);
            }
        }

        protected abstract bool SaveSub(BinaryWriter bw);

        protected abstract bool LoadSub(BinaryReader br, int coordinateVer);
    }
}

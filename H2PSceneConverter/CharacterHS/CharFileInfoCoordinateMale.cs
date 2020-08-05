// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoCoordinateMale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoCoordinateMale : CharFileInfoCoordinate
    {
        public CharFileInfoCoordinateMale()
        {
            for (int index = 0; index < Enum.GetNames(typeof(CharDefine.CoordinateType)).Length; ++index)
            {
                CharFileInfoClothesMale fileInfoClothesMale = new CharFileInfoClothesMale();
                this.dictClothesInfo[(CharDefine.CoordinateType)index] = fileInfoClothesMale;
            }
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            int num = this.dictClothesInfo.Count(v => v.Value != null);
            bw.Write(num);
            foreach (KeyValuePair<CharDefine.CoordinateType, CharFileInfoClothes> keyValuePair in this.dictClothesInfo.Where(v => v.Value != null))
            {
                bw.Write((int)keyValuePair.Key);
                keyValuePair.Value.SaveWithoutPNG(bw);
            }
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int coordinateVer)
        {
            this.dictClothesInfo.Clear();
            int num1 = br.ReadInt32();
            for (int index = 0; index < num1; ++index)
            {
                int num2 = br.ReadInt32();
                CharFileInfoClothesMale fileInfoClothesMale = new CharFileInfoClothesMale();
                fileInfoClothesMale.LoadWithoutPNG(br);
                this.dictClothesInfo[(CharDefine.CoordinateType)num2] = fileInfoClothesMale;
            }
            return true;
        }
    }
}

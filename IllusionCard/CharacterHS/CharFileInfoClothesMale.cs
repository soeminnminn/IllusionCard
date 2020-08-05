// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoClothesMale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoClothesMale : CharFileInfoClothes
    {
        public CharFileInfoClothesMale()
          : base("【HoneySelectClothesMale】", "coordinate/male/", Enum.GetNames(typeof(CharDefine.ClothesKindMale)).Length)
        {
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
            this.clothesTypeSex = (byte)0;
            int[] numArray = new int[2];
            for (int index = 0; index < this.clothesId.Length; ++index)
            {
                this.clothesId[index] = numArray[index];
                this.clothesColor[index] = new HSColorSet();
                this.clothesColor2[index] = new HSColorSet();
            }
        }

        public override bool Copy(CharFileInfoClothes srcData)
        {
            if (!(srcData is CharFileInfoClothesMale fileInfoClothesMale))
                return false;
            for (int index = 0; index < this.clothesKindNum; ++index)
            {
                this.clothesId[index] = fileInfoClothesMale.clothesId[index];
                this.clothesColor[index].Copy(fileInfoClothesMale.clothesColor[index]);
                this.clothesColor2[index].Copy(fileInfoClothesMale.clothesColor2[index]);
            }
            for (int index = 0; index < 10; ++index)
                this.accessory[index].Copy(srcData.accessory[index]);
            return true;
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int clothesVer, int colorVer)
        {
            return true;
        }
    }
}

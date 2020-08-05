// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoClothesFemale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoClothesFemale : CharFileInfoClothes
    {
        public bool swimType;
        public bool hideSwimOptTop;
        public bool hideSwimOptBot;

        public CharFileInfoClothesFemale()
          : base("【HoneySelectClothesFemale】", "coordinate/female/", Enum.GetNames(typeof(CharDefine.ClothesKindFemale)).Length)
        {
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
            this.clothesTypeSex = (byte)1;
            int[] numArray1 = new int[11];
            numArray1[0] = 101;
            int[] numArray2 = numArray1;
            for (int index = 0; index < this.clothesKindNum; ++index)
            {
                this.clothesId[index] = numArray2[index];
                this.clothesColor[index] = new HSColorSet();
                this.clothesColor2[index] = new HSColorSet();
            }
            this.swimType = false;
            this.hideSwimOptTop = false;
            this.hideSwimOptBot = false;
        }

        public override bool Copy(CharFileInfoClothes srcData)
        {
            if (!(srcData is CharFileInfoClothesFemale infoClothesFemale))
                return false;
            for (int index = 0; index < this.clothesKindNum; ++index)
            {
                this.clothesId[index] = infoClothesFemale.clothesId[index];
                this.clothesColor[index].Copy(infoClothesFemale.clothesColor[index]);
                this.clothesColor2[index].Copy(infoClothesFemale.clothesColor2[index]);
            }
            for (int index = 0; index < 10; ++index)
                this.accessory[index].Copy(srcData.accessory[index]);
            this.swimType = infoClothesFemale.swimType;
            this.hideSwimOptTop = infoClothesFemale.hideSwimOptTop;
            this.hideSwimOptBot = infoClothesFemale.hideSwimOptBot;
            return true;
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            bw.Write(this.swimType);
            bw.Write(this.hideSwimOptTop);
            bw.Write(this.hideSwimOptBot);
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int clothesVer, int colorVer)
        {
            this.swimType = br.ReadBoolean();
            this.hideSwimOptTop = br.ReadBoolean();
            this.hideSwimOptBot = br.ReadBoolean();
            return true;
        }
    }
}

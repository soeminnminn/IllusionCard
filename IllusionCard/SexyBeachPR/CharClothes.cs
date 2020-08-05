﻿using System;

namespace SexyBeachPR
{
    [Serializable]
    public class CharClothes
    {
        public string[] AccessoryTypeName = new string[13]
        {
            "なし",
            "頭",
            "耳",
            "眼鏡",
            "顔",
            "首",
            "肩",
            "胸",
            "腰",
            "背中",
            "腕",
            "手",
            "脚"
        };
        
        public CharAccessory[,] regAccessory = new CharAccessory[2, 5];
        public ColorSet[,] regAcsColor = new ColorSet[2, 5];
        public CharAccessory[] nowAccessory = new CharAccessory[5];
        public bool[] accessoryState = new bool[5];
        protected CharBody bodyBase;
        protected CharFemaleBody femaleBody;
        protected CharMaleBody maleBody;
        public byte setType;
        public bool alwaysSwimsuit;

        public CharClothes(CharBody cha)
        {
            this.bodyBase = cha;
            if (cha.Sex == 0)
                this.maleBody = cha as CharMale;
            else
                this.femaleBody = cha as CharFemale;
            for (int index1 = 0; index1 < 2; ++index1)
            {
                for (int index2 = 0; index2 < 5; ++index2)
                {
                    this.regAccessory[index1, index2] = new CharAccessory();
                    this.regAcsColor[index1, index2] = new ColorSet();
                }
            }
            for (int index = 0; index < 5; ++index)
            {
                this.nowAccessory[index] = new CharAccessory();
                this.accessoryState[index] = true;
            }
        }

        public bool IsAccessory(int slotNo)
        {
            return MathfEx.RangeEqualOn(0, slotNo, 4) && this.nowAccessory[slotNo].accessoryType != -1;
        }

        public virtual byte[] SaveBytes()
        {
            return null;
        }

        public virtual bool LoadBytes(byte[] data, int version)
        {
            return true;
        }
    }
}

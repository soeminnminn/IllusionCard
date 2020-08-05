// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoStatusMale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using UnityEngine;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoStatusMale : CharFileInfoStatus
    {
        public bool visibleSon = true;
        public bool visibleSonAlways = true;
        public byte visibleBodyAlways = 1;
        public Color simpleColor = new Color(0.188f, 0.286f, 0.8f, 0.5f);
        public bool visibleSimple;

        public CharFileInfoStatusMale()
        {
            this.clothesState = new byte[Enum.GetNames(typeof(CharDefine.ClothesStateKindMale)).Length];
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            bw.Write(this.visibleSon);
            bw.Write(this.visibleSonAlways);
            bw.Write(this.visibleBodyAlways);
            bw.Write(this.visibleSimple);
            bw.Write(this.simpleColor.r);
            bw.Write(this.simpleColor.g);
            bw.Write(this.simpleColor.b);
            bw.Write(this.simpleColor.a);
            bw.Write(this.clothesState.Length);
            for (int index = 0; index < this.clothesState.Length; ++index)
                bw.Write(this.clothesState[index]);
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int statusVer)
        {
            this.visibleSon = br.ReadBoolean();
            this.visibleSonAlways = br.ReadBoolean();
            this.visibleBodyAlways = br.ReadByte();
            if (2 <= statusVer)
            {
                this.visibleSimple = br.ReadBoolean();
                this.simpleColor.r = br.ReadSingle();
                this.simpleColor.g = br.ReadSingle();
                this.simpleColor.b = br.ReadSingle();
                this.simpleColor.a = br.ReadSingle();
            }
            int num = br.ReadInt32();
            for (int index = 0; index < num; ++index)
                this.clothesState[index] = br.ReadByte();
            return true;
        }
    }
}

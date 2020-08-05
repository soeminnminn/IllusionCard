// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoCustomMale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using UnityEngine;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoCustomMale : CharFileInfoCustom
    {
        public HSColorSet beardColor = new HSColorSet();
        public int matBeardId;

        public CharFileInfoCustomMale()
          : base(Enum.GetNames(typeof(CharDefine.HairKindMale)).Length)
        {
            this.sex = (byte)0;
            this.shapeValueFace = new float[CharDefine.cm_headshapename.Length];
            this.shapeValueBody = new float[CharDefine.cm_bodyshapename.Length];
            this.hairId = new int[this.hairKindNum];
            this.hairColor = new HSColorSet[this.hairKindNum];
            this.hairAcsColor = new HSColorSet[this.hairKindNum];
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
            this.matBeardId = 0;
            this.beardColor.SetDiffuseRGBA(Color.white);
            this.beardColor.hsvDiffuse.S = 1f;
            int[] numArray = new int[1];
            for (int index = 0; index < this.hairId.Length; ++index)
            {
                this.hairId[index] = numArray[index];
                this.hairColor[index] = new HSColorSet();
                this.hairColor[index].hsvDiffuse = new HsvColor(30f, 0.5f, 0.7f);
                this.hairColor[index].hsvSpecular = new HsvColor(0.0f, 0.0f, 0.5f);
                this.hairColor[index].specularIntensity = 0.4f;
                this.hairColor[index].specularSharpness = 0.55f;
                this.hairAcsColor[index] = new HSColorSet();
                this.hairAcsColor[index].hsvDiffuse = new HsvColor(0.0f, 0.8f, 1f);
                this.hairAcsColor[index].hsvSpecular = new HsvColor(0.0f, 0.0f, 0.5f);
                this.hairAcsColor[index].specularIntensity = 0.4f;
                this.hairAcsColor[index].specularSharpness = 0.55f;
            }
            this.name = "俺";
            this.personality = 99;
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            bw.Write(this.matBeardId);
            this.beardColor.Save(bw);
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int customVer, int colorVer)
        {
            this.matBeardId = br.ReadInt32();
            this.beardColor.Load(br, colorVer);
            return true;
        }

        protected override bool ModCheckSub()
        {
            return false;
        }
    }
}

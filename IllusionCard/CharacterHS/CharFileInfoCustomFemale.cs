// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoCustomFemale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using UnityEngine;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoCustomFemale : CharFileInfoCustom
    {
        public HSColorSet eyeshadowColor = new HSColorSet();
        public HSColorSet cheekColor = new HSColorSet();
        public HSColorSet lipColor = new HSColorSet();
        public HSColorSet moleColor = new HSColorSet();
        public HSColorSet eyelashesColor = new HSColorSet();
        public HSColorSet eyeHiColor = new HSColorSet();
        public HSColorSet sunburnColor = new HSColorSet();
        public HSColorSet nipColor = new HSColorSet();
        public HSColorSet underhairColor = new HSColorSet();
        public HSColorSet nailColor = new HSColorSet();
        public float areolaSize = 0.5f;
        public float bustSoftness = 0.5f;
        public float bustWeight = 0.5f;
        public int texEyeshadowId;
        public int texCheekId;
        public int texLipId;
        public int texMoleId;
        public int matEyelashesId;
        public int matEyeHiId;
        public int texSunburnId;
        public int matNipId;
        public int matUnderhairId;

        public CharFileInfoCustomFemale()
          : base(Enum.GetNames(typeof(CharDefine.HairKindFemale)).Length)
        {
            this.sex = (byte)1;
            this.shapeValueFace = new float[CharDefine.cf_headshapename.Length];
            this.shapeValueBody = new float[CharDefine.cf_bodyshapename.Length - 1];
            this.hairId = new int[this.hairKindNum];
            this.hairColor = new HSColorSet[this.hairKindNum];
            this.hairAcsColor = new HSColorSet[this.hairKindNum];
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
            this.texEyeshadowId = 0;
            this.eyeshadowColor.SetDiffuseRGBA(Color.white);
            this.texCheekId = 0;
            this.cheekColor.SetDiffuseRGBA(Color.white);
            this.texLipId = 0;
            this.lipColor.SetDiffuseRGBA(Color.white);
            this.texMoleId = 0;
            this.moleColor.SetDiffuseRGBA(Color.white);
            this.matEyelashesId = 0;
            this.eyelashesColor.SetDiffuseRGBA(Color.white);
            this.eyelashesColor.hsvDiffuse.S = 1f;
            this.matEyeHiId = 0;
            this.eyeHiColor.SetDiffuseRGBA(Color.white);
            this.texSunburnId = 0;
            this.sunburnColor.SetDiffuseRGBA(Color.white);
            this.matNipId = 0;
            this.nipColor.SetDiffuseRGBA(Color.white);
            this.nipColor.hsvDiffuse.S = 1f;
            this.matUnderhairId = 0;
            this.underhairColor.SetDiffuseRGBA(Color.white);
            this.underhairColor.hsvDiffuse.S = 1f;
            this.nailColor.SetDiffuseRGBA(Color.white);
            this.nailColor.hsvDiffuse.S = 1f;
            this.areolaSize = 0.5f;
            this.bustSoftness = 0.5f;
            this.bustWeight = 0.5f;
            int[] numArray = new int[4] { 0, 1, 0, 0 };
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
            this.name = "カスタム娘";
            this.personality = 0;
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            bw.Write(this.texEyeshadowId);
            this.eyeshadowColor.Save(bw);
            bw.Write(this.texCheekId);
            this.cheekColor.Save(bw);
            bw.Write(this.texLipId);
            this.lipColor.Save(bw);
            bw.Write(this.texMoleId);
            this.moleColor.Save(bw);
            bw.Write(this.matEyelashesId);
            this.eyelashesColor.Save(bw);
            bw.Write(this.matEyeHiId);
            this.eyeHiColor.Save(bw);
            bw.Write(this.texSunburnId);
            this.sunburnColor.Save(bw);
            bw.Write(this.matNipId);
            this.nipColor.Save(bw);
            bw.Write(this.matUnderhairId);
            this.underhairColor.Save(bw);
            this.nailColor.Save(bw);
            bw.Write(this.areolaSize);
            bw.Write(this.bustSoftness);
            bw.Write(this.bustWeight);
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int customVer, int colorVer)
        {
            this.texEyeshadowId = br.ReadInt32();
            this.eyeshadowColor.Load(br, colorVer);
            this.texCheekId = br.ReadInt32();
            this.cheekColor.Load(br, colorVer);
            this.texLipId = br.ReadInt32();
            this.lipColor.Load(br, colorVer);
            this.texMoleId = br.ReadInt32();
            this.moleColor.Load(br, colorVer);
            this.matEyelashesId = br.ReadInt32();
            this.eyelashesColor.Load(br, colorVer);
            this.matEyeHiId = br.ReadInt32();
            this.eyeHiColor.Load(br, colorVer);
            this.texSunburnId = br.ReadInt32();
            this.sunburnColor.Load(br, colorVer);
            this.matNipId = br.ReadInt32();
            this.nipColor.Load(br, colorVer);
            this.matUnderhairId = br.ReadInt32();
            this.underhairColor.Load(br, colorVer);
            this.nailColor.Load(br, colorVer);
            this.areolaSize = CharFile.ClampEx(br.ReadSingle(), 0.0f, 1f);
            this.bustSoftness = CharFile.ClampEx(br.ReadSingle(), 0.0f, 1f);
            this.bustWeight = CharFile.ClampEx(br.ReadSingle(), 0.0f, 1f);
            return true;
        }

        protected override bool ModCheckSub()
        {
            return !MathfEx.RangeEqualOn<float>(0.0f, this.areolaSize, 1.0f) || !MathfEx.RangeEqualOn<float>(0.0f, this.bustSoftness, 1.0f) || !MathfEx.RangeEqualOn<float>(0.0f, this.bustWeight, 1.0f);
        }
    }
}

// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoCustom
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using UnityEngine;

namespace CharacterHS
{
    [Serializable]
    public abstract class CharFileInfoCustom : BlockControlBase
    {
        public HSColorSet skinColor = new HSColorSet();
        public HSColorSet tattoo_fColor = new HSColorSet();
        public HSColorSet eyebrowColor = new HSColorSet();
        public HSColorSet eyeLColor = new HSColorSet();
        public HSColorSet eyeRColor = new HSColorSet();
        public HSColorSet eyeWColor = new HSColorSet();
        public HSColorSet tattoo_bColor = new HSColorSet();
        public string name = string.Empty;
        public float voicePitch = 1f;
        public byte sex;
        public int customLoadVersion;
        public readonly int hairKindNum;
        public float[] shapeValueFace;
        public float[] shapeValueBody;
        public int[] hairId;
        public int hairType;
        public HSColorSet[] hairColor;
        public HSColorSet[] hairAcsColor;
        public int headId;
        public int texFaceId;
        public int texTattoo_fId;
        public int matEyebrowId;
        public int matEyeLId;
        public int matEyeRId;
        public int texFaceDetailId;
        public float faceDetailWeight;
        public int texBodyId;
        public int texTattoo_bId;
        public int texBodyDetailId;
        public float bodyDetailWeight;
        public int personality;
        public bool isConcierge;

        public CharFileInfoCustom(int hknum)
          : base("カスタム情報", 4)
        {
            this.hairKindNum = hknum;
        }

        protected void MemberInitialize()
        {
            if (this.shapeValueFace != null)
            {
                for (int index = 0; index < this.shapeValueFace.Length; ++index)
                    this.shapeValueFace[index] = 0.5f;
            }
            if (this.shapeValueBody != null)
            {
                for (int index = 0; index < this.shapeValueBody.Length; ++index)
                    this.shapeValueBody[index] = 0.5f;
            }
            this.headId = 0;
            this.skinColor.SetDiffuseRGB(Color.white);
            this.skinColor.SetSpecularRGB(Color.white);
            this.texFaceId = 0;
            this.texTattoo_fId = 0;
            this.tattoo_fColor.SetDiffuseRGBA(Color.white);
            this.matEyebrowId = 0;
            this.eyebrowColor.SetDiffuseRGBA(Color.white);
            this.eyebrowColor.hsvDiffuse.S = 1f;
            this.matEyeLId = 0;
            this.eyeLColor.SetDiffuseRGBA(Color.white);
            this.eyeLColor.hsvDiffuse.S = 1f;
            this.matEyeRId = 0;
            this.eyeRColor.SetDiffuseRGBA(Color.white);
            this.eyeRColor.hsvDiffuse.S = 1f;
            this.eyeWColor.SetDiffuseRGB(Color.white);
            this.texFaceDetailId = 0;
            this.faceDetailWeight = 0.5f;
            this.texBodyId = 0;
            this.texTattoo_bId = 0;
            this.tattoo_bColor.SetDiffuseRGBA(Color.white);
            this.texBodyDetailId = 0;
            this.bodyDetailWeight = 0.0f;
            this.voicePitch = 1f;
            this.isConcierge = false;
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(1);
                    binaryWriter.Write(this.shapeValueFace.Length);
                    for (int index = 0; index < this.shapeValueFace.Length; ++index)
                        binaryWriter.Write(this.shapeValueFace[index]);
                    binaryWriter.Write(this.shapeValueBody.Length);
                    for (int index = 0; index < this.shapeValueBody.Length; ++index)
                        binaryWriter.Write(this.shapeValueBody[index]);
                    binaryWriter.Write(this.hairKindNum);
                    for (int index = 0; index < this.hairKindNum; ++index)
                    {
                        binaryWriter.Write(this.hairId[index]);
                        this.hairColor[index].Save(binaryWriter);
                        this.hairAcsColor[index].Save(binaryWriter);
                    }
                    binaryWriter.Write(this.hairType);
                    binaryWriter.Write(this.headId);
                    this.skinColor.Save(binaryWriter);
                    binaryWriter.Write(this.texFaceId);
                    binaryWriter.Write(this.texTattoo_fId);
                    this.tattoo_fColor.Save(binaryWriter);
                    binaryWriter.Write(this.matEyebrowId);
                    this.eyebrowColor.Save(binaryWriter);
                    binaryWriter.Write(this.matEyeLId);
                    this.eyeLColor.Save(binaryWriter);
                    binaryWriter.Write(this.matEyeRId);
                    this.eyeRColor.Save(binaryWriter);
                    this.eyeWColor.Save(binaryWriter);
                    binaryWriter.Write(this.texFaceDetailId);
                    binaryWriter.Write(this.faceDetailWeight);
                    binaryWriter.Write(this.texBodyId);
                    binaryWriter.Write(this.texTattoo_bId);
                    this.tattoo_bColor.Save(binaryWriter);
                    binaryWriter.Write(this.texBodyDetailId);
                    binaryWriter.Write(this.bodyDetailWeight);
                    binaryWriter.Write(this.name);
                    binaryWriter.Write(this.personality);
                    binaryWriter.Write(this.voicePitch);
                    binaryWriter.Write(this.isConcierge);
                    this.SaveSub(binaryWriter);
                    return memoryStream.ToArray();
                }
            }
        }

        public override bool LoadBytes(byte[] data, int customVer)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    int num1 = binaryReader.ReadInt32();
                    int num2 = binaryReader.ReadInt32();
                    for (int index = 0; index < num2; ++index)
                        this.shapeValueFace[index] = CharFile.ClampEx(binaryReader.ReadSingle(), 0.0f, 1f);
                    int num3 = binaryReader.ReadInt32();
                    for (int index = 0; index < num3; ++index)
                        this.shapeValueBody[index] = CharFile.ClampEx(binaryReader.ReadSingle(), 0.0f, 1f);
                    int num4 = binaryReader.ReadInt32();
                    for (int index = 0; index < num4; ++index)
                    {
                        this.hairId[index] = binaryReader.ReadInt32();
                        this.hairColor[index].Load(binaryReader, num1);
                        this.hairAcsColor[index].Load(binaryReader, num1);
                    }
                    if (2 <= customVer)
                        this.hairType = binaryReader.ReadInt32();
                    this.headId = binaryReader.ReadInt32();
                    this.skinColor.Load(binaryReader, num1);
                    this.texFaceId = binaryReader.ReadInt32();
                    this.texTattoo_fId = binaryReader.ReadInt32();
                    this.tattoo_fColor.Load(binaryReader, num1);
                    this.matEyebrowId = binaryReader.ReadInt32();
                    this.eyebrowColor.Load(binaryReader, num1);
                    this.matEyeLId = binaryReader.ReadInt32();
                    this.eyeLColor.Load(binaryReader, num1);
                    this.matEyeRId = binaryReader.ReadInt32();
                    this.eyeRColor.Load(binaryReader, num1);
                    this.eyeWColor.Load(binaryReader, num1);
                    this.texFaceDetailId = binaryReader.ReadInt32();
                    this.faceDetailWeight = binaryReader.ReadSingle();
                    this.texBodyId = binaryReader.ReadInt32();
                    this.texTattoo_bId = binaryReader.ReadInt32();
                    this.tattoo_bColor.Load(binaryReader, num1);
                    this.texBodyDetailId = binaryReader.ReadInt32();
                    this.bodyDetailWeight = binaryReader.ReadSingle();
                    this.name = binaryReader.ReadString();
                    this.personality = binaryReader.ReadInt32();
                    this.personality = 0;
                    if (3 <= customVer)
                        this.voicePitch = binaryReader.ReadSingle();
                    if (4 <= customVer)
                        this.isConcierge = binaryReader.ReadBoolean();
                    return this.LoadSub(binaryReader, customVer, num1);
                }
            }
        }

        protected abstract bool SaveSub(BinaryWriter bw);

        protected abstract bool LoadSub(BinaryReader br, int customVer, int colorVer);

        public bool ModCheck()
        {
            for (int index = 0; index < this.shapeValueFace.Length; ++index)
            {
                if (!MathfEx.RangeEqualOn<float>(0.0f, this.shapeValueFace[index], 1.0f))
                    return true;
            }
            for (int index = 0; index < this.shapeValueBody.Length; ++index)
            {
                if (!MathfEx.RangeEqualOn<float>(0.0f, this.shapeValueBody[index], 1.0f))
                    return true;
            }
            return this.ModCheckSub();
        }

        protected abstract bool ModCheckSub();
    }
}

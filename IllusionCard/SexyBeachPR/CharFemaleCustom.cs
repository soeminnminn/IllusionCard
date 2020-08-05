using System;
using System.IO;

namespace SexyBeachPR
{
    [Serializable]
    public class CharFemaleCustom : CharCustom
    {
        public int[] hairId = new int[Enum.GetNames(typeof(CharFemaleCustom.HairKind)).Length];
        public ColorSet[] hairColor = new ColorSet[Enum.GetNames(typeof(CharFemaleCustom.HairKind)).Length];
        public ColorSet[] acsColor = new ColorSet[Enum.GetNames(typeof(CharFemaleCustom.HairKind)).Length];
        public int requiredDefence = 5;
        public float bustSoftness = 0.5f;
        public float bustWeight = 0.5f;
        public bool setHair;
        public float nipSize;

        public CharFemaleCustom(CharBody cha)
          : base(cha)
        {
            for (int index = 0; index < this.hairId.Length; ++index)
            {
                this.hairId[index] = 0;
                this.hairColor[index] = new ColorSet();
                this.hairColor[index].diffuseColor = new HsvColor(30f, 0.5f, 0.7f);
                this.hairColor[index].specularColor = new HsvColor(0.0f, 0.0f, 0.5f);
                this.hairColor[index].specularIntensity = 2f;
                this.hairColor[index].specularSharpness = 5f;
                this.acsColor[index] = new ColorSet();
                this.acsColor[index].diffuseColor = new HsvColor(0.0f, 0.8f, 1f);
                this.acsColor[index].specularColor = new HsvColor(0.0f, 0.0f, 0.5f);
                this.acsColor[index].specularIntensity = 2f;
                this.acsColor[index].specularSharpness = 5f;
            }
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter((Stream)memoryStream))
                {
                    byte num = 0;
                    writer.Write(num);
                    writer.Write(this.personality);
                    writer.Write(this.name);
                    writer.Write(this.headId);
                    for (int index = 0; index < this.ShapeFaceNum; ++index)
                        writer.Write((double)this.shapeFace[index]);
                    for (int index = 0; index < this.ShapeBodyNum; ++index)
                        writer.Write((double)this.shapeBody[index]);
                    for (int index = 0; index < this.hairId.Length; ++index)
                        writer.Write(this.hairId[index]);
                    foreach (ColorSet colorSet in this.hairColor)
                        colorSet.Save(writer);
                    foreach (ColorSet colorSet in this.acsColor)
                        colorSet.Save(writer);
                    writer.Write(this.texFaceId);
                    this.skinColor.Save(writer);
                    writer.Write(this.texEyeshadowId);
                    this.eyeshadowColor.Save(writer);
                    writer.Write(this.texCheekId);
                    this.cheekColor.Save(writer);
                    writer.Write(this.texLipId);
                    this.lipColor.Save(writer);
                    writer.Write(this.texTattoo_fId);
                    this.tattoo_fColor.Save(writer);
                    writer.Write(this.texMoleId);
                    this.moleColor.Save(writer);
                    writer.Write(this.matEyebrowId);
                    this.eyebrowColor.Save(writer);
                    writer.Write(this.matEyelashesId);
                    this.eyelashesColor.Save(writer);
                    writer.Write(this.matEyeLId);
                    this.eyeLColor.Save(writer);
                    writer.Write(this.matEyeRId);
                    this.eyeRColor.Save(writer);
                    writer.Write(this.matEyeHiId);
                    this.eyeHiColor.Save(writer);
                    this.eyeWColor.Save(writer);
                    writer.Write((double)this.faceDetailWeight);
                    writer.Write(this.texBodyId);
                    writer.Write(this.texSunburnId);
                    this.sunburnColor.Save(writer);
                    writer.Write(this.texTattoo_bId);
                    this.tattoo_bColor.Save(writer);
                    writer.Write(this.matNipId);
                    this.nipColor.Save(writer);
                    writer.Write(this.matUnderHairId);
                    this.underhairColor.Save(writer);
                    this.nailColor.Save(writer);
                    writer.Write((double)this.nipSize);
                    writer.Write((double)this.bodyDetailWeight);
                    writer.Write(this.requiredDefence);
                    writer.Write((double)this.bustSoftness);
                    writer.Write((double)this.bustWeight);
                    return memoryStream.ToArray();
                }
            }
        }

        public override bool LoadBytes(byte[] data, int version)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader((Stream)memoryStream))
                {
                    reader.BaseStream.Seek(1L, SeekOrigin.Current);
                    this.personality = reader.ReadInt32();
                    if (!CharaListInfo.Instance.IsPersonality(this.personality))
                        this.personality = 14;
                    this.name = reader.ReadString();
                    this.headId = reader.ReadInt32();
                    if (version < 3)
                    {
                        for (int index = 0; index < 66; ++index)
                            this.shapeFace[index] = (float)reader.ReadDouble();
                    }
                    else
                    {
                        for (int index = 0; index < this.ShapeFaceNum; ++index)
                            this.shapeFace[index] = (float)reader.ReadDouble();
                    }
                    for (int index = 0; index < this.ShapeBodyNum; ++index)
                        this.shapeBody[index] = (float)reader.ReadDouble();
                    for (int index = 0; index < this.hairId.Length; ++index)
                        this.hairId[index] = reader.ReadInt32();
                    foreach (ColorSet colorSet in this.hairColor)
                        colorSet.Load(reader, version);
                    foreach (ColorSet colorSet in this.acsColor)
                        colorSet.Load(reader, version);
                    this.texFaceId = reader.ReadInt32();
                    this.skinColor.Load(reader, version);
                    this.texEyeshadowId = reader.ReadInt32();
                    this.eyeshadowColor.Load(reader, version);
                    this.texCheekId = reader.ReadInt32();
                    this.cheekColor.Load(reader, version);
                    this.texLipId = reader.ReadInt32();
                    this.lipColor.Load(reader, version);
                    this.texTattoo_fId = reader.ReadInt32();
                    this.tattoo_fColor.Load(reader, version);
                    this.texMoleId = reader.ReadInt32();
                    this.moleColor.Load(reader, version);
                    this.matEyebrowId = reader.ReadInt32();
                    this.eyebrowColor.Load(reader, version);
                    this.matEyelashesId = reader.ReadInt32();
                    this.eyelashesColor.Load(reader, version);
                    this.matEyeLId = reader.ReadInt32();
                    this.eyeLColor.Load(reader, version);
                    this.matEyeRId = reader.ReadInt32();
                    this.eyeRColor.Load(reader, version);
                    this.matEyeHiId = reader.ReadInt32();
                    this.eyeHiColor.Load(reader, version);
                    this.eyeWColor.Load(reader, version);
                    if (version >= 2)
                        this.faceDetailWeight = (float)reader.ReadDouble();
                    this.texBodyId = reader.ReadInt32();
                    this.texSunburnId = reader.ReadInt32();
                    this.sunburnColor.Load(reader, version);
                    this.texTattoo_bId = reader.ReadInt32();
                    this.tattoo_bColor.Load(reader, version);
                    this.matNipId = reader.ReadInt32();
                    this.nipColor.Load(reader, version);
                    this.matUnderHairId = reader.ReadInt32();
                    this.underhairColor.Load(reader, version);
                    this.nailColor.Load(reader, version);
                    if (version >= 1)
                        this.nipSize = (float)reader.ReadDouble();
                    if (version >= 2)
                        this.bodyDetailWeight = (float)reader.ReadDouble();
                    if (version >= 4)
                        this.requiredDefence = reader.ReadInt32();
                    if (version >= 5)
                    {
                        this.bustSoftness = (float)reader.ReadDouble();
                        this.bustWeight = (float)reader.ReadDouble();
                    }
                }
            }
            return true;
        }

        public enum HairKind
        {
            back,
            front,
            side,
            option,
        }
    }
}

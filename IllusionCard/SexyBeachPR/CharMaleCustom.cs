using System;
using System.IO;

namespace SexyBeachPR
{
    [Serializable]
    public class CharMaleCustom : CharCustom
    {
        public ColorSet hairColor = new ColorSet();
        public ColorSet acsColor = new ColorSet();
        public int hairId;

        public CharMaleCustom(CharBody cha)
          : base(cha)
        {
            this.hairColor.diffuseColor = new HsvColor(30f, 0.5f, 0.7f);
            this.hairColor.specularColor = new HsvColor(0.0f, 0.0f, 0.5f);
            this.hairColor.specularIntensity = 2f;
            this.hairColor.specularSharpness = 5f;
            this.acsColor.diffuseColor = new HsvColor(0.0f, 0.8f, 1f);
            this.acsColor.specularColor = new HsvColor(0.0f, 0.0f, 0.5f);
            this.acsColor.specularIntensity = 2f;
            this.acsColor.specularSharpness = 5f;
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
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
                    writer.Write(this.hairId);
                    this.hairColor.Save(writer);
                    this.acsColor.Save(writer);
                    writer.Write(this.texFaceId);
                    this.skinColor.Save(writer);
                    writer.Write(this.texTattoo_fId);
                    this.tattoo_fColor.Save(writer);
                    writer.Write(this.matEyebrowId);
                    this.eyebrowColor.Save(writer);
                    writer.Write(this.matEyeLId);
                    this.eyeLColor.Save(writer);
                    writer.Write(this.matEyeRId);
                    this.eyeRColor.Save(writer);
                    this.eyeWColor.Save(writer);
                    writer.Write((double)this.faceDetailWeight);
                    writer.Write(this.texBodyId);
                    writer.Write(this.texTattoo_bId);
                    this.tattoo_bColor.Save(writer);
                    writer.Write((double)this.bodyDetailWeight);
                    writer.Write(this.beardId);
                    this.beardColor.Save(writer);
                    return memoryStream.ToArray();
                }
            }
        }

        public override bool LoadBytes(byte[] data, int version)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(memoryStream))
                {
                    reader.BaseStream.Seek(1L, SeekOrigin.Current);
                    this.personality = reader.ReadInt32();
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
                    this.hairId = reader.ReadInt32();
                    this.hairColor.Load(reader, version);
                    this.acsColor.Load(reader, version);
                    this.texFaceId = reader.ReadInt32();
                    this.skinColor.Load(reader, version);
                    this.texTattoo_fId = reader.ReadInt32();
                    this.tattoo_fColor.Load(reader, version);
                    this.matEyebrowId = reader.ReadInt32();
                    this.eyebrowColor.Load(reader, version);
                    this.matEyeLId = reader.ReadInt32();
                    this.eyeLColor.Load(reader, version);
                    this.matEyeRId = reader.ReadInt32();
                    this.eyeRColor.Load(reader, version);
                    this.eyeWColor.Load(reader, version);
                    if (version >= 2)
                        this.faceDetailWeight = (float)reader.ReadDouble();
                    this.texBodyId = reader.ReadInt32();
                    this.texTattoo_bId = reader.ReadInt32();
                    this.tattoo_bColor.Load(reader, version);
                    if (version >= 2)
                        this.bodyDetailWeight = (float)reader.ReadDouble();
                    this.beardId = reader.ReadInt32();
                    this.beardColor.Load(reader, version);
                }
            }
            return true;
        }
    }
}

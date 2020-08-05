// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoClothes
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

namespace CharacterHS
{
    [Serializable]
    public abstract class CharFileInfoClothes
    {
        public readonly string ClothesFileMark = string.Empty;
        public readonly string ClothesFileDirectory = string.Empty;
        public string clothesFileName = string.Empty;
        public string comment = string.Empty;
        public const int ClothesFileVersion = 3;
        public byte[] clothesPNG;
        public int clothesLoadFileVersion;
        public readonly int clothesKindNum;
        public int[] clothesId;
        public HSColorSet[] clothesColor;
        public HSColorSet[] clothesColor2;
        public byte clothesTypeSex;
        private Accessory[] _accessory;

        public CharFileInfoClothes(string fileMarkName, string fileDirectory, int cknum)
        {
            this.ClothesFileMark = fileMarkName;
            this.ClothesFileDirectory = fileDirectory;
            this.clothesKindNum = cknum;
            this.clothesId = new int[this.clothesKindNum];
            this.clothesColor = new HSColorSet[this.clothesKindNum];
            this.clothesColor2 = new HSColorSet[this.clothesKindNum];
            this.accessory = new Accessory[10];
            for (int index = 0; index < this.accessory.Length; ++index)
                this.accessory[index] = new Accessory();
            this.comment = "コーディネート名";
        }

        public Accessory[] accessory
        {
            get
            {
                return this._accessory;
            }
            private set
            {
                this._accessory = value;
            }
        }

        protected void MemberInitialize()
        {
        }

        public abstract bool Copy(CharFileInfoClothes srcData);

        public string ConvertClothesFilePath(string path, bool newFile = false)
        {
            string str1 = string.Empty;
            string path1 = string.Empty;
            if (path != string.Empty)
            {
                str1 = Path.GetDirectoryName(path);
                path1 = Path.GetFileName(path);
            }
            string str2 = !(str1 == string.Empty) ? str1 + "/" : UserData.Path + this.ClothesFileDirectory;
            if (path1 == string.Empty)
            {
                if (newFile || this.clothesFileName == string.Empty)
                {
                    string empty = string.Empty;
                    path1 = this.clothesTypeSex != 0 ? "coordF" + empty + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") : "coordM" + empty + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                }
                else
                    path1 = this.clothesFileName;
            }
            this.clothesFileName = Path.GetFileNameWithoutExtension(path1) + ".png";
            return str2 + this.clothesFileName;
        }

        public bool Save(string path)
        {
            string path1 = this.ConvertClothesFilePath(path, false);
            string directoryName = Path.GetDirectoryName(path1);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fileStream = new FileStream(path1, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fileStream))
                    return this.Save(bw);
            }
        }

        public bool Save(BinaryWriter bw)
        {
            if (this.clothesPNG != null)
                bw.Write(this.clothesPNG);
            long position = bw.BaseStream.Position;
            this.SaveWithoutPNG(bw);
            bw.Write(position);
            return true;
        }

        public bool SaveWithoutPNG(BinaryWriter bw)
        {
            bw.Write(this.ClothesFileMark);
            bw.Write(3);
            bw.Write(1);
            bw.Write(this.clothesKindNum);
            for (int index = 0; index < this.clothesKindNum; ++index)
            {
                bw.Write(this.clothesId[index]);
                this.clothesColor[index].Save(bw);
                this.clothesColor2[index].Save(bw);
            }
            bw.Write(10);
            for (int index = 0; index < 10; ++index)
                this.accessory[index].Save(bw);
            bw.Write(this.comment);
            bw.Write(this.clothesTypeSex);
            return this.SaveSub(bw);
        }

        public bool Load(string path, bool noSetPng = false)
        {
            string path1 = this.ConvertClothesFilePath(path, false);
            if (!File.Exists(path1))
                return false;
            this.clothesFileName = Path.GetFileName(path1);
            using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fileStream))
                    return this.Load(br, noSetPng);
            }
        }

        public bool Load(Stream st, bool noSetPNG = false)
        {
            using (BinaryReader br = new BinaryReader(st))
                return this.Load(br, noSetPNG);
        }

        public bool Load(BinaryReader br, bool noSetPng = false)
        {
            long size = 0;
            if (noSetPng)
                PngAssist.CheckPngData(br, ref size, true);
            else
                this.clothesPNG = PngAssist.LoadPngData(br);
            if (!this.LoadWithoutPNG(br))
                return false;
            br.ReadInt64();
            return true;
        }

        public bool LoadWithoutPNG(BinaryReader br)
        {
            if (br.BaseStream.Length - br.BaseStream.Position == 0L)
                return false;
            try
            {
                if (br.ReadString() != this.ClothesFileMark)
                    return false;
                this.clothesLoadFileVersion = br.ReadInt32();
                if (this.clothesLoadFileVersion > 3)
                    return false;
                int num1 = br.ReadInt32();
                int num2 = br.ReadInt32();
                for (int index = 0; index < num2; ++index)
                {
                    this.clothesId[index] = br.ReadInt32();
                    this.clothesColor[index].Load(br, num1);
                    if (3 <= this.clothesLoadFileVersion)
                        this.clothesColor2[index].Load(br, num1);
                }
                int num3 = br.ReadInt32();
                for (int index = 0; index < num3; ++index)
                    this.accessory[index].Load(br, this.clothesLoadFileVersion, num1);
                if (2 <= this.clothesLoadFileVersion)
                {
                    this.comment = br.ReadString();
                    this.clothesTypeSex = br.ReadByte();
                }
                return this.LoadSub(br, this.clothesLoadFileVersion, num1);
            }
            catch (EndOfStreamException)
            {
            }
            return false;
        }

        protected abstract bool SaveSub(BinaryWriter bw);

        protected abstract bool LoadSub(BinaryReader br, int clothesVer, int colorVer);

        [Serializable]
        public class Accessory : ISerializable
        {
            public int type = -1;
            public int id = -1;
            public string parentKey = string.Empty;
            public Vector3 addPos = Vector3.zero;
            public Vector3 addRot = Vector3.zero;
            public Vector3 addScl = Vector3.one;
            public HSColorSet color = new HSColorSet();
            public HSColorSet color2 = new HSColorSet();

            public Accessory()
            {
            }

            protected Accessory(SerializationInfo info, StreamingContext context)
            {
                this.type = info.GetInt32(nameof(type));
                this.id = info.GetInt32(nameof(id));
                this.parentKey = info.GetString(nameof(parentKey));
                this.addPos.x = info.GetSingle("addPosX");
                this.addPos.y = info.GetSingle("addPosY");
                this.addPos.z = info.GetSingle("addPosZ");
                this.addRot.x = info.GetSingle("addRotX");
                this.addRot.y = info.GetSingle("addRotY");
                this.addRot.z = info.GetSingle("addRotZ");
                this.addScl.x = info.GetSingle("addSclX");
                this.addScl.y = info.GetSingle("addSclY");
                this.addScl.z = info.GetSingle("addSclZ");
                this.color.hsvDiffuse.H = info.GetSingle("diffuseH");
                this.color.hsvDiffuse.S = info.GetSingle("diffuseS");
                this.color.hsvDiffuse.V = info.GetSingle("diffuseV");
                this.color.alpha = info.GetSingle("alpha");
                this.color.hsvSpecular.H = info.GetSingle("specularH");
                this.color.hsvSpecular.S = info.GetSingle("specularS");
                this.color.hsvSpecular.V = info.GetSingle("specularV");
                this.color.specularIntensity = info.GetSingle("specularIntensity");
                this.color.specularSharpness = info.GetSingle("specularSharpness");
                this.color2.hsvDiffuse.H = info.GetSingle("diffuseH2");
                this.color2.hsvDiffuse.S = info.GetSingle("diffuseS2");
                this.color2.hsvDiffuse.V = info.GetSingle("diffuseV2");
                this.color2.alpha = info.GetSingle("alpha2");
                this.color2.hsvSpecular.H = info.GetSingle("specularH2");
                this.color2.hsvSpecular.S = info.GetSingle("specularS2");
                this.color2.hsvSpecular.V = info.GetSingle("specularV2");
                this.color2.specularIntensity = info.GetSingle("specularIntensity2");
                this.color2.specularSharpness = info.GetSingle("specularSharpness2");
            }

            public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("type", this.type);
                info.AddValue("id", this.id);
                info.AddValue("parentKey", parentKey);
                info.AddValue("addPosX", this.addPos.x);
                info.AddValue("addPosY", this.addPos.y);
                info.AddValue("addPosZ", this.addPos.z);
                info.AddValue("addRotX", this.addRot.x);
                info.AddValue("addRotY", this.addRot.y);
                info.AddValue("addRotZ", this.addRot.z);
                info.AddValue("addSclX", this.addScl.x);
                info.AddValue("addSclY", this.addScl.y);
                info.AddValue("addSclZ", this.addScl.z);
                info.AddValue("diffuseH", this.color.hsvDiffuse.H);
                info.AddValue("diffuseS", this.color.hsvDiffuse.S);
                info.AddValue("diffuseV", this.color.hsvDiffuse.V);
                info.AddValue("alpha", this.color.alpha);
                info.AddValue("specularH", this.color.hsvSpecular.H);
                info.AddValue("specularS", this.color.hsvSpecular.S);
                info.AddValue("specularV", this.color.hsvSpecular.V);
                info.AddValue("specularIntensity", this.color.specularIntensity);
                info.AddValue("specularSharpness", this.color.specularSharpness);
                info.AddValue("diffuseH2", this.color2.hsvDiffuse.H);
                info.AddValue("diffuseS2", this.color2.hsvDiffuse.S);
                info.AddValue("diffuseV2", this.color2.hsvDiffuse.V);
                info.AddValue("alpha2", this.color2.alpha);
                info.AddValue("specularH2", this.color2.hsvSpecular.H);
                info.AddValue("specularS2", this.color2.hsvSpecular.S);
                info.AddValue("specularV2", this.color2.hsvSpecular.V);
                info.AddValue("specularIntensity2", this.color2.specularIntensity);
                info.AddValue("specularSharpness2", this.color2.specularSharpness);
            }

            public void MemberInitialize()
            {
                this.type = -1;
                this.id = -1;
                this.parentKey = string.Empty;
                this.addPos = Vector3.zero;
                this.addRot = Vector3.zero;
                this.addScl = Vector3.one;
                this.color = new HSColorSet();
                this.color2 = new HSColorSet();
            }

            public void Copy(Accessory src)
            {
                this.type = src.type;
                this.id = src.id;
                this.parentKey = src.parentKey;
                this.addPos = src.addPos;
                this.addRot = src.addRot;
                this.addScl = src.addScl;
                this.color.Copy(src.color);
                this.color2.Copy(src.color2);
            }

            public void Save(BinaryWriter writer)
            {
                writer.Write(this.type);
                writer.Write(this.id);
                writer.Write(this.parentKey);
                writer.Write(this.addPos.x);
                writer.Write(this.addPos.y);
                writer.Write(this.addPos.z);
                writer.Write(this.addRot.x);
                writer.Write(this.addRot.y);
                writer.Write(this.addRot.z);
                writer.Write(this.addScl.x);
                writer.Write(this.addScl.y);
                writer.Write(this.addScl.z);
                this.color.Save(writer);
                this.color2.Save(writer);
            }

            public void Load(BinaryReader reader, int verClothes, int verColor)
            {
                this.type = reader.ReadInt32();
                this.id = reader.ReadInt32();
                this.parentKey = reader.ReadString();
                this.addPos.x = reader.ReadSingle();
                this.addPos.y = reader.ReadSingle();
                this.addPos.z = reader.ReadSingle();
                this.addRot.x = reader.ReadSingle();
                this.addRot.y = reader.ReadSingle();
                this.addRot.z = reader.ReadSingle();
                this.addScl.x = reader.ReadSingle();
                this.addScl.y = reader.ReadSingle();
                this.addScl.z = reader.ReadSingle();
                this.color.Load(reader, verColor);
                if (3 > verClothes)
                    return;
                this.color2.Load(reader, verColor);
            }
        }
    }
}

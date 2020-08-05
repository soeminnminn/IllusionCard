// Decompiled with JetBrains decompiler
// Type: CharSave_SexyBeachPR
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using SaveAssist;
using System.IO;
using UnityEngine;

public class CharSave_SexyBeachPR
{
    public const string CharaFileFemaleDir = "chara/female/";
    public const string CharaFileMaleDir = "chara/male/";
    public const string CharaFileFemaleMark = "【PremiumResortCharaFemale】";
    public const string CharaFileMaleMark = "【PremiumResortCharaMale】";

    public const int CharaFileVersion = 1;
    public const int PreviewVersion = 1;
    public const int CustomVersion = 5;
    public const int ClothesVersion = 2;
    public const int MaleShapeFaceNum = 67;
    public const int MaleShapeBodyNum = 21;
    public const int FemaleShapeFaceNum = 67;
    public const int FemaleShapeBodyNum = 32;
    public const int CoordinateNum = 2;
    public const int AccessorySlotNum = 5;

    public CharSave_SexyBeachPR(ConvertType type)
    {
        this.convertType = type;
        this.savedata = new SaveData();
    }

    public SaveData savedata { get; private set; }

    public ConvertType convertType { get; private set; }

    public string GetFileTag(string filepath = "")
    {
        string str = string.Empty;
        if (!File.Exists(filepath))
            return str;
        using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                if (reader.BaseStream.Length == 0L)
                    return str;
                long size = 0;
                PngAssist.CheckPngData(reader, ref size, true);
                if (reader.BaseStream.Length - reader.BaseStream.Position == 0L)
                    return str;
                try
                {
                    str = reader.ReadString();
                }
                catch (EndOfStreamException)
                {
                }
            }
        }
        return str;
    }

    public bool LoadCharaFile(string filepath = "")
    {
        if (!File.Exists(filepath))
            return false;
        this.savedata.fileName = Path.GetFileName(filepath);
        bool flag;
        using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                if (reader.BaseStream.Length == 0L)
                {
                    flag = false;
                }
                else
                {
                    this.savedata.pngData = PngAssist.LoadPngData(reader);
                    if (reader.BaseStream.Length - reader.BaseStream.Position == 0L)
                    {
                        flag = false;
                    }
                    else
                    {
                        try
                        {
                            string str = reader.ReadString();
                            byte sex;
                            if (str == "【PremiumResortCharaFemale】")
                            {
                                sex = 1;
                            }
                            else
                            {
                                if (!(str == "【PremiumResortCharaMale】"))
                                    return false;
                                sex = 0;
                            }
                            this.savedata.sex = sex;
                            this.savedata.fileVersion = reader.ReadInt32();
                            if (this.savedata.fileVersion > 1)
                            {
                                flag = false;
                            }
                            else
                            {
                                if (this.savedata.pngData != null && this.convertType == ConvertType.HoneySelect)
                                {
                                    this.savedata.pngData = PngAssist.ResizePng(this.savedata.pngData, 252, 352);
                                }
                                int length = reader.ReadInt32();
                                BlockHeader[] blockHeaderArray = new BlockHeader[length];
                                for (int index = 0; index < length; ++index)
                                {
                                    blockHeaderArray[index] = new BlockHeader();
                                    blockHeaderArray[index].LoadHeader(reader);
                                }
                                int index1 = 0 + 1;
                                this.savedata.customVersion = blockHeaderArray[index1].version;
                                if (this.savedata.customVersion <= 5)
                                {
                                    reader.BaseStream.Seek(blockHeaderArray[index1].pos, SeekOrigin.Begin);
                                    if (!this.LoadCustomData(reader.ReadBytes((int)blockHeaderArray[index1].size), blockHeaderArray[index1].version, sex))
                                        return false;
                                }
                                int index2 = index1 + 1;
                                this.savedata.clothesVersion = blockHeaderArray[index2].version;
                                if (blockHeaderArray[index2].version <= 2)
                                {
                                    reader.BaseStream.Seek(blockHeaderArray[index2].pos, SeekOrigin.Begin);
                                    if (!this.LoadClothesData(reader.ReadBytes((int)blockHeaderArray[index2].size), blockHeaderArray[index2].version, sex))
                                        return false;
                                }
                                flag = true;
                            }
                        }
                        catch (EndOfStreamException)
                        {
                            flag = false;
                        }
                    }
                }
            }
        }
        return flag;
    }

    public bool LoadCharaFile(BinaryReader br)
    {
        if (br.BaseStream.Length == 0L)
        {
            Debug.LogError("データが破損しています。");
            return false;
        }
        this.savedata.pngData = PngAssist.LoadPngData(br);
        string str = br.ReadString();
        byte sex;
        if (str == "【PremiumResortCharaFemale】")
            sex = 1;
        else if (str == "【PremiumResortCharaMale】")
        {
            sex = 0;
        }
        else
        {
            Debug.LogError("ファイルの種類が違います。");
            return false;
        }
        this.savedata.sex = sex;
        this.savedata.fileVersion = br.ReadInt32();
        if (this.savedata.fileVersion > 1)
        {
            Debug.LogError("実行ファイルよりも新しいキャラファイルです。");
            return false;
        }
        if (this.savedata.pngData != null && this.convertType == ConvertType.HoneySelect)
        {
            this.savedata.pngData = PngAssist.ResizePng(this.savedata.pngData, 252, 352);
        }
        int length = br.ReadInt32();
        BlockHeader[] blockHeaderArray = new BlockHeader[length];
        for (int index = 0; index < length; ++index)
        {
            blockHeaderArray[index] = new BlockHeader();
            blockHeaderArray[index].LoadHeader(br);
        }
        int index1 = 0 + 1;
        this.savedata.customVersion = blockHeaderArray[index1].version;
        if (this.savedata.customVersion > 5)
        {
            Debug.LogError("実行ファイルよりも新しいカスタム設定です。");
        }
        else
        {
            br.BaseStream.Seek(blockHeaderArray[index1].pos, SeekOrigin.Begin);
            if (!this.LoadCustomData(br.ReadBytes((int)blockHeaderArray[index1].size), blockHeaderArray[index1].version, sex))
            {
                Debug.LogError("カスタム設定の読み込みに失敗しました");
                return false;
            }
        }
        int index2 = index1 + 1;
        this.savedata.clothesVersion = blockHeaderArray[index2].version;
        if (blockHeaderArray[index2].version > 2)
        {
            Debug.LogError("実行ファイルよりも新しい服装設定です。");
        }
        else
        {
            br.BaseStream.Seek(blockHeaderArray[index2].pos, SeekOrigin.Begin);
            if (!this.LoadClothesData(br.ReadBytes((int)blockHeaderArray[index2].size), blockHeaderArray[index2].version, sex))
            {
                Debug.LogError("服装設定の読み込みに失敗しました");
                return false;
            }
        }
        return true;
    }

    public bool LoadCustomData(byte[] data, int version, byte sex)
    {
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            using (BinaryReader reader = new BinaryReader(memoryStream))
            {
                reader.BaseStream.Seek(1L, SeekOrigin.Current);
                this.savedata.personality = reader.ReadInt32();
                this.savedata.name = reader.ReadString();
                this.savedata.headId = reader.ReadInt32();
                this.savedata.sbpr_shapeFace = new float[sex != 0 ? 67 : 67];
                if (version < 3)
                {
                    for (int index = 0; index < 66; ++index)
                        this.savedata.sbpr_shapeFace[index] = (float)reader.ReadDouble();
                }
                else
                {
                    for (int index = 0; index < this.savedata.sbpr_shapeFace.Length; ++index)
                        this.savedata.sbpr_shapeFace[index] = (float)reader.ReadDouble();
                }
                this.savedata.sbpr_shapeBody = new float[sex != 0 ? 32 : 21];
                for (int index = 0; index < this.savedata.sbpr_shapeBody.Length; ++index)
                    this.savedata.sbpr_shapeBody[index] = (float)reader.ReadDouble();
                this.savedata.hairId = new int[sex != 0 ? 4 : 1];
                for (int index = 0; index < this.savedata.hairId.Length; ++index)
                    this.savedata.hairId[index] = reader.ReadInt32();
                this.savedata.hairColor = new ColorSetSBPR[sex != 0 ? 4 : 1];
                for (int index = 0; index < this.savedata.hairColor.Length; ++index)
                {
                    this.savedata.hairColor[index] = new ColorSetSBPR();
                    this.savedata.hairColor[index].Load(reader, (int)this.convertType);
                }
                this.savedata.hairAcsColor = new ColorSetSBPR[sex != 0 ? 4 : 1];
                for (int index = 0; index < this.savedata.hairAcsColor.Length; ++index)
                {
                    this.savedata.hairAcsColor[index] = new ColorSetSBPR();
                    this.savedata.hairAcsColor[index].Load(reader, (int)this.convertType);
                }
                this.savedata.texFaceId = reader.ReadInt32();
                this.savedata.skinColor = new ColorSetSBPR();
                this.savedata.skinColor.Load(reader, (int)this.convertType);
                if (sex == 1)
                {
                    this.savedata.texEyeshadowId = reader.ReadInt32();
                    this.savedata.eyeshadowColor = new ColorSetSBPR();
                    this.savedata.eyeshadowColor.Load(reader, (int)this.convertType);
                    this.savedata.texCheekId = reader.ReadInt32();
                    this.savedata.cheekColor = new ColorSetSBPR();
                    this.savedata.cheekColor.Load(reader, (int)this.convertType);
                    this.savedata.texLipId = reader.ReadInt32();
                    this.savedata.lipColor = new ColorSetSBPR();
                    this.savedata.lipColor.Load(reader, (int)this.convertType);
                }
                this.savedata.texTattoo_fId = reader.ReadInt32();
                this.savedata.tattoo_fColor = new ColorSetSBPR();
                this.savedata.tattoo_fColor.Load(reader, (int)this.convertType);
                if (sex == 1)
                {
                    this.savedata.texMoleId = reader.ReadInt32();
                    this.savedata.moleColor = new ColorSetSBPR();
                    this.savedata.moleColor.Load(reader, (int)this.convertType);
                }
                this.savedata.matEyebrowId = reader.ReadInt32();
                this.savedata.eyebrowColor = new ColorSetSBPR();
                this.savedata.eyebrowColor.Load(reader, (int)this.convertType);
                if (sex == 1)
                {
                    this.savedata.matEyelashesId = reader.ReadInt32();
                    this.savedata.eyelashesColor = new ColorSetSBPR();
                    this.savedata.eyelashesColor.Load(reader, (int)this.convertType);
                }
                this.savedata.matEyeLId = reader.ReadInt32();
                this.savedata.eyeLColor = new ColorSetSBPR();
                this.savedata.eyeLColor.Load(reader, (int)this.convertType);
                this.savedata.matEyeRId = reader.ReadInt32();
                this.savedata.eyeRColor = new ColorSetSBPR();
                this.savedata.eyeRColor.Load(reader, (int)this.convertType);
                if (sex == 1)
                {
                    this.savedata.matEyeHiId = reader.ReadInt32();
                    this.savedata.eyeHiColor = new ColorSetSBPR();
                    this.savedata.eyeHiColor.Load(reader, (int)this.convertType);
                }
                this.savedata.eyeWColor = new ColorSetSBPR();
                this.savedata.eyeWColor.Load(reader, (int)this.convertType);
                if (version >= 2)
                    this.savedata.faceDetailWeight = (float)reader.ReadDouble();
                this.savedata.texBodyId = reader.ReadInt32();
                if (sex == 1)
                {
                    this.savedata.texSunburnId = reader.ReadInt32();
                    this.savedata.sunburnColor = new ColorSetSBPR();
                    this.savedata.sunburnColor.Load(reader, (int)this.convertType);
                }
                this.savedata.texTattoo_bId = reader.ReadInt32();
                this.savedata.tattoo_bColor = new ColorSetSBPR();
                this.savedata.tattoo_bColor.Load(reader, (int)this.convertType);
                if (sex == 1)
                {
                    this.savedata.matNipId = reader.ReadInt32();
                    this.savedata.nipColor = new ColorSetSBPR();
                    this.savedata.nipColor.Load(reader, (int)this.convertType);
                    this.savedata.matUnderhairId = reader.ReadInt32();
                    this.savedata.underhairColor = new ColorSetSBPR();
                    this.savedata.underhairColor.Load(reader, (int)this.convertType);
                    this.savedata.nailColor = new ColorSetSBPR();
                    this.savedata.nailColor.Load(reader, (int)this.convertType);
                    if (version >= 1)
                        this.savedata.nipSize = (float)reader.ReadDouble();
                }
                if (version >= 2)
                {
                    this.savedata.bodyDetailWeight = (float)reader.ReadDouble();
                    if (this.convertType == ConvertType.HoneySelect)
                        this.savedata.bodyDetailWeight *= 0.6f;
                }
                if (sex == 0)
                {
                    this.savedata.beardId = reader.ReadInt32();
                    this.savedata.beardColor = new ColorSetSBPR();
                    this.savedata.beardColor.Load(reader, (int)this.convertType);
                }
                else
                {
                    if (version >= 4)
                        reader.BaseStream.Seek(4L, SeekOrigin.Current);
                    if (version >= 5)
                    {
                        this.savedata.bustSoftness = (float)reader.ReadDouble();
                        this.savedata.bustWeight = (float)reader.ReadDouble();
                    }
                }
                if (this.convertType == ConvertType.HoneySelect)
                {
                    if (sex == 1)
                    {
                        if (14 > this.savedata.personality || this.savedata.personality > 18)
                            this.savedata.personality = 0;
                        else
                            this.savedata.personality -= 14;
                        this.savedata.hairType = new int[41]
                        {
                            0, 2, 3, 3, 0, 1, 2, 2, 1, 4, 2, 5, 0, 4, 1, 5, 4, 4, 3, 3, 3, 
                            2, 3, 4, 5, 0, 5, 0, 1, 5, 0, 0, 2, 5, 3, 3, 5, 5, 5, 2, 0
                        }[this.savedata.hairId[0]];
                    }
                    else
                        this.savedata.personality = 0;
                    this.ConvertShapeValue();
                }
                return true;
            }
        }
    }

    public void ConvertShapeValue()
    {
        if (this.convertType != ConvertType.HoneySelect)
            return;
        if (this.savedata.sex == 0)
        {
            this.savedata.shapeBody = new float[21];
            for (int index = 0; index < this.savedata.shapeBody.Length; ++index)
                this.savedata.shapeBody[index] = this.savedata.sbpr_shapeBody[index];
            this.savedata.shapeFace = new float[67];
            int[] numArray = new int[67]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 27, 28, 29, 30, 31, 32, 49, 65, 50, 51, 52,
                33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 13, 14, 15, 16, 17, 18,
                19, 20, 21, 66, 22, 23, 24, 25, 26, 58, 59, 60, 61, 62, 63, 64, 53, 54, 55, 56, 57
            };
            for (int index = 0; index < 67; ++index)
                this.savedata.shapeFace[index] = this.savedata.sbpr_shapeFace[numArray[index]];
        }
        else
        {
            this.savedata.shapeBody = new float[32];
            for (int index = 0; index < this.savedata.shapeBody.Length; ++index)
                this.savedata.shapeBody[index] = this.savedata.sbpr_shapeBody[index];
            this.savedata.shapeFace = new float[67];
            int[] numArray = new int[67]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 27, 28, 29, 30, 31, 32, 48, 49, 50, 51, 52,
                33, 34, 35, 36, 37, 38, 65, 39, 40, 41, 42, 43, 44, 45, 46, 47, 13, 14, 15, 16, 17, 18,
                19, 20, 21, 66, 22, 23, 24, 25, 26, 58, 59, 60, 61, 62, 63, 64, 53, 54, 55, 56, 57
            };
            for (int index = 0; index < 67; ++index)
                this.savedata.shapeFace[index] = this.savedata.sbpr_shapeFace[numArray[index]];
        }
    }

    public bool LoadClothesData(byte[] data, int version, byte sex)
    {
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            using (BinaryReader reader = new BinaryReader(memoryStream))
            {
                this.savedata.coord = new Coordinate[2];
                reader.BaseStream.Seek(1L, SeekOrigin.Current);
                for (int index = 0; index < this.savedata.coord.Length; ++index)
                {
                    this.savedata.coord[index] = new Coordinate();
                    if (sex == 0)
                    {
                        this.savedata.coord[index].clothesTopId = reader.ReadInt32();
                        this.savedata.coord[index].shoesId = reader.ReadInt32();
                        if (version >= 2)
                        {
                            this.savedata.coord[index].clothesTopColor = new ColorSetSBPR();
                            this.savedata.coord[index].clothesTopColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].shoesColor = new ColorSetSBPR();
                            this.savedata.coord[index].shoesColor.Load(reader, (int)this.convertType);
                        }
                    }
                    else
                    {
                        this.savedata.coord[index].clothesTopId = reader.ReadInt32();
                        this.savedata.coord[index].clothesBotId = reader.ReadInt32();
                        this.savedata.coord[index].braId = reader.ReadInt32();
                        this.savedata.coord[index].shortsId = reader.ReadInt32();
                        this.savedata.coord[index].glovesId = reader.ReadInt32();
                        this.savedata.coord[index].panstId = reader.ReadInt32();
                        this.savedata.coord[index].socksId = reader.ReadInt32();
                        this.savedata.coord[index].shoesId = reader.ReadInt32();
                        this.savedata.coord[index].swimsuitId = reader.ReadInt32();
                        this.savedata.coord[index].swimTopId = reader.ReadInt32();
                        this.savedata.coord[index].swimBotId = reader.ReadInt32();
                        if (this.convertType == ConvertType.HoneySelect)
                        {
                            if (this.savedata.coord[index].clothesTopId == 102 || this.savedata.coord[index].clothesTopId == 111)
                                this.savedata.coord[index].clothesTopId = 101;
                            if (this.savedata.coord[index].clothesBotId == 82)
                                this.savedata.coord[index].clothesBotId = 0;
                            if (this.savedata.coord[index].swimsuitId == 46)
                                this.savedata.coord[index].swimsuitId = 50;
                        }
                        if (version >= 2)
                        {
                            this.savedata.coord[index].clothesTopColor = new ColorSetSBPR();
                            this.savedata.coord[index].clothesTopColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].clothesBotColor = new ColorSetSBPR();
                            this.savedata.coord[index].clothesBotColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].braColor = new ColorSetSBPR();
                            this.savedata.coord[index].braColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].shortsColor = new ColorSetSBPR();
                            this.savedata.coord[index].shortsColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].glovesColor = new ColorSetSBPR();
                            this.savedata.coord[index].glovesColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].panstColor = new ColorSetSBPR();
                            this.savedata.coord[index].panstColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].socksColor = new ColorSetSBPR();
                            this.savedata.coord[index].socksColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].shoesColor = new ColorSetSBPR();
                            this.savedata.coord[index].shoesColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].swimsuitColor = new ColorSetSBPR();
                            this.savedata.coord[index].swimsuitColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].swimTopColor = new ColorSetSBPR();
                            this.savedata.coord[index].swimTopColor.Load(reader, (int)this.convertType);
                            this.savedata.coord[index].swimBotColor = new ColorSetSBPR();
                            this.savedata.coord[index].swimBotColor.Load(reader, (int)this.convertType);
                        }
                    }
                }
                if (sex == 0)
                {
                    if (version >= 2)
                        reader.BaseStream.Seek(1L, SeekOrigin.Current);
                }
                else if (version >= 1)
                {
                    this.savedata.stateSwimOptTop = reader.ReadByte();
                    this.savedata.stateSwimOptBot = reader.ReadByte();
                    reader.BaseStream.Seek(1L, SeekOrigin.Current);
                }
                this.savedata.accessory = new Accessory[2, 5];
                this.savedata.accessoryColor = new ColorSetSBPR[2, 5];
                for (int index1 = 0; index1 < 2; ++index1)
                {
                    for (int index2 = 0; index2 < 5; ++index2)
                    {
                        this.savedata.accessory[index1, index2] = new Accessory();
                        this.savedata.accessory[index1, index2].Load(reader);
                        if (version >= 2)
                        {
                            this.savedata.accessoryColor[index1, index2] = new ColorSetSBPR();
                            this.savedata.accessoryColor[index1, index2].Load(reader, (int)this.convertType);
                        }
                    }
                }
                return true;
            }
        }
    }

    public class ColorSetSBPR
    {
        public HsvColor diffuseColor = new HsvColor(20f, 0.8f, 0.8f);
        public float alpha = 1f;
        public HsvColor specularColor = new HsvColor(0.0f, 0.0f, 0.8f);
        public float specularIntensity = 0.5f;
        public float specularSharpness = 3f;

        public void Load(BinaryReader reader, int convertType)
        {
            this.diffuseColor.H = (float)reader.ReadDouble();
            this.diffuseColor.S = (float)reader.ReadDouble();
            this.diffuseColor.V = (float)reader.ReadDouble();
            this.alpha = (float)reader.ReadDouble();
            this.specularColor.H = (float)reader.ReadDouble();
            this.specularColor.S = (float)reader.ReadDouble();
            this.specularColor.V = (float)reader.ReadDouble();
            this.specularIntensity = (float)reader.ReadDouble();
            this.specularSharpness = (float)reader.ReadDouble();
            if (convertType != 0)
                return;
            this.specularIntensity = Mathf.InverseLerp(0.0f, 5f, this.specularIntensity) * 0.8f;
            this.specularSharpness = Mathf.InverseLerp(0.0f, 9f, this.specularSharpness) * 0.9f;
        }

        public void CopyToHoneySelect(HSColorSet dst)
        {
            dst.hsvDiffuse.Copy(this.diffuseColor);
            dst.alpha = this.alpha;
            dst.hsvSpecular.Copy(this.specularColor);
            dst.specularIntensity = this.specularIntensity;
            dst.specularSharpness = this.specularSharpness;
        }
    }

    public class Coordinate
    {
        public int clothesTopId;
        public ColorSetSBPR clothesTopColor;
        public int clothesBotId;
        public ColorSetSBPR clothesBotColor;
        public int braId;
        public ColorSetSBPR braColor;
        public int shortsId;
        public ColorSetSBPR shortsColor;
        public int glovesId;
        public ColorSetSBPR glovesColor;
        public int panstId;
        public ColorSetSBPR panstColor;
        public int socksId;
        public ColorSetSBPR socksColor;
        public int shoesId;
        public ColorSetSBPR shoesColor;
        public int swimsuitId;
        public ColorSetSBPR swimsuitColor;
        public int swimTopId;
        public ColorSetSBPR swimTopColor;
        public int swimBotId;
        public ColorSetSBPR swimBotColor;
    }

    public class Accessory
    {
        public int accessoryType = -1;
        public int accessoryId = -1;
        public string parentKey = string.Empty;
        public Vector3 plusPos = Vector3.zero;
        public Vector3 plusRot = Vector3.zero;
        public Vector3 plusScl = Vector3.one;

        public void Load(BinaryReader reader)
        {
            this.accessoryType = reader.ReadInt32();
            this.accessoryId = reader.ReadInt32();
            this.parentKey = reader.ReadString();
            this.plusPos.x = (float)reader.ReadDouble();
            this.plusPos.y = (float)reader.ReadDouble();
            this.plusPos.z = (float)reader.ReadDouble();
            this.plusRot.x = (float)reader.ReadDouble();
            this.plusRot.y = (float)reader.ReadDouble();
            this.plusRot.z = (float)reader.ReadDouble();
            this.plusScl.x = (float)reader.ReadDouble();
            this.plusScl.y = (float)reader.ReadDouble();
            this.plusScl.z = (float)reader.ReadDouble();
        }
    }

    public class SaveData
    {
        public string fileName = string.Empty;
        public string name = string.Empty;
        public float nipSize = 0.5f;
        public float bustSoftness = 0.5f;
        public float bustWeight = 0.5f;
        public byte[] pngData;
        public int fileVersion;
        public byte sex;
        public int customVersion;
        public int personality;
        public int headId;
        public float[] sbpr_shapeFace;
        public float[] sbpr_shapeBody;
        public float[] shapeFace;
        public float[] shapeBody;
        public int[] hairId;
        public int hairType;
        public ColorSetSBPR[] hairColor;
        public ColorSetSBPR[] hairAcsColor;
        public int texFaceId;
        public ColorSetSBPR skinColor;
        public int texEyeshadowId;
        public ColorSetSBPR eyeshadowColor;
        public int texCheekId;
        public ColorSetSBPR cheekColor;
        public int texLipId;
        public ColorSetSBPR lipColor;
        public int texTattoo_fId;
        public ColorSetSBPR tattoo_fColor;
        public int texMoleId;
        public ColorSetSBPR moleColor;
        public int matEyebrowId;
        public ColorSetSBPR eyebrowColor;
        public int matEyelashesId;
        public ColorSetSBPR eyelashesColor;
        public int matEyeLId;
        public ColorSetSBPR eyeLColor;
        public int matEyeRId;
        public ColorSetSBPR eyeRColor;
        public int matEyeHiId;
        public ColorSetSBPR eyeHiColor;
        public ColorSetSBPR eyeWColor;
        public float faceDetailWeight;
        public int texBodyId;
        public int texSunburnId;
        public ColorSetSBPR sunburnColor;
        public int texTattoo_bId;
        public ColorSetSBPR tattoo_bColor;
        public int matNipId;
        public ColorSetSBPR nipColor;
        public int matUnderhairId;
        public ColorSetSBPR underhairColor;
        public ColorSetSBPR nailColor;
        public float bodyDetailWeight;
        public int beardId;
        public ColorSetSBPR beardColor;
        public int clothesVersion;
        public Coordinate[] coord;
        public byte stateSwimOptTop;
        public byte stateSwimOptBot;
        public Accessory[,] accessory;
        public ColorSetSBPR[,] accessoryColor;
    }

    public enum ConvertType
    {
        HoneySelect,
    }
}

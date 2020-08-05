// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFile
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DeepCopy;
using UnityEngine;

namespace CharacterHS
{
    public abstract class CharFile
    {
        public readonly string CharaFileMark = string.Empty;
        public readonly string CharaFileDirectory = string.Empty;
        public string charaFileName = string.Empty;
        public string blockFileName = string.Empty;
        public const int CharaFileVersion = 2;
        public byte[] charaFilePNG;
        public int charaLoadFileVersion;

        public CharFile(string fileMarkName, string fileDirectory)
        {
            this.CharaFileMark = fileMarkName;
            this.CharaFileDirectory = fileDirectory;
            this.previewInfo = new CharFileInfoPreview();
            this.customInfo = null;
            this.coordinateInfo = null;
            this.statusInfo = null;
            this.parameterInfo = null;
            this.clothesInfo = null;
        }

        public CharFileInfoPreview previewInfo { get; protected set; }

        public CharFileInfoCustom customInfo { get; protected set; }

        public CharFileInfoCoordinate coordinateInfo { get; protected set; }

        public CharFileInfoStatus statusInfo { get; protected set; }

        public CharFileInfoParameter parameterInfo { get; protected set; }

        public CharFileInfoClothes clothesInfo { get; protected set; }

        public void CopyBlockData<T>(T srcInfo) where T : BlockControlBase
        {
            if ((object)srcInfo is CharFileInfoPreview)
                this.previewInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoPreview);
            else if ((object)srcInfo is CharFileInfoCustomMale)
                this.customInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoCustomMale);
            else if ((object)srcInfo is CharFileInfoCustomFemale)
                this.customInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoCustomFemale);
            else if ((object)srcInfo is CharFileInfoCoordinateMale)
                this.coordinateInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoCoordinateMale);
            else if ((object)srcInfo is CharFileInfoCoordinateFemale)
                this.coordinateInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoCoordinateFemale);
            else if ((object)srcInfo is CharFileInfoStatusMale)
                this.statusInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoStatusMale);
            else if ((object)srcInfo is CharFileInfoStatusFemale)
                this.statusInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoStatusFemale);
            else if ((object)srcInfo is CharFileInfoParameterMale)
            {
                this.parameterInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoParameterMale);
            }
            else
            {
                if (!((object)srcInfo is CharFileInfoParameterFemale))
                    return;
                this.parameterInfo = DeepCopyHelper.DeepCopy((object)srcInfo as CharFileInfoParameterFemale);
            }
        }

        public bool SetClothesInfo(CharFileInfoClothes info)
        {
            return this.clothesInfo.Copy(info);
        }

        public bool ChangeCoordinateType(CharDefine.CoordinateType type)
        {
            if (this.coordinateInfo == null)
                return false;
            CharFileInfoClothes info = this.coordinateInfo.GetInfo(type);
            if (info == null || !this.clothesInfo.Copy(info))
                return false;
            this.statusInfo.coordinateType = type;
            return true;
        }

        public bool SetCoordinateInfo(CharDefine.CoordinateType type)
        {
            return this.coordinateInfo != null && this.coordinateInfo.SetInfo(type, this.clothesInfo);
        }

        public static int CheckHoneySelectCharaFile(string path)
        {
            if (!File.Exists(path))
                return -1;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    if (reader.BaseStream.Length == 0L)
                        return -1;
                    long size = 0;
                    PngAssist.CheckPngData(reader, ref size, true);
                    if (reader.BaseStream.Length - reader.BaseStream.Position == 0L)
                        return -1;
                    try
                    {
                        string str = reader.ReadString();
                        int num = reader.ReadInt32();
                        if ("【HoneySelectCharaMale】" == str)
                            return num == 1 ? 0 : 2;
                        if (!("【HoneySelectCharaFemale】" == str))
                            return -1;
                        return num == 1 ? 1 : 3;
                    }
                    catch (EndOfStreamException)
                    {
                    }
                    return -1;
                }
            }
        }

        public static byte[] GetHashPngBytes(byte[] data)
        {
            byte[] numArray = new byte[32];
            int srcOffset = data.Length - 8 - 8 - 32;
            int num = Marshal.SizeOf(data.GetType().GetElementType());
            Buffer.BlockCopy(data, srcOffset, numArray, 0, numArray.Length * num);
            return numArray;
        }

        public static string GetHashPngStr(byte[] data)
        {
            byte[] hashPngBytes = GetHashPngBytes(data);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hashPngBytes)
                stringBuilder.Append(num.ToString("x2"));
            return stringBuilder.ToString();
        }

        public byte[] GetHashParamBytes()
        {
            byte[] numArray1 = this.customInfo.SaveBytes();
            byte[] numArray2 = this.coordinateInfo.SaveBytes();
            byte[] numArray3 = this.parameterInfo.SaveBytes();
            byte[] data = new byte[numArray1.Length + numArray2.Length + numArray3.Length];
            Array.Copy(numArray1, data, numArray1.Length);
            Array.Copy(numArray2, 0, data, numArray1.Length, numArray2.Length);
            Array.Copy(numArray3, 0, data, numArray1.Length + numArray2.Length, numArray3.Length);
            return YS_Assist.CreateSha256(data, this.CharaFileMark);
        }

        public string GetHashParamStr()
        {
            byte[] hashParamBytes = this.GetHashParamBytes();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hashParamBytes)
                stringBuilder.Append(num.ToString("x2"));
            return stringBuilder.ToString();
        }

        public string ConvertCharaFilePath(string path, bool newFile = false)
        {
            string str1 = string.Empty;
            string path1 = string.Empty;
            if (path != string.Empty)
            {
                str1 = Path.GetDirectoryName(path);
                path1 = Path.GetFileName(path);
            }
            string str2 = !(str1 == string.Empty) ? str1 + "/" : UserData.Path + this.CharaFileDirectory;
            if (path1 == string.Empty)
            {
                if (newFile || this.charaFileName == string.Empty)
                {
                    string empty = string.Empty;
                    path1 = !(this.CharaFileMark == "【HoneySelectCharaMale】") ? "charaF" + empty + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") : "charaM" + empty + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                }
                else
                    path1 = this.charaFileName;
            }
            this.charaFileName = Path.GetFileNameWithoutExtension(path1) + ".png";
            return str2 + this.charaFileName;
        }

        public bool Save(string path = "")
        {
            string path1 = this.ConvertCharaFilePath(path, false);
            string directoryName = Path.GetDirectoryName(path1);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fileStream = new FileStream(path1, FileMode.Create, FileAccess.Write))
                return this.Save(fileStream);
        }

        public bool Save(Stream st)
        {
            using (BinaryWriter writer = new BinaryWriter(st))
                return this.Save(writer);
        }

        public bool Save(BinaryWriter writer)
        {
            if (this.charaFilePNG != null)
                writer.Write(this.charaFilePNG);
            return this.SaveWithoutPNG(writer);
        }

        public bool SaveWithoutPNG(BinaryWriter writer)
        {
            long position = writer.BaseStream.Position;
            writer.Write(this.CharaFileMark);
            writer.Write(2);
            byte[] buffer1 = null;
            if (this.previewInfo != null)
            {
                this.previewInfo.Update(this);
                buffer1 = this.previewInfo.SaveBytes();
            }
            byte[] buffer2 = null;
            if (this.customInfo != null)
                buffer2 = this.customInfo.SaveBytes();
            byte[] buffer3 = null;
            if (this.coordinateInfo != null)
                buffer3 = this.coordinateInfo.SaveBytes();
            byte[] buffer4 = null;
            if (this.statusInfo != null)
                buffer4 = this.statusInfo.SaveBytes();
            byte[] buffer5 = null;
            if (this.parameterInfo != null)
                buffer5 = this.parameterInfo.SaveBytes();
            int length = 5;
            writer.Write(length);
            long num1 = 0;
            string[] strArray = new string[5]
            {
                this.previewInfo.tagName,
                this.customInfo.tagName,
                this.coordinateInfo.tagName,
                this.statusInfo.tagName,
                this.parameterInfo.tagName
            };
            int[] numArray1 = new int[5]
            {
                this.previewInfo.version,
                this.customInfo.version,
                this.coordinateInfo.version,
                this.statusInfo.version,
                this.parameterInfo.version
            };
            long[] numArray2 = new long[5]
            {
                buffer1 != null ?  buffer1.GetLength(0) : 0L,
                buffer2 != null ?  buffer2.GetLength(0) : 0L,
                buffer3 != null ?  buffer3.GetLength(0) : 0L,
                buffer4 != null ?  buffer4.GetLength(0) : 0L,
                buffer5 != null ?  buffer5.GetLength(0) : 0L
            };
            long[] numArray3 = new long[5]
            {
                num1,
                num1 + numArray2[0],
                num1 + numArray2[0] + numArray2[1],
                num1 + numArray2[0] + numArray2[1] + numArray2[2],
                num1 + numArray2[0] + numArray2[1] + numArray2[2] + numArray2[3]
            };
            BlockHeader[] blockHeaderArray = new BlockHeader[length];
            for (int index = 0; index < length; ++index)
            {
                blockHeaderArray[index] = new BlockHeader();
                blockHeaderArray[index].SetHeader(strArray[index], numArray1[index], numArray3[index], numArray2[index]);
                blockHeaderArray[index].SaveHeader(writer);
            }
            long num2 = writer.BaseStream.Position - position;
            if (numArray2[0] != 0L)
                writer.Write(buffer1);
            if (numArray2[1] != 0L)
                writer.Write(buffer2);
            if (numArray2[2] != 0L)
                writer.Write(buffer3);
            if (numArray2[3] != 0L)
                writer.Write(buffer4);
            if (numArray2[4] != 0L)
                writer.Write(buffer5);
            byte[] buffer6 = this.charaFilePNG != null ? YS_Assist.CreateSha256(this.charaFilePNG, this.CharaFileMark) : new byte[32];
            writer.Write(buffer6);
            writer.Write(position);
            writer.Write(num2);
            return true;
        }

        public bool Load(string path = "", bool noSetPNG = false, bool noLoadStatus = true)
        {
            string path1 = this.ConvertCharaFilePath(path, false);
            if (!File.Exists(path1))
                return false;
            using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.Read))
                return this.Load(fileStream, noSetPNG, noLoadStatus);
        }

        public bool Load(Stream st, bool noSetPNG = false, bool noLoadStatus = true)
        {
            using (BinaryReader reader = new BinaryReader(st))
                return this.Load(reader, noSetPNG, noLoadStatus);
        }

        public bool Load(BinaryReader reader, bool noSetPNG = false, bool noLoadStatus = true)
        {
            if (reader.BaseStream.Length == 0L)
                return false;
            long size = 0;
            if (noSetPNG)
                PngAssist.CheckPngData(reader, ref size, true);
            else
                this.charaFilePNG = PngAssist.LoadPngData(reader);
            if (reader.BaseStream.Length - reader.BaseStream.Position == 0L)
                return false;
            try
            {
                if (reader.ReadString() != this.CharaFileMark)
                    return false;
                this.charaLoadFileVersion = reader.ReadInt32();
                if (this.charaLoadFileVersion > 2)
                    return false;
                int length = reader.ReadInt32();
                BlockHeader[] blockHeaderArray = new BlockHeader[length];
                for (int index = 0; index < length; ++index)
                {
                    blockHeaderArray[index] = new BlockHeader();
                    blockHeaderArray[index].LoadHeader(reader);
                }
                int index1 = 0;
                long position = reader.BaseStream.Position;
                this.previewInfo.previewLoadVersion = blockHeaderArray[index1].version;
                int index2 = index1 + 1;
                this.customInfo.customLoadVersion = blockHeaderArray[index2].version;
                if (this.customInfo.customLoadVersion <= this.customInfo.version)
                {
                    reader.BaseStream.Seek(position + blockHeaderArray[index2].pos, SeekOrigin.Begin);
                    if (!this.customInfo.LoadBytes(reader.ReadBytes((int)blockHeaderArray[index2].size), blockHeaderArray[index2].version))
                        return false;
                }
                if ("ill_Sitri" != Path.GetFileNameWithoutExtension(this.charaFileName))
                    this.customInfo.isConcierge = false;
                int index3 = index2 + 1;
                this.coordinateInfo.coordinateLoadVersion = blockHeaderArray[index3].version;
                if (this.coordinateInfo.coordinateLoadVersion <= this.coordinateInfo.version)
                {
                    reader.BaseStream.Seek(position + blockHeaderArray[index3].pos, SeekOrigin.Begin);
                    if (!this.coordinateInfo.LoadBytes(reader.ReadBytes((int)blockHeaderArray[index3].size), blockHeaderArray[index3].version))
                        return false;
                }
                int index4 = index3 + 1;
                if (!noLoadStatus)
                {
                    this.statusInfo.statusLoadVersion = blockHeaderArray[index4].version;
                    if (this.statusInfo.statusLoadVersion <= this.statusInfo.version)
                    {
                        reader.BaseStream.Seek(position + blockHeaderArray[index4].pos, SeekOrigin.Begin);
                        if (!this.statusInfo.LoadBytes(reader.ReadBytes((int)blockHeaderArray[index4].size), blockHeaderArray[index4].version))
                            return false;
                    }
                }
                int index5 = index4 + 1;
                this.parameterInfo.parameterLoadVersion = blockHeaderArray[index5].version;
                if (this.parameterInfo.parameterLoadVersion <= this.parameterInfo.version)
                {
                    reader.BaseStream.Seek(position + blockHeaderArray[index5].pos, SeekOrigin.Begin);
                    if (!this.parameterInfo.LoadBytes(reader.ReadBytes((int)blockHeaderArray[index5].size), blockHeaderArray[index5].version))
                        return false;
                }
                long offset = position + blockHeaderArray[index5].pos + blockHeaderArray[index5].size;
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                if (2 <= this.charaLoadFileVersion)
                    reader.BaseStream.Seek(32L, SeekOrigin.Current);
                reader.BaseStream.Seek(8L, SeekOrigin.Current);
                reader.BaseStream.Seek(8L, SeekOrigin.Current);
                this.ChangeCoordinateType(this.statusInfo.coordinateType);
                return true;
            }
            catch (EndOfStreamException)
            {
            }
            return false;
        }

        public bool ChangeSavePng(string path, byte[] newPng)
        {
            if (newPng == null)
                return false;
            string path1 = this.ConvertCharaFilePath(path, false);
            if (!File.Exists(path1))
                return false;
            byte[] buffer = null;
            long size = 0;
            long num1 = 0;
            using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    PngAssist.CheckPngData(reader, ref size, true);
                    if (reader.BaseStream.Length - reader.BaseStream.Position == 0L)
                        return false;
                    try
                    {
                        long num2 = 16;
                        buffer = reader.ReadBytes((int)(reader.BaseStream.Length - size - num2));
                        size = reader.ReadInt64();
                        num1 = reader.ReadInt64();
                    }
                    catch (EndOfStreamException)
                    {
                        return false;
                    }
                }
            }
            if (buffer == null)
                return false;
            long length = newPng.Length;
            using (FileStream fileStream = new FileStream(path1, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(newPng);
                    binaryWriter.Write(buffer);
                    binaryWriter.Write(length);
                    binaryWriter.Write(num1);
                }
            }
            return true;
        }

        public bool LoadBlockData<T>(T blockInfo, string path) where T : BlockControlBase
        {
            if (blockInfo == null)
                return false;
            string path1 = this.ConvertCharaFilePath(path, false);
            if (!File.Exists(path1))
                return false;
            this.blockFileName = Path.GetFileNameWithoutExtension(path1);
            using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.Read))
                return this.LoadBlockData(blockInfo, fileStream);
        }

        public bool LoadBlockData<T>(T blockInfo, Stream st) where T : BlockControlBase
        {
            using (BinaryReader reader = new BinaryReader(st))
                return this.LoadBlockData(blockInfo, reader);
        }

        public bool LoadBlockData<T>(T blockInfo, BinaryReader reader) where T : BlockControlBase
        {
            if (reader.BaseStream.Length == 0L)
                return false;
            long size = 0;
            PngAssist.CheckPngData(reader, ref size, true);
            if (reader.BaseStream.Length - reader.BaseStream.Position == 0L)
                return false;
            try
            {
                if (reader.ReadString() != this.CharaFileMark)
                    return false;
                this.charaLoadFileVersion = reader.ReadInt32();
                if (this.charaLoadFileVersion > 2)
                    return false;
                int length = reader.ReadInt32();
                BlockHeader[] blockHeaderArray = new BlockHeader[length];
                for (int index = 0; index < length; ++index)
                {
                    blockHeaderArray[index] = new BlockHeader();
                    blockHeaderArray[index].LoadHeader(reader);
                }
                long position = reader.BaseStream.Position;
                int index1 = -1;
                for (int index2 = 0; index2 < blockHeaderArray.Length; ++index2)
                {
                    if (blockHeaderArray[index2].tagName.StartsWith(blockInfo.tagName))
                    {
                        index1 = index2;
                        break;
                    }
                }
                if (index1 == -1)
                    return false;
                int version = blockHeaderArray[index1].version;
                if (version > blockInfo.version)
                    return false;
                reader.BaseStream.Seek(position + blockHeaderArray[index1].pos, SeekOrigin.Begin);
                byte[] data = reader.ReadBytes((int)blockHeaderArray[index1].size);
                blockInfo.LoadBytes(data, version);
                if ("ill_Sitri" != this.blockFileName)
                {
                    if ((object)blockInfo is CharFileInfoPreview)
                        ((object)blockInfo as CharFileInfoPreview).isConcierge = 0;
                    else if ((object)blockInfo is CharFileInfoCustomFemale)
                        ((object)blockInfo as CharFileInfoCustomFemale).isConcierge = false;
                }
                return true;
            }
            catch (EndOfStreamException)
            {
            }
            return false;
        }

        public bool SaveBlockData<T>(T blockInfo, string path) where T : BlockControlBase
        {
            if (blockInfo == null || !File.Exists(this.ConvertCharaFilePath(path, false)))
                return false;
            CharFile charFile = !(this is CharMaleFile) ? new CharFemaleFile() : (CharFile)new CharMaleFile();
            if (!charFile.Load(path, false, true))
                return false;
            charFile.CopyBlockData(blockInfo);
            File.Delete(path);
            charFile.Save(path);
            return true;
        }

        public static float ClampEx(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }
    }
}

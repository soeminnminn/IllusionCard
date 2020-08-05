using System;
using System.IO;
using System.Text;

namespace SexyBeachPR
{
    public class CharSave
    {
        private string CharaFileDir = string.Empty;
        private string CharaFileMark = string.Empty;
        public int LoadCharaFileVersion;
        public int LoadPreviewVersion;
        public int LoadCustomVersion;
        public int LoadClothesVersion;

        public byte Sex { get; protected set; }

        public string SaveFileName { get; private set; }

        public byte[] SavePngData { get; private set; }

        public void SetSavePng(byte[] data)
        {
            this.SavePngData = data;
        }

        protected virtual void Init(CharaListInfo info, byte sex, int id, int no)
        {
            this.Sex = sex;
            this.SaveFileName = string.Empty;
            this.SavePngData = null;
            if (this.Sex == 0)
            {
                this.CharaFileDir = "chara/male/";
                this.CharaFileMark = "【PremiumResortCharaMale】";
            }
            else
            {
                this.CharaFileDir = "chara/female/";
                this.CharaFileMark = "【PremiumResortCharaFemale】";
            }
        }

        public string ConvertCharaFilePath(string filepath, bool newFile = false)
        {
            string str1 = string.Empty;
            string path = string.Empty;
            if (filepath != string.Empty)
            {
                str1 = Path.GetDirectoryName(filepath);
                path = Path.GetFileName(filepath);
            }
            string str2 = !(str1 == string.Empty) ? str1 + "/" : UserData.Path + this.CharaFileDir;
            if (path == string.Empty)
                path = newFile || this.SaveFileName == string.Empty ? CommonAssist.GetDateTimeString(DateTime.Now) : this.SaveFileName;
            this.SaveFileName = Path.GetFileNameWithoutExtension(path) + ".png";
            return str2 + this.SaveFileName;
        }

        public bool CreateCharaFileDebug(string filepath = "")
        {
            string path = this.ConvertCharaFilePath(filepath, true);
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    byte[] pngData = null;
                    PngAssist.CreatePng(ref pngData, 0);
                    if (pngData != null)
                        writer.Write(pngData);
                    this.SavePngData = pngData;
                    this.WriteCharaDataWithoutPNG(writer);
                }
            }
            return true;
        }

        public bool OverwriteCharaFile(string filepath = "")
        {
            string path = this.ConvertCharaFilePath(filepath, false);
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                return this.OverwriteCharaFile(fileStream);
        }

        public bool OverwriteCharaFile(Stream st)
        {
            using (BinaryWriter writer = new BinaryWriter(st))
                return this.OverwriteCharaFile(writer);
        }

        public bool OverwriteCharaFile(BinaryWriter writer)
        {
            if (this.SavePngData != null)
                writer.Write(this.SavePngData);
            return this.WriteCharaDataWithoutPNG(writer);
        }

        public bool WriteCharaDataWithoutPNG(BinaryWriter writer)
        {
            CharBody charBody = this as CharBody;
            long position1 = writer.BaseStream.Position;
            writer.Write(this.CharaFileMark);
            writer.Write(1);
            CharCustom charCustomInstance = charBody.GetCharCustomInstance();
            byte[] buffer1 = null;
            if (charCustomInstance != null)
                buffer1 = charCustomInstance.SaveBytes();
            CharClothes charClothesInstance = charBody.GetCharClothesInstance();
            byte[] buffer2 = null;
            if (charClothesInstance != null)
                buffer2 = charClothesInstance.SaveBytes();
            byte[] numArray1 = null;
            CharSave.PreviewInfo previewInfo = new CharSave.PreviewInfo();
            previewInfo.Sex = charBody.Sex;
            previewInfo.Name = charCustomInstance.name;
            if (previewInfo.Sex == 0)
            {
                previewInfo.Personality = byte.MaxValue;
                previewInfo.Height = byte.MaxValue;
                previewInfo.BustSize = byte.MaxValue;
                previewInfo.HairType = byte.MaxValue;
            }
            else
            {
                previewInfo.Personality = charCustomInstance.personality;
                float shapeBodyValue1 = charCustomInstance.GetShapeBodyValue(0);
                previewInfo.Height = shapeBodyValue1 >= 0.330000013113022 ? (shapeBodyValue1 <= 0.660000026226044 ? (byte)1 : (byte)2) : (byte)0;
                float shapeBodyValue2 = charCustomInstance.GetShapeBodyValue(1);
                previewInfo.BustSize = shapeBodyValue2 >= 0.330000013113022 ? (shapeBodyValue2 <= 0.660000026226044 ? (byte)1 : (byte)2) : (byte)0;
                previewInfo.HairType = 0;
            }
            byte[] buffer3 = previewInfo.SaveBytes();
            int length = 4;
            writer.Write(length);
            long num = writer.BaseStream.Position + CharSave.BlockInfo.GetBlockInfoSize() * length;
            string[] strArray = new string[4]
            {
              "プレビュー情報",
              "カスタムデータ",
              "服装データ",
              "顔画像データ"
            };
            int[] numArray3 = new int[4] { 1, 5, 2, 100 };
            long[] numArray4 = new long[4]
            {
              buffer3 != null ?  buffer3.GetLength(0) : 0L,
              buffer1 != null ?  buffer1.GetLength(0) : 0L,
              buffer2 != null ?  buffer2.GetLength(0) : 0L,
              numArray1 != null ?  numArray1.GetLength(0) : 0L
            };
            long[] numArray5 = new long[4]
            {
              num,
              num + numArray4[0],
              num + numArray4[0] + numArray4[1],
              num + numArray4[0] + numArray4[1] + numArray4[2]
            };
            CharSave.BlockInfo[] blockInfoArray = new CharSave.BlockInfo[length];
            for (int index = 0; index < length; ++index)
            {
                blockInfoArray[index] = new CharSave.BlockInfo();
                blockInfoArray[index].SetInfo(strArray[index], numArray3[index], numArray5[index], numArray4[index]);
                blockInfoArray[index].SaveInfo(writer);
            }
            long position2 = writer.BaseStream.Position;
            if (numArray4[0] != 0L)
            {
                writer.Write(buffer3);
            }
            if (numArray4[1] != 0L)
                writer.Write(buffer1);
            if (numArray4[2] != 0L)
                writer.Write(buffer2);
            if (numArray4[3] == 0L)
                ;
            writer.Write(position1);
            writer.Write(position2);
            return true;
        }

        public bool LoadCharaFile(Stream st, bool NoSetPNG = false)
        {
            using (BinaryReader reader = new BinaryReader(st))
                return this.LoadCharaFile(reader, NoSetPNG);
        }

        public bool LoadCharaFile(BinaryReader reader, bool NoSetPNG = false)
        {
            if (reader.BaseStream.Length == 0L)
                return false;
            CharBody charBody = this as CharBody;
            if (NoSetPNG)
            {
                long size = 0;
                PngAssist.CheckPngData(reader, ref size, true);
            }
            else
                this.SavePngData = PngAssist.LoadPngData(reader);
            if (reader.ReadString() != this.CharaFileMark)
                return false;
            this.LoadCharaFileVersion = reader.ReadInt32();
            if (this.LoadCharaFileVersion > 1)
                return false;
            int length = reader.ReadInt32();
            CharSave.BlockInfo[] blockInfoArray = new CharSave.BlockInfo[length];
            for (int index = 0; index < length; ++index)
            {
                blockInfoArray[index] = new CharSave.BlockInfo();
                blockInfoArray[index].LoadInfo(reader);
            }
            int index1 = 0;
            this.LoadPreviewVersion = blockInfoArray[index1].version;
            int index2 = index1 + 1;
            CharCustom charCustomInstance = charBody.GetCharCustomInstance();
            this.LoadCustomVersion = blockInfoArray[index2].version;
            if (blockInfoArray[index2].version <= 5)
            {
                reader.BaseStream.Seek(blockInfoArray[index2].pos, SeekOrigin.Begin);
                byte[] data = reader.ReadBytes((int)blockInfoArray[index2].size);
                if (charCustomInstance == null || !charCustomInstance.LoadBytes(data, blockInfoArray[index2].version))
                    return false;
            }
            if (charBody.Sex != 0)
            {
                PersonalityIdInfo info = new PersonalityIdInfo();
                CharaListInfo.Instance.GetPersonalityInfo(charCustomInstance.personality, info);
                charBody.SetVoiceCorrectValue(info.voiceCorrect);
            }
            int index3 = index2 + 1;
            this.LoadClothesVersion = blockInfoArray[index3].version;
            if (blockInfoArray[index3].version <= 2)
            {
                reader.BaseStream.Seek(blockInfoArray[index3].pos, SeekOrigin.Begin);
                byte[] data = reader.ReadBytes((int)blockInfoArray[index3].size);
                CharClothes charClothesInstance = charBody.GetCharClothesInstance();
                if (charClothesInstance == null || !charClothesInstance.LoadBytes(data, blockInfoArray[index3].version))
                    return false;
            }
            int index4 = index3 + 1;
            long offset = blockInfoArray[index4].pos + blockInfoArray[index4].size;
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            return true;
        }

        public static CharSave.PreviewInfo LoadPreviewInfo(string filepath, byte sex)
        {
            int ver = 0;
            byte[] data = CharSave.LoadCharaBlockData(filepath, 0, sex, ref ver);
            if (data == null)
                return null;
            CharSave.PreviewInfo previewInfo = new CharSave.PreviewInfo();
            previewInfo.LoadBytes(data, ver);
            return previewInfo;
        }

        public static byte[] LoadCharaBlockData(string filepath, int blockId, byte sex, ref int ver)
        {
            if (!File.Exists(filepath))
                return null;
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                return CharSave.LoadCharaBlockData(fileStream, blockId, sex, ref ver);
        }

        public static byte[] LoadCharaBlockData(Stream st, int blockId, byte sex, ref int ver)
        {
            using (BinaryReader reader = new BinaryReader(st))
                return CharSave.LoadCharaBlockData(reader, blockId, sex, ref ver);
        }

        public static byte[] LoadCharaBlockData(BinaryReader reader, int blockId, byte sex, ref int ver)
        {
            if (reader.BaseStream.Length == 0L)
                return null;
            long size = 0;
            PngAssist.CheckPngData(reader, ref size, true);
            if (reader.ReadString() != (sex != 0 ? "【PremiumResortCharaFemale】" : "【PremiumResortCharaMale】"))
                return null;
            if (reader.ReadInt32() > 1)
                return null;
            int length = reader.ReadInt32();
            CharSave.BlockInfo[] blockInfoArray = new CharSave.BlockInfo[length];
            for (int index = 0; index < length; ++index)
            {
                blockInfoArray[index] = new CharSave.BlockInfo();
                blockInfoArray[index].LoadInfo(reader);
            }
            string[] strArray = new string[4]
            {
              "プレビュー情報",
              "カスタムデータ",
              "服装データ",
              "顔画像データ"
            };
            int[] numArray = new int[4] { 1, 5, 2, 100 };
            int index1 = -1;
            for (int index2 = 0; index2 < blockInfoArray.Length; ++index2)
            {
                if (blockInfoArray[index2].tagName.StartsWith(strArray[blockId]))
                {
                    index1 = index2;
                    break;
                }
            }
            if (index1 == -1)
                return null;
            if (blockInfoArray[index1].version > numArray[blockId])
                return null;
            ver = blockInfoArray[index1].version;
            reader.BaseStream.Seek(blockInfoArray[index1].pos, SeekOrigin.Begin);
            return reader.ReadBytes((int)blockInfoArray[index1].size);
        }

        public class BlockInfo
        {
            public string tagName = string.Empty;
            private const int tagSize = 128;
            public byte[] tag;
            public int version;
            public long pos;
            public long size;

            public void SetInfo(string _tagName, int _version, long _pos, long _size)
            {
                this.tagName = _tagName;
                this.tag = CharSave.BlockInfo.ChangeStringToByte(this.tagName);
                this.version = _version;
                this.pos = _pos;
                this.size = _size;
            }

            public bool SaveInfo(BinaryWriter writer)
            {
                if (this.tag == null)
                    return false;
                writer.Write(this.tag);
                writer.Write(this.version);
                writer.Write(this.pos);
                writer.Write(this.size);
                return true;
            }

            public bool LoadInfo(BinaryReader reader)
            {
                this.tag = reader.ReadBytes(tagSize);
                this.tagName = CharSave.BlockInfo.ChangeByteToString(this.tag);
                this.version = reader.ReadInt32();
                this.pos = reader.ReadInt64();
                this.size = reader.ReadInt64();
                return true;
            }

            public static int GetBlockInfoSize()
            {
                return 148;
            }

            public static byte[] ChangeStringToByte(string _tagName)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(_tagName);
                if (bytes.GetLength(0) > tagSize)
                    return null;
                byte[] numArray = new byte[tagSize];
                Buffer.BlockCopy(bytes, 0, numArray, 0, bytes.GetLength(0));
                return numArray;
            }

            public static string ChangeByteToString(byte[] _tag)
            {
                return Encoding.UTF8.GetString(_tag);
            }
        }

        public class PreviewInfo
        {
            public string Name = string.Empty;
            public int NameLen;
            public byte Sex;
            public int Personality;
            public byte Height;
            public byte BustSize;
            public int HairType;

            public byte[] SaveBytes()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                    {
                        byte num = 0;
                        binaryWriter.Write(num);
                        this.NameLen = Encoding.UTF8.GetByteCount(this.Name);
                        binaryWriter.Write(this.NameLen);
                        binaryWriter.Write(this.Name);
                        binaryWriter.Write(this.Sex);
                        binaryWriter.Write(this.Personality);
                        binaryWriter.Write(this.Height);
                        binaryWriter.Write(this.BustSize);
                        binaryWriter.Write(this.HairType);
                        return memoryStream.ToArray();
                    }
                }
            }

            public bool LoadBytes(byte[] data, int version)
            {
                using (MemoryStream memoryStream = new MemoryStream(data))
                {
                    using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                    {
                        binaryReader.BaseStream.Seek(1L, SeekOrigin.Current);
                        if (version >= 1)
                            this.NameLen = binaryReader.ReadInt32();
                        this.Name = binaryReader.ReadString();
                        this.Sex = binaryReader.ReadByte();
                        this.Personality = binaryReader.ReadInt32();
                        if (this.Sex != 0 && !CharaListInfo.Instance.IsPersonality(this.Personality))
                            this.Personality = 14;
                        this.Height = binaryReader.ReadByte();
                        this.BustSize = binaryReader.ReadByte();
                        this.HairType = binaryReader.ReadInt32();
                    }
                }
                return true;
            }
        }
    }
}

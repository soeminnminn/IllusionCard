using System;
using System.IO;
using MessagePack;

namespace CharacterKK
{
    public class ChaFileCoordinate
    {
        public static readonly string BlockName = "Coordinate";
        public Version loadVersion = new Version(ChaFileDefine.ChaFileCoordinateVersion.ToString());
        public string coordinateName = string.Empty;
        public int loadProductNo;
        public ChaFileClothes clothes;
        public ChaFileAccessory accessory;
        public bool enableMakeup;
        public ChaFileMakeup makeup;
        public byte[] pngData;
        private int lastLoadErrorCode;

        public ChaFileCoordinate()
        {
            this.MemberInit();
        }

        public string coordinateFileName { get; private set; }

        public int GetLastErrorCode()
        {
            return this.lastLoadErrorCode;
        }

        public void MemberInit()
        {
            this.clothes = new ChaFileClothes();
            this.accessory = new ChaFileAccessory();
            this.makeup = new ChaFileMakeup();
            this.coordinateFileName = string.Empty;
            this.coordinateName = string.Empty;
            this.pngData = null;
            this.lastLoadErrorCode = 0;
        }

        public byte[] SaveBytes()
        {
            byte[] buffer1 = MessagePackSerializer.Serialize(this.clothes);
            byte[] buffer2 = MessagePackSerializer.Serialize(this.accessory);
            byte[] buffer3 = MessagePackSerializer.Serialize(this.makeup);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(buffer1.Length);
                    binaryWriter.Write(buffer1);
                    binaryWriter.Write(buffer2.Length);
                    binaryWriter.Write(buffer2);
                    binaryWriter.Write(this.enableMakeup);
                    binaryWriter.Write(buffer3.Length);
                    binaryWriter.Write(buffer3);
                    return memoryStream.ToArray();
                }
            }
        }

        public bool LoadBytes(byte[] data, Version ver)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    try
                    {
                        int count1 = binaryReader.ReadInt32();
                        this.clothes = MessagePackSerializer.Deserialize<ChaFileClothes>(binaryReader.ReadBytes(count1));
                        int count2 = binaryReader.ReadInt32();
                        this.accessory = MessagePackSerializer.Deserialize<ChaFileAccessory>(binaryReader.ReadBytes(count2));
                        this.enableMakeup = binaryReader.ReadBoolean();
                        int count3 = binaryReader.ReadInt32();
                        this.makeup = MessagePackSerializer.Deserialize<ChaFileMakeup>(binaryReader.ReadBytes(count3));
                    }
                    catch (EndOfStreamException)
                    {
                        return false;
                    }
                    this.clothes.ComplementWithVersion();
                    this.accessory.ComplementWithVersion();
                    this.makeup.ComplementWithVersion();
                    return true;
                }
            }
        }

        public void SaveFile(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            this.coordinateFileName = Path.GetFileName(path);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    if (this.pngData != null)
                        binaryWriter.Write(this.pngData);
                    binaryWriter.Write(100);
                    binaryWriter.Write("【KoiKatuClothes】");
                    binaryWriter.Write(ChaFileDefine.ChaFileCoordinateVersion.ToString());
                    binaryWriter.Write(this.coordinateName);
                    byte[] buffer = this.SaveBytes();
                    binaryWriter.Write(buffer.Length);
                    binaryWriter.Write(buffer);
                }
            }
        }

        public bool LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                this.lastLoadErrorCode = -6;
                return false;
            }
            this.coordinateFileName = Path.GetFileName(path);
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return this.LoadFile(fileStream);
        }

        public bool LoadFile(Stream st)
        {
            using (BinaryReader br = new BinaryReader(st))
            {
                try
                {
                    PngAssist.SkipPng(br);
                    if (br.BaseStream.Length - br.BaseStream.Position == 0L)
                    {
                        this.lastLoadErrorCode = -5;
                        return false;
                    }
                    this.loadProductNo = br.ReadInt32();
                    if (this.loadProductNo > 100)
                    {
                        this.lastLoadErrorCode = -3;
                        return false;
                    }
                    if (br.ReadString() != "【KoiKatuClothes】")
                    {
                        this.lastLoadErrorCode = -1;
                        return false;
                    }
                    this.loadVersion = new Version(br.ReadString());
                    if (0 > ChaFileDefine.ChaFileClothesVersion.CompareTo(this.loadVersion))
                    {
                        this.lastLoadErrorCode = -2;
                        return false;
                    }
                    this.coordinateName = br.ReadString();
                    int count = br.ReadInt32();
                    if (this.LoadBytes(br.ReadBytes(count), this.loadVersion))
                    {
                        this.lastLoadErrorCode = 0;
                        return true;
                    }
                    this.lastLoadErrorCode = -999;
                    return false;
                }
                catch (EndOfStreamException)
                {
                    this.lastLoadErrorCode = -999;
                    return false;
                }
            }
        }
    }
}

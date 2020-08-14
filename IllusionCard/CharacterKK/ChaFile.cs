using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;

namespace CharacterKK
{
    public class ChaFile
    {
        public Version loadVersion = new Version(ChaFileDefine.ChaFileVersion.ToString());
        public int loadProductNo;
        public byte[] pngData;
        public byte[] facePngData;
        public ChaFileCustom custom;
        public ChaFileParameter parameter;
        public ChaFileStatus status;
        private int lastLoadErrorCode;

        public ChaFile()
        {
            this.custom = new ChaFileCustom();
            this.coordinate = new ChaFileCoordinate[Enum.GetNames(typeof(ChaFileDefine.CoordinateType)).Length];
            for (int index = 0; index < this.coordinate.Length; ++index)
                this.coordinate[index] = new ChaFileCoordinate();
            this.parameter = new ChaFileParameter();
            this.status = new ChaFileStatus();
            this.lastLoadErrorCode = 0;
        }

        public string charaFileName { get; protected set; }

        public ChaFileCoordinate[] coordinate { get; set; }

        public int GetLastErrorCode()
        {
            return this.lastLoadErrorCode;
        }

        protected bool SaveFile(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            this.charaFileName = Path.GetFileName(path);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                return this.SaveFile(fileStream, true);
        }

        protected bool SaveFile(Stream st, bool savePng)
        {
            using (BinaryWriter bw = new BinaryWriter(st))
                return this.SaveFile(bw, savePng);
        }

        protected bool SaveFile(BinaryWriter bw, bool savePng)
        {
            if (savePng && this.pngData != null)
                bw.Write(this.pngData);
            bw.Write(100);
            bw.Write("【KoiKatuChara】");
            bw.Write(ChaFileDefine.ChaFileVersion.ToString());
            int num1 = 0;
            if (this.facePngData != null)
                num1 = this.facePngData.Length;
            bw.Write(num1);
            if (num1 != 0)
                bw.Write(this.facePngData);
            byte[] customBytes = this.GetCustomBytes();
            byte[] coordinateBytes = this.GetCoordinateBytes();
            byte[] parameterBytes = this.GetParameterBytes();
            byte[] statusBytes = this.GetStatusBytes();
            int length = 4;
            long num2 = 0;
            string[] strArray1 = new string[4]
            {
              ChaFileCustom.BlockName,
              ChaFileCoordinate.BlockName,
              ChaFileParameter.BlockName,
              ChaFileStatus.BlockName
            };
            string[] strArray2 = new string[4]
            {
              ChaFileDefine.ChaFileCustomVersion.ToString(),
              ChaFileDefine.ChaFileCoordinateVersion.ToString(),
              ChaFileDefine.ChaFileParameterVersion.ToString(),
              ChaFileDefine.ChaFileStatusVersion.ToString()
            };
            long[] numArray1 = new long[length];
            numArray1[0] = customBytes != null ? customBytes.Length : 0L;
            numArray1[1] = coordinateBytes != null ? coordinateBytes.Length : 0L;
            numArray1[2] = parameterBytes != null ? parameterBytes.Length : 0L;
            numArray1[3] = statusBytes != null ? statusBytes.Length : 0L;
            long[] numArray2 = new long[4]
            {
              num2,
              num2 + numArray1[0],
              num2 + numArray1[0] + numArray1[1],
              num2 + numArray1[0] + numArray1[1] + numArray1[2]
            };
            BlockHeader blockHeader = new BlockHeader();
            for (int index = 0; index < length; ++index)
            {
                BlockHeader.Info info = new BlockHeader.Info()
                {
                    name = strArray1[index],
                    version = strArray2[index],
                    size = numArray1[index],
                    pos = numArray2[index]
                };
                blockHeader.lstInfo.Add(info);
            }
            byte[] buffer = MessagePackSerializer.Serialize(blockHeader);
            bw.Write(buffer.Length);
            bw.Write(buffer);
            long num3 = 0;
            foreach (long num4 in numArray1)
                num3 += num4;
            bw.Write(num3);
            bw.Write(customBytes);
            bw.Write(coordinateBytes);
            bw.Write(parameterBytes);
            bw.Write(statusBytes);
            return true;
        }

        protected bool LoadFile(string path, bool noLoadPNG = false, bool noLoadStatus = true)
        {
            if (!File.Exists(path))
            {
                this.lastLoadErrorCode = -6;
                return false;
            }
            this.charaFileName = Path.GetFileName(path);
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return this.LoadFile(fileStream, noLoadPNG, noLoadStatus);
        }

        protected bool LoadFile(Stream st, bool noLoadPNG = false, bool noLoadStatus = true)
        {
            using (BinaryReader br = new BinaryReader(st))
                return this.LoadFile(br, noLoadPNG, noLoadStatus);
        }

        protected bool LoadFile(BinaryReader br, bool noLoadPNG = false, bool noLoadStatus = true)
        {
            long pngSize = PngAssist.CheckSize(br);
            if (pngSize != 0L)
            {
                if (noLoadPNG)
                    br.BaseStream.Seek(pngSize, SeekOrigin.Current);
                else
                    this.pngData = br.ReadBytes((int)pngSize);
                if (br.BaseStream.Length - br.BaseStream.Position == 0L)
                {
                    this.lastLoadErrorCode = -5;
                    return false;
                }
            }
            try
            {
                this.loadProductNo = br.ReadInt32();
                if (this.loadProductNo > 100)
                {
                    this.lastLoadErrorCode = -3;
                    return false;
                }
                if (br.ReadString() != "【KoiKatuChara】")
                {
                    this.lastLoadErrorCode = -1;
                    return false;
                }
                this.loadVersion = new Version(br.ReadString());
                if (0 > ChaFileDefine.ChaFileVersion.CompareTo(this.loadVersion))
                {
                    this.lastLoadErrorCode = -2;
                    return false;
                }
                int count1 = br.ReadInt32();
                if (count1 != 0)
                    this.facePngData = br.ReadBytes(count1);
                int count2 = br.ReadInt32();
                BlockHeader blockHeader = MessagePackSerializer.Deserialize<BlockHeader>(br.ReadBytes(count2));
                long num = br.ReadInt64();
                long position = br.BaseStream.Position;
                BlockHeader.Info info1 = blockHeader.SearchInfo(ChaFileCustom.BlockName);
                if (info1 != null)
                {
                    Version ver = new Version(info1.version);
                    if (0 > ChaFileDefine.ChaFileCustomVersion.CompareTo(ver))
                    {
                        this.lastLoadErrorCode = -2;
                    }
                    else
                    {
                        br.BaseStream.Seek(position + info1.pos, SeekOrigin.Begin);
                        this.SetCustomBytes(br.ReadBytes((int)info1.size), ver);
                    }
                }
                BlockHeader.Info info2 = blockHeader.SearchInfo(ChaFileCoordinate.BlockName);
                if (info2 != null)
                {
                    Version ver = new Version(info2.version);
                    if (0 > ChaFileDefine.ChaFileCoordinateVersion.CompareTo(ver))
                    {
                        this.lastLoadErrorCode = -2;
                    }
                    else
                    {
                        br.BaseStream.Seek(position + info2.pos, SeekOrigin.Begin);
                        this.SetCoordinateBytes(br.ReadBytes((int)info2.size), ver);
                    }
                }
                BlockHeader.Info info3 = blockHeader.SearchInfo(ChaFileParameter.BlockName);
                if (info3 != null)
                {
                    Version version = new Version(info3.version);
                    if (0 > ChaFileDefine.ChaFileParameterVersion.CompareTo(version))
                    {
                        this.lastLoadErrorCode = -2;
                    }
                    else
                    {
                        br.BaseStream.Seek(position + info3.pos, SeekOrigin.Begin);
                        this.SetParameterBytes(br.ReadBytes((int)info3.size));
                    }
                }
                if (!noLoadStatus)
                {
                    BlockHeader.Info info4 = blockHeader.SearchInfo(ChaFileStatus.BlockName);
                    if (info4 != null)
                    {
                        Version version = new Version(info4.version);
                        if (0 > ChaFileDefine.ChaFileStatusVersion.CompareTo(version))
                        {
                            this.lastLoadErrorCode = -2;
                        }
                        else
                        {
                            br.BaseStream.Seek(position + info4.pos, SeekOrigin.Begin);
                            this.SetStatusBytes(br.ReadBytes((int)info4.size));
                        }
                    }
                }
                br.BaseStream.Seek(position + num, SeekOrigin.Begin);
            }
            catch (EndOfStreamException)
            {
                this.lastLoadErrorCode = -999;
                return false;
            }
            this.lastLoadErrorCode = 0;
            return true;
        }

        public bool AssignCoordinate(ChaFileDefine.CoordinateType type, ChaFileCoordinate srcCoorde)
        {
            if (srcCoorde == null)
                return false;
            byte[] data = srcCoorde.SaveBytes();
            this.coordinate[(int)type].LoadBytes(data, srcCoorde.loadVersion);
            return true;
        }

        public byte[] GetCustomBytes()
        {
            return GetCustomBytes(this.custom);
        }

        public static byte[] GetCustomBytes(ChaFileCustom _custom)
        {
            return _custom.SaveBytes();
        }

        public byte[] GetCoordinateBytes()
        {
            return GetCoordinateBytes(this.coordinate);
        }

        public static byte[] GetCoordinateBytes(ChaFileCoordinate[] _coordinate)
        {
            if (_coordinate.Length == 0)
                return null;
            List<byte[]> numArrayList = new List<byte[]>();
            foreach (ChaFileCoordinate chaFileCoordinate in _coordinate)
                numArrayList.Add(chaFileCoordinate.SaveBytes());
            return MessagePackSerializer.Serialize(numArrayList);
        }

        public byte[] GetParameterBytes()
        {
            return GetParameterBytes(this.parameter);
        }

        public static byte[] GetParameterBytes(ChaFileParameter _parameter)
        {
            return MessagePackSerializer.Serialize(_parameter);
        }

        public byte[] GetStatusBytes()
        {
            return GetStatusBytes(this.status);
        }

        public static byte[] GetStatusBytes(ChaFileStatus _status)
        {
            return MessagePackSerializer.Serialize(_status);
        }

        public void SetCustomBytes(byte[] data, Version ver)
        {
            this.custom.LoadBytes(data, ver);
        }

        public void SetCoordinateBytes(byte[] data, Version ver)
        {
            List<byte[]> numArrayList = MessagePackSerializer.Deserialize<List<byte[]>>(data);
            for (int index = 0; index < this.coordinate.Length && numArrayList.Count > index; ++index)
                this.coordinate[index].LoadBytes(numArrayList[index], ver);
        }

        public void SetParameterBytes(byte[] data)
        {
            ChaFileParameter src = MessagePackSerializer.Deserialize<ChaFileParameter>(data);
            src.ComplementWithVersion();
            this.parameter.Copy(src);
        }

        public void SetStatusBytes(byte[] data)
        {
            ChaFileStatus src = MessagePackSerializer.Deserialize<ChaFileStatus>(data);
            src.ComplementWithVersion();
            this.status.Copy(src);
        }

        public static void CopyChaFile(ChaFile dst, ChaFile src)
        {
            dst.CopyAll(src);
        }

        public void CopyAll(ChaFile _chafile)
        {
            this.CopyCustom(_chafile.custom);
            this.CopyCoordinate(_chafile.coordinate);
            this.CopyParameter(_chafile.parameter);
            this.CopyStatus(_chafile.status);
        }

        public void CopyCustom(ChaFileCustom _custom)
        {
            this.SetCustomBytes(GetCustomBytes(_custom), ChaFileDefine.ChaFileCustomVersion);
        }

        public void CopyCoordinate(ChaFileCoordinate[] _coordinate)
        {
            this.SetCoordinateBytes(GetCoordinateBytes(_coordinate), ChaFileDefine.ChaFileCoordinateVersion);
        }

        public void CopyParameter(ChaFileParameter _parameter)
        {
            this.SetParameterBytes(GetParameterBytes(_parameter));
        }

        public void CopyStatus(ChaFileStatus _status)
        {
            this.SetStatusBytes(GetStatusBytes(_status));
        }
    }
}

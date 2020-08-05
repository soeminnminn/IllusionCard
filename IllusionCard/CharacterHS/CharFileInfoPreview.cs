// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoPreview
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using System.Text;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoPreview : BlockControlBase
    {
        public int productNo = 11;
        public string name = string.Empty;
        public int previewLoadVersion;
        public int sex;
        public int personality;
        public int height;
        public int bustSize;
        public int hairType;
        public int state;
        public int resistH;
        public int resistPain;
        public int resistAnal;
        public int isConcierge;

        public CharFileInfoPreview()
          : base("プレビュー情報", 4)
        {
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(11);
                    binaryWriter.Write(this.sex);
                    binaryWriter.Write(this.personality);
                    int byteCount = Encoding.UTF8.GetByteCount(this.name);
                    binaryWriter.Write(byteCount);
                    binaryWriter.Write(this.name);
                    binaryWriter.Write(this.height);
                    binaryWriter.Write(this.bustSize);
                    binaryWriter.Write(this.hairType);
                    binaryWriter.Write(this.state);
                    binaryWriter.Write(this.resistH);
                    binaryWriter.Write(this.resistPain);
                    binaryWriter.Write(this.resistAnal);
                    binaryWriter.Write(this.isConcierge);
                    return memoryStream.ToArray();
                }
            }
        }

        public override bool LoadBytes(byte[] data, int previewVer)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    if (4 <= previewVer)
                        this.productNo = binaryReader.ReadInt32();
                    this.sex = binaryReader.ReadInt32();
                    if (2 <= previewVer)
                    {
                        this.personality = binaryReader.ReadInt32();
                        this.personality = 0;
                        binaryReader.ReadInt32();
                        this.name = binaryReader.ReadString();
                        this.height = binaryReader.ReadInt32();
                        this.bustSize = binaryReader.ReadInt32();
                        this.hairType = binaryReader.ReadInt32();
                    }
                    if (4 <= previewVer)
                    {
                        this.state = binaryReader.ReadInt32();
                        this.resistH = binaryReader.ReadInt32();
                        this.resistPain = binaryReader.ReadInt32();
                        this.resistAnal = binaryReader.ReadInt32();
                    }
                    if (3 <= previewVer)
                        this.isConcierge = binaryReader.ReadInt32();
                    return true;
                }
            }
        }

        public void Update(CharFile chaFile)
        {
            this.sex = chaFile.customInfo.sex;
            this.name = chaFile.customInfo.name;
            this.isConcierge = !chaFile.customInfo.isConcierge ? 0 : 1;
            if (this.sex == 0)
            {
                this.personality = byte.MaxValue;
                this.height = byte.MaxValue;
                this.bustSize = byte.MaxValue;
                this.hairType = byte.MaxValue;
                this.state = byte.MaxValue;
                this.resistH = byte.MaxValue;
                this.resistPain = byte.MaxValue;
                this.resistAnal = byte.MaxValue;
            }
            else
            {
                this.personality = chaFile.customInfo.personality;
                float heightMark = chaFile.customInfo.shapeValueBody[0];
                this.height = heightMark >= 0.330000013113022 ? (heightMark <= 0.660000026226044 ? 1 : 2) : 0;
                float bustSizeMark = chaFile.customInfo.shapeValueBody[1];
                this.bustSize = bustSizeMark >= 0.330000013113022 ? (bustSizeMark <= 0.660000026226044 ? 1 : 2) : 0;
                this.hairType = chaFile.customInfo.hairType;
                CharFileInfoParameterFemale parameterInfo = chaFile.parameterInfo as CharFileInfoParameterFemale;
                this.state = (int)parameterInfo.nowState;
                this.resistH = parameterInfo.resistH != 100 ? 0 : 1;
                this.resistPain = parameterInfo.resistPain != 100 ? 0 : 1;
                this.resistAnal = parameterInfo.resistAnal != 100 ? 0 : 1;
            }
        }
    }
}

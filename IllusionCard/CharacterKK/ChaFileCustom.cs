using System;
using System.IO;
using MessagePack;

namespace CharacterKK
{
    public class ChaFileCustom
    {
        public static readonly string BlockName = "Custom";
        public ChaFileFace face;
        public ChaFileBody body;
        public ChaFileHair hair;

        public ChaFileCustom()
        {
            this.MemberInit();
        }

        public void MemberInit()
        {
            this.face = new ChaFileFace();
            this.body = new ChaFileBody();
            this.hair = new ChaFileHair();
        }

        public byte[] SaveBytes()
        {
            byte[] buffer1 = MessagePackSerializer.Serialize(this.face);
            byte[] buffer2 = MessagePackSerializer.Serialize(this.body);
            byte[] buffer3 = MessagePackSerializer.Serialize(this.hair);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(buffer1.Length);
                    binaryWriter.Write(buffer1);
                    binaryWriter.Write(buffer2.Length);
                    binaryWriter.Write(buffer2);
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
                    int count1 = binaryReader.ReadInt32();
                    this.face = MessagePackSerializer.Deserialize<ChaFileFace>(binaryReader.ReadBytes(count1));
                    int count2 = binaryReader.ReadInt32();
                    this.body = MessagePackSerializer.Deserialize<ChaFileBody>(binaryReader.ReadBytes(count2));
                    int count3 = binaryReader.ReadInt32();
                    this.hair = MessagePackSerializer.Deserialize<ChaFileHair>(binaryReader.ReadBytes(count3));
                    this.face.ComplementWithVersion();
                    this.body.ComplementWithVersion();
                    this.hair.ComplementWithVersion();
                    return true;
                }
            }
        }
    }
}

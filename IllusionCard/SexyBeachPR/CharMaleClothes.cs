using System;
using System.IO;

namespace SexyBeachPR
{
    [Serializable]
    public class CharMaleClothes : CharClothes
    {
        public Coordinate[] regCoord = new Coordinate[2];
        public Coordinate nowCoord = new Coordinate();
        public byte stateClothes;
        public byte stateShoes;

        public CharMaleClothes(CharBody cha)
          : base(cha)
        {
            for (int index = 0; index < this.regCoord.Length; ++index)
                this.regCoord[index] = new Coordinate();
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(this.setType);
                    for (int index = 0; index < this.regCoord.Length; ++index)
                    {
                        writer.Write(this.regCoord[index].clothesId);
                        writer.Write(this.regCoord[index].shoesId);
                        this.regCoord[index].clothesColor.Save(writer);
                        this.regCoord[index].shoesColor.Save(writer);
                    }
                    writer.Write(this.alwaysSwimsuit);
                    for (int index1 = 0; index1 < 2; ++index1)
                    {
                        for (int index2 = 0; index2 < 5; ++index2)
                        {
                            this.regAccessory[index1, index2].Save(writer);
                            this.regAcsColor[index1, index2].Save(writer);
                        }
                    }
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
                    this.setType = reader.ReadByte();
                    for (int index = 0; index < this.regCoord.Length; ++index)
                    {
                        this.regCoord[index].clothesId = reader.ReadInt32();
                        this.regCoord[index].shoesId = reader.ReadInt32();
                        if (version >= 2)
                        {
                            this.regCoord[index].clothesColor.Load(reader, version);
                            this.regCoord[index].shoesColor.Load(reader, version);
                        }
                    }
                    if (version >= 2)
                        this.alwaysSwimsuit = reader.ReadBoolean();
                    for (int index1 = 0; index1 < 2; ++index1)
                    {
                        for (int index2 = 0; index2 < 5; ++index2)
                        {
                            this.regAccessory[index1, index2].Load(reader);
                            if (version >= 2)
                                this.regAcsColor[index1, index2].Load(reader, version);
                        }
                    }
                }
            }
            return true;
        }

        public class Coordinate
        {
            public ColorSet clothesColor = new ColorSet();
            public ColorSet shoesColor = new ColorSet();
            public int clothesId;
            public int shoesId;
        }
    }
}

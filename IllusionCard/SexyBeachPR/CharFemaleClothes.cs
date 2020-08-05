using System;
using System.IO;

namespace SexyBeachPR
{
    [Serializable]
    public class CharFemaleClothes : CharClothes
    {
        public Coordinate[] regCoord = new Coordinate[2];
        public Coordinate nowCoord = new Coordinate();
        public byte stateTypeClothesTop = 2;
        public byte stateTypeClothesBot = 2;
        public byte stateTypeBra = 2;
        public byte stateTypeShorts = 2;
        public byte stateTypeGloves = 2;
        public byte stateTypePanst = 2;
        public byte stateTypeSocks = 2;
        public byte stateTypeShoes = 2;
        public byte stateTypeSwimsuit = 2;
        public byte stateTypeSwimClothesTop = 2;
        public byte stateTypeSwimClothesBot = 2;
        public bool[] VisibleBra = new bool[7];
        public bool[] VisibleShorts = new bool[7];
        public bool[] SwimBlendshapeAnim = new bool[6];
        public bool NotBra;
        public bool NotShorts;
        public bool NotBot;
        public bool AlwaysNip;
        public bool SwimsuitNoSeparate;
        public byte stateClothesTop;
        public byte stateClothesBot;
        public byte stateBra;
        public byte stateShorts;
        public byte stateGloves;
        public byte statePanst;
        public byte stateSocks;
        public byte stateShoes;
        public byte stateSwimsuitTop;
        public byte stateSwimsuitBot;
        public byte stateSwimClothesTop;
        public byte stateSwimClothesBot;
        public byte stateSwimOptTop;
        public byte stateSwimOptBot;

        public CharFemaleClothes(CharBody cha)
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
                        writer.Write(this.regCoord[index].clothesTopId);
                        writer.Write(this.regCoord[index].clothesBotId);
                        writer.Write(this.regCoord[index].braId);
                        writer.Write(this.regCoord[index].shortsId);
                        writer.Write(this.regCoord[index].glovesId);
                        writer.Write(this.regCoord[index].panstId);
                        writer.Write(this.regCoord[index].socksId);
                        writer.Write(this.regCoord[index].shoesId);
                        writer.Write(this.regCoord[index].swimsuitId);
                        writer.Write(this.regCoord[index].swimTopId);
                        writer.Write(this.regCoord[index].swimBotId);
                        this.regCoord[index].clothesTopColor.Save(writer);
                        this.regCoord[index].clothesBotColor.Save(writer);
                        this.regCoord[index].braColor.Save(writer);
                        this.regCoord[index].shortsColor.Save(writer);
                        this.regCoord[index].glovesColor.Save(writer);
                        this.regCoord[index].panstColor.Save(writer);
                        this.regCoord[index].socksColor.Save(writer);
                        this.regCoord[index].shoesColor.Save(writer);
                        this.regCoord[index].swimsuitColor.Save(writer);
                        this.regCoord[index].swimTopColor.Save(writer);
                        this.regCoord[index].swimBotColor.Save(writer);
                    }
                    writer.Write(this.stateSwimOptTop);
                    writer.Write(this.stateSwimOptBot);
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
                        this.regCoord[index].clothesTopId = reader.ReadInt32();
                        this.regCoord[index].clothesBotId = reader.ReadInt32();
                        this.regCoord[index].braId = reader.ReadInt32();
                        this.regCoord[index].shortsId = reader.ReadInt32();
                        this.regCoord[index].glovesId = reader.ReadInt32();
                        this.regCoord[index].panstId = reader.ReadInt32();
                        this.regCoord[index].socksId = reader.ReadInt32();
                        this.regCoord[index].shoesId = reader.ReadInt32();
                        this.regCoord[index].swimsuitId = reader.ReadInt32();
                        this.regCoord[index].swimTopId = reader.ReadInt32();
                        this.regCoord[index].swimBotId = reader.ReadInt32();
                        if (version >= 2)
                        {
                            this.regCoord[index].clothesTopColor.Load(reader, version);
                            this.regCoord[index].clothesBotColor.Load(reader, version);
                            this.regCoord[index].braColor.Load(reader, version);
                            this.regCoord[index].shortsColor.Load(reader, version);
                            this.regCoord[index].glovesColor.Load(reader, version);
                            this.regCoord[index].panstColor.Load(reader, version);
                            this.regCoord[index].socksColor.Load(reader, version);
                            this.regCoord[index].shoesColor.Load(reader, version);
                            this.regCoord[index].swimsuitColor.Load(reader, version);
                            this.regCoord[index].swimTopColor.Load(reader, version);
                            this.regCoord[index].swimBotColor.Load(reader, version);
                        }
                    }
                    if (version >= 1)
                    {
                        this.stateSwimOptTop = reader.ReadByte();
                        this.stateSwimOptBot = reader.ReadByte();
                        this.alwaysSwimsuit = reader.ReadBoolean();
                    }
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
            public ColorSet clothesTopColor = new ColorSet();
            public ColorSet clothesBotColor = new ColorSet();
            public ColorSet braColor = new ColorSet();
            public ColorSet shortsColor = new ColorSet();
            public ColorSet glovesColor = new ColorSet();
            public ColorSet panstColor = new ColorSet();
            public ColorSet socksColor = new ColorSet();
            public ColorSet shoesColor = new ColorSet();
            public ColorSet swimsuitColor = new ColorSet();
            public ColorSet swimTopColor = new ColorSet();
            public ColorSet swimBotColor = new ColorSet();
            public int clothesTopId;
            public int clothesBotId;
            public int braId;
            public int shortsId;
            public int glovesId;
            public int panstId;
            public int socksId;
            public int shoesId;
            public int swimsuitId;
            public int swimTopId;
            public int swimBotId;
        }
    }
}

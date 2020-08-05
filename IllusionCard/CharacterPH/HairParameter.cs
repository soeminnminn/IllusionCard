using System;
using System.IO;

namespace CharacterPH
{
    public class HairParameter : ParameterBase
    {
        public HairPartParameter[] parts;

        public HairParameter(SEX sex)
          : base(sex)
        {
            this.parts = new HairPartParameter[3];
            for (int index = 0; index < this.parts.Length; ++index)
                this.parts[index] = new HairPartParameter(sex);
        }

        public HairParameter(HairParameter copy)
          : base(copy.sex)
        {
            this.parts = new HairPartParameter[3];
            this.Copy(copy);
        }

        public void Init()
        {
            for (int index = 0; index < this.parts.Length; ++index)
                this.parts[index].Init();
        }

        public void Copy(HairParameter copy)
        {
            if (this.parts == null)
                this.parts = new HairPartParameter[3];
            for (int index = 0; index < this.parts.Length; ++index)
            {
                if (this.parts[index] == null)
                    this.parts[index] = new HairPartParameter(copy.parts[index]);
                else
                    this.parts[index].Copy(copy.parts[index]);
            }
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            writer.Write(this.parts.Length);
            for (int index = 0; index < this.parts.Length; ++index)
                this.parts[index].Save(writer, sex);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            this.parts = new HairPartParameter[reader.ReadInt32()];
            for (int index = 0; index < this.parts.Length; ++index)
            {
                this.parts[index] = new HairPartParameter(sex);
                this.parts[index].Load(reader, sex, version);
            }
        }
    }
}

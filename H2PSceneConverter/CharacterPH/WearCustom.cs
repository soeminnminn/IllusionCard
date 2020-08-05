using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class WearCustom : ParameterBase
    {
        public WEAR_TYPE type = WEAR_TYPE.NUM;
        public int id = -1;
        public ColorParameter_PBR2 color;

        public WearCustom(SEX sex, WEAR_TYPE type, int id)
          : base(sex)
        {
            this.type = type;
            this.id = id;
            this.color = new ColorParameter_PBR2();
        }

        public WearCustom(SEX sex, WEAR_TYPE type, int id, Color color)
          : base(sex)
        {
            this.type = type;
            this.id = id;
            this.color = new ColorParameter_PBR2();
        }

        public WearCustom(WearCustom copy)
          : base(copy.sex)
        {
            this.Copy(copy);
        }

        public void Init()
        {
            this.id = -1;
            this.color = new ColorParameter_PBR2();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write((int)this.type);
            this.Write(writer, this.id);
            if (this.color != null)
                this.color.Save(writer);
            else
                writer.Write(0);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            this.type = (WEAR_TYPE)reader.ReadInt32();
            this.Read(reader, ref this.id);
            this.color.Load(reader, version);
        }

        public void Copy(WearCustom source)
        {
            this.type = source.type;
            this.id = source.id;
            if (source.color != null)
            {
                if (this.color == null)
                    this.color = new ColorParameter_PBR2();
                this.color.Copy(source.color);
            }
            else
                this.color = null;
        }
    }
}

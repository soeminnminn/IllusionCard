using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class HairPartParameter : ParameterBase
    {
        public int ID;
        public ColorParameter_Hair hairColor;
        public ColorParameter_PBR1 acceColor;

        public HairPartParameter(SEX sex)
          : base(sex)
        {
            this.Init();
        }

        public HairPartParameter(HairPartParameter copy)
          : base(copy.sex)
        {
            this.Copy(copy);
        }

        public void Init()
        {
            this.ID = 0;
            this.hairColor = new ColorParameter_Hair();
            this.acceColor = null;
        }

        public void Copy(HairPartParameter copy)
        {
            this.ID = copy.ID;
            this.hairColor = copy.hairColor == null ? null : new ColorParameter_Hair(copy.hairColor);
            if (copy.acceColor != null)
                this.acceColor = new ColorParameter_PBR1(copy.acceColor);
            else
                this.acceColor = null;
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            this.Write(writer, this.ID);
            if (this.hairColor != null)
                this.hairColor.Save(writer);
            else
                writer.Write(0);
            if (this.acceColor != null)
                this.acceColor.Save(writer);
            else
                writer.Write(0);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            this.Read(reader, ref this.ID);
            if (version <= CUSTOM_DATA_VERSION.DEBUG_03)
            {
                Color white = Color.white;
                this.Read(reader, ref white);
                if (version <= CUSTOM_DATA_VERSION.DEBUG_00)
                    return;
                this.Read(reader, ref white);
            }
            else
            {
                this.hairColor.Load(reader, version);
                if (this.acceColor == null)
                    this.acceColor = new ColorParameter_PBR1();
                if (this.acceColor.Load(reader, version))
                    return;
                this.acceColor = null;
            }
        }
    }
}

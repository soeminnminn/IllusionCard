using System;
using System.IO;

namespace CharacterPH
{
    public class WearParameter : ParameterBase
    {
        public WearCustom[] wears = new WearCustom[11];
        public bool swimOptTop = true;
        public bool swimOptBtm = true;
        public bool isSwimwear;

        public WearParameter(SEX sex)
          : base(sex)
        {
            for (int index = 0; index < this.wears.Length; ++index)
                this.wears[index] = new WearCustom(sex, (WEAR_TYPE)index, 0);
        }

        public WearParameter(WearParameter copy)
          : base(copy.sex)
        {
            for (int index = 0; index < copy.wears.Length; ++index)
                this.wears[index] = new WearCustom(copy.wears[index]);
            this.isSwimwear = copy.isSwimwear;
            this.swimOptTop = copy.swimOptTop;
            this.swimOptBtm = copy.swimOptBtm;
        }

        public void Init()
        {
            for (int index = 0; index < this.wears.Length; ++index)
                this.wears[index].Init();
            this.isSwimwear = false;
            this.swimOptTop = true;
            this.swimOptBtm = true;
        }

        public int GetWearID(WEAR_TYPE type)
        {
            return this.wears[(int)type] != null ? this.wears[(int)type].id : -1;
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            for (int index = 0; index < this.wears.Length; ++index)
                this.wears[index].Save(writer);
            if (sex != SEX.FEMALE)
                return;
            this.Write(writer, this.isSwimwear);
            this.Write(writer, this.swimOptTop);
            this.Write(writer, this.swimOptBtm);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            for (int index = 0; index < this.wears.Length; ++index)
                this.wears[index].Load(reader, sex, version);
            if (sex != SEX.FEMALE)
                return;
            this.Read(reader, ref this.isSwimwear);
            this.Read(reader, ref this.swimOptTop);
            this.Read(reader, ref this.swimOptBtm);
        }

        public void Copy(WearParameter source)
        {
            this.isSwimwear = source.isSwimwear;
            this.swimOptTop = source.swimOptTop;
            this.swimOptBtm = source.swimOptBtm;
            for (int index = 0; index < this.wears.Length; ++index)
                this.wears[index].Copy(source.wears[index]);
        }
    }
}

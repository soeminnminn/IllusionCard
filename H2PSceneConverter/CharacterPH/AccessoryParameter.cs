using System;
using System.IO;

namespace CharacterPH
{
    public class AccessoryParameter : ParameterBase
    {
        public AccessoryCustom[] slot = new AccessoryCustom[10];

        public AccessoryParameter(SEX sex)
          : base(sex)
        {
            for (int index = 0; index < this.slot.Length; ++index)
                this.slot[index] = new AccessoryCustom(sex);
        }

        public AccessoryParameter(AccessoryParameter copy)
          : base(copy.sex)
        {
            for (int index = 0; index < copy.slot.Length; ++index)
                this.slot[index] = new AccessoryCustom(copy.slot[index]);
        }

        public void Init()
        {
            for (int index = 0; index < this.slot.Length; ++index)
                this.slot[index].Init();
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            for (int index = 0; index < this.slot.Length; ++index)
                this.slot[index].Save(writer, sex);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            for (int index = 0; index < this.slot.Length; ++index)
                this.slot[index].Load(reader, sex, version);
        }

        public void Copy(AccessoryParameter source)
        {
            this.sex = source.sex;
            for (int index = 0; index < this.slot.Length; ++index)
                this.slot[index].Copy(source.slot[index]);
        }
    }
}

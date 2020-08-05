using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    [Serializable]
    public class AccessoryCustom : ParameterBase
    {
        public ACCESSORY_TYPE type = ACCESSORY_TYPE.NONE;
        public int id = -1;
        public ACCESSORY_ATTACH nowAttach = ACCESSORY_ATTACH.NONE;
        public Vector3 addPos = Vector3.zero;
        public Vector3 addRot = Vector3.zero;
        public Vector3 addScl = Vector3.one;
        public const int SLOT_NUM = 10;
        public ColorParameter_PBR2 color;

        public AccessoryCustom(SEX sex)
          : base(sex)
        {
            this.Init();
        }

        public AccessoryCustom(AccessoryCustom copy)
          : base(copy.sex)
        {
            this.Copy(copy);
        }

        public void Init()
        {
            this.type = ACCESSORY_TYPE.NONE;
            this.id = -1;
            this.nowAttach = ACCESSORY_ATTACH.NONE;
            this.addPos = Vector3.zero;
            this.addRot = Vector3.zero;
            this.addScl = Vector3.one;
            this.color = null;
        }

        public void Copy(AccessoryCustom src)
        {
            this.type = src.type;
            this.id = src.id;
            this.nowAttach = src.nowAttach;
            this.addPos = src.addPos;
            this.addRot = src.addRot;
            this.addScl = src.addScl;
            if (src.color != null)
                this.color = new ColorParameter_PBR2(src.color);
            else
                this.color = null;
        }

        public void Set(ACCESSORY_TYPE type, int id, string key)
        {
            this.type = type;
            this.id = id;
            this.nowAttach = AccessoryData.CheckAttach(key);
            this.color = new ColorParameter_PBR2();
            if (this.nowAttach != ACCESSORY_ATTACH.NONE || type == ACCESSORY_TYPE.NONE)
                return;
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            this.Write(writer, this.type);
            this.Write(writer, this.id);
            this.Write(writer, this.nowAttach);
            this.Write(writer, this.addPos);
            this.Write(writer, this.addRot);
            this.Write(writer, this.addScl);
            if (this.color != null)
                this.color.Save(writer);
            else
                writer.Write(0);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            this.Read(reader, ref this.type);
            this.Read(reader, ref this.id);
            this.Read(reader, ref this.nowAttach);
            this.Read(reader, ref this.addPos);
            this.Read(reader, ref this.addRot);
            this.Read(reader, ref this.addScl);
            this.color = new ColorParameter_PBR2();
            this.color.Load(reader, version);
        }

        protected void Write(BinaryWriter writer, ACCESSORY_TYPE val)
        {
            writer.Write((int)val);
        }

        protected void Read(BinaryReader reader, ref ACCESSORY_TYPE val)
        {
            val = (ACCESSORY_TYPE)reader.ReadInt32();
        }

        protected void Write(BinaryWriter writer, ACCESSORY_ATTACH val)
        {
            writer.Write((int)val);
        }

        protected void Read(BinaryReader reader, ref ACCESSORY_ATTACH val)
        {
            val = (ACCESSORY_ATTACH)reader.ReadInt32();
        }
    }
}

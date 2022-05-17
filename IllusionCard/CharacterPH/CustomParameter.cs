using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class CustomParameter : ParameterBase
    {
        public HairParameter hair;
        public HeadParameter head;
        public BodyParameter body;
        public WearParameter wear;
        public AccessoryParameter acce;

        public CustomParameter(SEX sex)
          : base(sex)
        {
            this.sex = sex;
            this.hair = new HairParameter(sex);
            this.head = new HeadParameter(sex);
            this.body = new BodyParameter(sex);
            this.wear = new WearParameter(sex);
            this.acce = new AccessoryParameter(sex);
        }

        public CustomParameter(CustomParameter copy)
          : base(copy.sex)
        {
            this.hair = new HairParameter(copy.hair);
            this.head = new HeadParameter(copy.head);
            this.body = new BodyParameter(copy.body);
            this.wear = new WearParameter(copy.wear);
            this.acce = new AccessoryParameter(copy.acce);
        }

        public void Copy(CustomParameter copy, int filter = -1)
        {
            if (copy == null)
                return;
            
            if (this.sex != copy.sex)
                Debug.LogWarning("違う性別のデータをコピーした");
            
            this.sex = copy.sex;
            
            if ((filter & 1) != 0)
                this.hair.Copy(copy.hair);
            
            if ((filter & 2) != 0)
                this.head.Copy(copy.head);
            
            if ((filter & 4) != 0)
                this.body.Copy(copy.body);
            
            if ((filter & 8) != 0)
            {
                this.wear.Copy(copy.wear);
            }
            
            if ((filter & 16) == 0)
                return;
            this.acce.Copy(copy.acce);
        }

        private void Init()
        {
            this.hair.Init();
            this.head.Init();
            this.body.Init();
            this.wear.Init();
            this.acce.Init();
        }

        public void Save(BinaryWriter writer)
        {
            this.Write(writer, CUSTOM_DATA_VERSION.DEBUG_10);
            this.Write(writer, this.sex);
            this.hair.Save(writer, this.sex);
            this.head.Save(writer, this.sex);
            this.body.Save(writer, this.sex);
            this.wear.Save(writer, this.sex);
            this.acce.Save(writer, this.sex);
        }

        public void Load(string file, bool female = true, bool male = true)
        {
            if (!File.Exists(file))
                return;
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    if (this.Load(reader, female, male))
                        return;
                    Debug.LogError("読み込みに失敗しました");
                }
            }
        }

        public bool Load(BinaryReader reader, bool female = true, bool male = true)
        {
            bool flag = true;
            try
            {
                long offset = PngAssist.CheckSize(reader);
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                string str = reader.ReadString();
                if (str == "【PlayHome_Female】")
                {
                    if (female)
                        this.Load(reader);
                    else
                        Debug.LogWarning("異性データ");
                }
                else if (str == "【PlayHome_Male】")
                {
                    if (male)
                        this.Load(reader);
                    else
                        Debug.LogWarning("異性データ");
                }
                else
                    Debug.LogWarning("読めないセーブデータ:" + str);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                flag = false;
            }
            return flag;
        }

        public void Load(BinaryReader reader)
        {
            this.Init();
            CUSTOM_DATA_VERSION version = CUSTOM_DATA_VERSION.UNKNOWN;
            this.Read(reader, ref version);
            this.Read(reader, ref this.sex);
            this.hair.Load(reader, this.sex, version);
            this.head.Load(reader, this.sex, version);
            this.body.Load(reader, this.sex, version);
            this.wear.Load(reader, this.sex, version);
            this.acce.Load(reader, this.sex, version);
        }

        public void SaveCoordinate(BinaryWriter writer)
        {
            this.Write(writer, CUSTOM_DATA_VERSION.DEBUG_10);
            this.Write(writer, this.sex);
            this.wear.Save(writer, this.sex);
            this.acce.Save(writer, this.sex);
        }

        public void LoadCoordinate(BinaryReader reader)
        {
            this.Init();
            CUSTOM_DATA_VERSION version = CUSTOM_DATA_VERSION.UNKNOWN;
            this.Read(reader, ref version);
            this.Read(reader, ref this.sex);
            this.wear.Load(reader, this.sex, version);
            this.acce.Load(reader, this.sex, version);
        }

        protected void Write(BinaryWriter writer, CUSTOM_DATA_VERSION version)
        {
            writer.Write((int)version);
        }

        protected void Read(BinaryReader reader, ref CUSTOM_DATA_VERSION version)
        {
            int num = reader.ReadInt32();
            if (num > 10)
                version = CUSTOM_DATA_VERSION.UNKNOWN;
            else
                version = (CUSTOM_DATA_VERSION)num;
        }

        protected void Write(BinaryWriter writer, SEX sex)
        {
            writer.Write((int)sex);
        }

        protected void Read(BinaryReader reader, ref SEX sex)
        {
            sex = (SEX)reader.ReadInt32();
        }

        public bool CheckWrongParam()
        {
            if (this.sex != SEX.FEMALE && this.sex != SEX.MALE || this.acce.slot.Length != 10)
                return true;
            for (int index = 0; index < this.head.shapeVals.Length; ++index)
            {
                if (this.head.shapeVals[index] < 0.0 || this.head.shapeVals[index] > 1.0)
                    return true;
            }
            for (int index = 0; index < this.body.shapeVals.Length; ++index)
            {
                if (this.body.shapeVals[index] < 0.0 || this.body.shapeVals[index] > 1.0)
                    return true;
            }
            return false;
        }
    }
}

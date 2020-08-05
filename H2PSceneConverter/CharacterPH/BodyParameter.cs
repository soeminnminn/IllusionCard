using System;
using System.IO;
using UnityEngine;
using CharacterHS;

namespace CharacterPH
{
    public class BodyParameter : ParameterBase
    {
        public ColorParameter_AlloyHSVOffset skinColor = new ColorParameter_AlloyHSVOffset();
        public ColorParameter_Alloy underhairColor = new ColorParameter_Alloy();
        public Color tattooColor = Color.white;
        public ColorParameter_AlloyHSVOffset nipColor = new ColorParameter_AlloyHSVOffset(true, 1f);
        public float sunburnColor_S = 1f;
        public float sunburnColor_V = 1f;
        public float sunburnColor_A = 1f;
        public ColorParameter_AlloyHSVOffset nailColor = new ColorParameter_AlloyHSVOffset();
        public ColorParameter_PBR1 manicureColor = new ColorParameter_PBR1();
        public float areolaSize = 0.7f;
        public float bustSoftness = 0.5f;
        public float bustWeight = 0.5f;
        public int bodyID;
        public int detailID;
        public float detailWeight;
        public int underhairID;
        public int tattooID;
        public float[] shapeVals;
        public int nipID;
        public int sunburnID;
        public float sunburnColor_H;

        public BodyParameter(SEX sex)
          : base(sex)
        {
            this.shapeVals = new float[sex != SEX.FEMALE ? CharDefine.cf_bodyshapename.Length : CharDefine.cf_bodyshapename.Length];
            for (int index = 0; index < this.shapeVals.Length; ++index)
                this.shapeVals[index] = 0.5f;
            if (sex != SEX.FEMALE)
                return;
            this.shapeVals[this.shapeVals.Length - 1] = 0.0f;
        }

        public BodyParameter(BodyParameter copy)
          : base(copy.sex)
        {
            this.Copy(copy);
        }

        public void Init()
        {
            this.bodyID = 0;
            this.skinColor = new ColorParameter_AlloyHSVOffset();
            this.detailID = 0;
            this.detailWeight = 0.0f;
            this.underhairID = 0;
            this.underhairColor = new ColorParameter_Alloy();
            this.tattooID = 0;
            this.tattooColor = Color.white;
            this.nailColor = new ColorParameter_AlloyHSVOffset();
            this.manicureColor = new ColorParameter_PBR1();
            this.manicureColor.mainColor1.a = 0.0f;
            this.areolaSize = 0.5f;
            this.bustSoftness = 0.5f;
            this.bustWeight = 0.5f;
            if (this.shapeVals != null)
            {
                for (int index = 0; index < this.shapeVals.Length; ++index)
                    this.shapeVals[index] = 0.5f;
            }
            this.nipID = 0;
            this.nipColor = new ColorParameter_AlloyHSVOffset(true, 1f);
            this.sunburnID = 0;
            this.sunburnColor_H = 0.0f;
            this.sunburnColor_S = 1f;
            this.sunburnColor_V = 1f;
            this.sunburnColor_A = 1f;
        }

        public void Copy(BodyParameter copy)
        {
            if (this.shapeVals == null || this.shapeVals.Length != copy.shapeVals.Length)
                this.shapeVals = new float[copy.shapeVals.Length];
            Array.Copy(copy.shapeVals, shapeVals, this.shapeVals.Length);
            this.bodyID = copy.bodyID;
            this.skinColor = new ColorParameter_AlloyHSVOffset(copy.skinColor);
            this.detailID = copy.detailID;
            this.detailWeight = copy.detailWeight;
            this.underhairID = copy.underhairID;
            this.underhairColor = new ColorParameter_Alloy(copy.underhairColor);
            this.tattooID = copy.tattooID;
            this.tattooColor = copy.tattooColor;
            this.nipID = copy.nipID;
            this.nipColor = new ColorParameter_AlloyHSVOffset(copy.nipColor);
            this.sunburnID = copy.sunburnID;
            this.sunburnColor_H = copy.sunburnColor_H;
            this.sunburnColor_S = copy.sunburnColor_S;
            this.sunburnColor_V = copy.sunburnColor_V;
            this.sunburnColor_A = copy.sunburnColor_A;
            this.nailColor = new ColorParameter_AlloyHSVOffset(copy.nailColor);
            this.manicureColor = new ColorParameter_PBR1(copy.manicureColor);
            this.areolaSize = copy.areolaSize;
            this.bustSoftness = copy.bustSoftness;
            this.bustWeight = copy.bustWeight;
        }

        public float GetHeight()
        {
            return this.sex == SEX.FEMALE ? this.shapeVals[0] : 1f;
        }

        public float GetBustSize()
        {
            return this.sex == SEX.FEMALE ? this.shapeVals[1] : 0.0f;
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            this.Write(writer, this.bodyID);
            this.skinColor.Save(writer);
            this.Write(writer, this.detailID);
            this.Write(writer, this.detailWeight);
            this.Write(writer, this.underhairID);
            this.underhairColor.Save(writer);
            this.Write(writer, this.tattooID);
            this.Write(writer, this.tattooColor);
            this.Write(writer, this.shapeVals);
            if (sex != SEX.FEMALE)
                return;
            this.Write(writer, this.nipID);
            this.nipColor.Save(writer);
            this.Write(writer, this.sunburnID);
            this.Write(writer, this.sunburnColor_H);
            this.Write(writer, this.sunburnColor_S);
            this.Write(writer, this.sunburnColor_V);
            this.Write(writer, this.sunburnColor_A);
            this.nailColor.Save(writer);
            this.manicureColor.Save(writer);
            this.Write(writer, this.areolaSize);
            this.Write(writer, this.bustSoftness);
            this.Write(writer, this.bustWeight);
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            this.Read(reader, ref this.bodyID);
            this.skinColor.Load(reader, version);
            this.Read(reader, ref this.detailID);
            this.Read(reader, ref this.detailWeight);
            this.Read(reader, ref this.underhairID);
            this.underhairColor.Load(reader, version);
            this.Read(reader, ref this.tattooID);
            this.Read(reader, ref this.tattooColor);
            this.Read(reader, ref this.shapeVals);
            if (sex != SEX.FEMALE)
                return;
            this.Read(reader, ref this.nipID);
            this.nipColor.Load(reader, version);
            this.Read(reader, ref this.sunburnID);
            if (version < CUSTOM_DATA_VERSION.DEBUG_06)
            {
                Color white = Color.white;
                this.Read(reader, ref white);
                this.sunburnColor_H = 0.0f;
                this.sunburnColor_S = 1f;
                this.sunburnColor_V = 1f;
                this.sunburnColor_A = 1f;
            }
            else
            {
                this.Read(reader, ref this.sunburnColor_H);
                this.Read(reader, ref this.sunburnColor_S);
                this.Read(reader, ref this.sunburnColor_V);
                this.Read(reader, ref this.sunburnColor_A);
            }
            if (version < CUSTOM_DATA_VERSION.DEBUG_03)
                return;
            this.nailColor.Load(reader, version);
            if (version >= CUSTOM_DATA_VERSION.DEBUG_09)
            {
                this.manicureColor.Load(reader, version);
            }
            else
            {
                this.manicureColor.mainColor1 = Color.white;
                this.manicureColor.mainColor1.a = 0.0f;
                this.manicureColor.specColor1 = Color.white;
                this.manicureColor.specular1 = 0.0f;
                this.manicureColor.smooth1 = 0.0f;
            }
            this.Read(reader, ref this.areolaSize);
            this.Read(reader, ref this.bustSoftness);
            this.Read(reader, ref this.bustWeight);
        }
    }
}

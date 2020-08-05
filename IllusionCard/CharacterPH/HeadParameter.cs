using System;
using System.IO;
using UnityEngine;
using CharacterHS;

namespace CharacterPH
{
    public class HeadParameter : ParameterBase
    {
        public ColorParameter_PBR1 eyeBrowColor = new ColorParameter_PBR1();
        public Color eyeScleraColorL = Color.white;
        public Color eyeIrisColorL = Color.black;
        public float eyeEmissiveL = 0.5f;
        public Color eyeScleraColorR = Color.white;
        public Color eyeIrisColorR = Color.black;
        public float eyeEmissiveR = 0.5f;
        public Color tattooColor = Color.white;
        public ColorParameter_PBR1 eyeLashColor = new ColorParameter_PBR1();
        public Color eyeshadowColor = Color.black;
        public Color cheekColor = Color.white;
        public Color lipColor = Color.white;
        public Color moleColor = Color.white;
        public ColorParameter_EyeHighlight eyeHighlightColor = new ColorParameter_EyeHighlight();
        public Color beardColor = Color.black;
        public int headID;
        public int faceTexID;
        public int detailID;
        public float detailWeight;
        public int eyeBrowID;
        public int eyeID_L;
        public float eyePupilDilationL;
        public int eyeID_R;
        public float eyePupilDilationR;
        public int tattooID;
        public float[] shapeVals;
        public int eyeLashID;
        public int eyeshadowTexID;
        public int cheekTexID;
        public int lipTexID;
        public int moleTexID;
        public int eyeHighlightTexID;
        public int beardID;

        public HeadParameter(SEX sex)
          : base(sex)
        {
            this.shapeVals = new float[sex != SEX.FEMALE ? CharDefine.cm_headshapename.Length : CharDefine.cf_headshapename.Length];
            for (int index = 0; index < this.shapeVals.Length; ++index)
                this.shapeVals[index] = 0.5f;
        }

        public HeadParameter(HeadParameter copy)
          : base(copy.sex)
        {
            this.Copy(copy);
        }

        public void Init()
        {
            this.headID = 0;
            this.faceTexID = 0;
            this.detailID = 0;
            this.detailWeight = 0.0f;
            this.eyeBrowID = 0;
            this.eyeBrowColor = new ColorParameter_PBR1();
            this.eyeScleraColorL = Color.white;
            this.eyeID_L = 0;
            this.eyeIrisColorL = Color.black;
            this.eyeScleraColorR = Color.white;
            this.eyeID_R = 0;
            this.eyeIrisColorR = Color.black;
            this.tattooID = 0;
            this.tattooColor = Color.white;
            if (this.shapeVals != null)
            {
                for (int index = 0; index < this.shapeVals.Length; ++index)
                    this.shapeVals[index] = 0.5f;
            }
            this.eyeLashID = 0;
            this.eyeLashColor = new ColorParameter_PBR1();
            this.eyeshadowTexID = 0;
            this.eyeshadowColor = Color.black;
            this.cheekTexID = 0;
            this.cheekColor = Color.white;
            this.lipTexID = 0;
            this.lipColor = Color.white;
            this.moleTexID = 0;
            this.moleColor = Color.white;
            this.eyeHighlightTexID = 0;
            this.eyeHighlightColor = new ColorParameter_EyeHighlight();
            this.beardID = 0;
            this.beardColor = Color.black;
        }

        public void Copy(HeadParameter copy)
        {
            if (this.shapeVals == null || this.shapeVals.Length != copy.shapeVals.Length)
                this.shapeVals = new float[copy.shapeVals.Length];
            Array.Copy(copy.shapeVals, shapeVals, this.shapeVals.Length);
            this.headID = copy.headID;
            this.faceTexID = copy.faceTexID;
            this.detailID = copy.detailID;
            this.detailWeight = copy.detailWeight;
            this.eyeBrowID = copy.eyeBrowID;
            this.eyeBrowColor = copy.eyeBrowColor;
            this.eyeID_L = copy.eyeID_L;
            this.eyeScleraColorL = copy.eyeScleraColorL;
            this.eyeIrisColorL = copy.eyeIrisColorL;
            this.eyePupilDilationL = copy.eyePupilDilationL;
            this.eyeEmissiveL = copy.eyeEmissiveL;
            this.eyeID_R = copy.eyeID_R;
            this.eyeScleraColorR = copy.eyeScleraColorR;
            this.eyeIrisColorR = copy.eyeIrisColorR;
            this.eyePupilDilationR = copy.eyePupilDilationR;
            this.eyeEmissiveR = copy.eyeEmissiveR;
            this.tattooID = copy.tattooID;
            this.tattooColor = copy.tattooColor;
            this.eyeLashID = copy.eyeLashID;
            this.eyeLashColor = copy.eyeLashColor;
            this.eyeshadowTexID = copy.eyeshadowTexID;
            this.eyeshadowColor = copy.eyeshadowColor;
            this.cheekTexID = copy.cheekTexID;
            this.cheekColor = copy.cheekColor;
            this.lipTexID = copy.lipTexID;
            this.lipColor = copy.lipColor;
            this.moleTexID = copy.moleTexID;
            this.moleColor = copy.moleColor;
            this.eyeHighlightTexID = copy.eyeHighlightTexID;
            this.eyeHighlightColor = copy.eyeHighlightColor;
            this.beardID = copy.beardID;
            this.beardColor = copy.beardColor;
        }

        public void Save(BinaryWriter writer, SEX sex)
        {
            this.Write(writer, this.headID);
            this.Write(writer, this.faceTexID);
            this.Write(writer, this.detailID);
            this.Write(writer, this.detailWeight);
            this.Write(writer, this.eyeBrowID);
            this.eyeBrowColor.Save(writer);
            this.Write(writer, this.eyeID_L);
            this.Write(writer, this.eyeScleraColorL);
            this.Write(writer, this.eyeIrisColorL);
            this.Write(writer, this.eyePupilDilationL);
            this.Write(writer, this.eyeEmissiveL);
            this.Write(writer, this.eyeID_R);
            this.Write(writer, this.eyeScleraColorR);
            this.Write(writer, this.eyeIrisColorR);
            this.Write(writer, this.eyePupilDilationR);
            this.Write(writer, this.eyeEmissiveR);
            this.Write(writer, this.tattooID);
            this.Write(writer, this.tattooColor);
            this.Write(writer, this.shapeVals);
            if (sex == SEX.FEMALE)
            {
                this.Write(writer, this.eyeLashID);
                this.eyeLashColor.Save(writer);
                this.Write(writer, this.eyeshadowTexID);
                this.Write(writer, this.eyeshadowColor);
                this.Write(writer, this.cheekTexID);
                this.Write(writer, this.cheekColor);
                this.Write(writer, this.lipTexID);
                this.Write(writer, this.lipColor);
                this.Write(writer, this.moleTexID);
                this.Write(writer, this.moleColor);
                this.Write(writer, this.eyeHighlightTexID);
                this.eyeHighlightColor.Save(writer);
            }
            else
            {
                if (sex != SEX.MALE)
                    return;
                this.Write(writer, this.beardID);
                this.Write(writer, this.beardColor);
                this.eyeHighlightColor.Save(writer);
            }
        }

        public void Load(BinaryReader reader, SEX sex, CUSTOM_DATA_VERSION version)
        {
            this.Read(reader, ref this.headID);
            this.Read(reader, ref this.faceTexID);
            this.Read(reader, ref this.detailID);
            this.Read(reader, ref this.detailWeight);
            this.Read(reader, ref this.eyeBrowID);
            this.eyeBrowColor.Load(reader, version);
            this.eyePupilDilationL = 0.0f;
            this.eyePupilDilationR = 0.0f;
            this.eyeEmissiveL = 0.5f;
            this.eyeEmissiveR = 0.5f;
            if (version < CUSTOM_DATA_VERSION.DEBUG_04)
            {
                this.Read(reader, ref this.eyeScleraColorL);
                this.eyeScleraColorR = this.eyeScleraColorL;
                this.Read(reader, ref this.eyeID_L);
                this.Read(reader, ref this.eyeIrisColorL);
                this.Read(reader, ref this.eyeID_R);
                this.Read(reader, ref this.eyeIrisColorR);
            }
            else
            {
                this.Read(reader, ref this.eyeID_L);
                this.Read(reader, ref this.eyeScleraColorL);
                this.Read(reader, ref this.eyeIrisColorL);
                this.Read(reader, ref this.eyePupilDilationL);
                if (version >= CUSTOM_DATA_VERSION.DEBUG_10)
                    this.Read(reader, ref this.eyeEmissiveL);
                this.Read(reader, ref this.eyeID_R);
                this.Read(reader, ref this.eyeScleraColorR);
                this.Read(reader, ref this.eyeIrisColorR);
                this.Read(reader, ref this.eyePupilDilationR);
                if (version >= CUSTOM_DATA_VERSION.DEBUG_10)
                    this.Read(reader, ref this.eyeEmissiveR);
            }
            this.Read(reader, ref this.tattooID);
            this.Read(reader, ref this.tattooColor);
            this.Read(reader, ref this.shapeVals);
            switch (sex)
            {
                case SEX.FEMALE:
                    this.Read(reader, ref this.eyeLashID);
                    this.eyeLashColor.Load(reader, version);
                    this.Read(reader, ref this.eyeshadowTexID);
                    this.Read(reader, ref this.eyeshadowColor);
                    this.Read(reader, ref this.cheekTexID);
                    this.Read(reader, ref this.cheekColor);
                    this.Read(reader, ref this.lipTexID);
                    this.Read(reader, ref this.lipColor);
                    this.Read(reader, ref this.moleTexID);
                    this.Read(reader, ref this.moleColor);
                    this.Read(reader, ref this.eyeHighlightTexID);
                    this.eyeHighlightColor.Load(reader, version);
                    break;
                case SEX.MALE:
                    this.Read(reader, ref this.beardID);
                    this.Read(reader, ref this.beardColor);
                    if (version < CUSTOM_DATA_VERSION.DEBUG_02)
                        break;
                    this.eyeHighlightColor.Load(reader, version);
                    break;
            }
        }

        public bool CheckEyeEqual()
        {
            return this.eyeID_L == this.eyeID_R && this.eyeScleraColorL == this.eyeScleraColorR && this.eyeIrisColorL == this.eyeIrisColorR && eyePupilDilationL == (double)this.eyePupilDilationR;
        }
    }
}

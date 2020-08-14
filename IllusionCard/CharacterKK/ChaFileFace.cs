using System;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileFace
    {
        public ChaFileFace()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public float[] shapeValueFace { get; set; }

        public int headId { get; set; }

        public int skinId { get; set; }

        public int detailId { get; set; }

        public float detailPower { get; set; }

        public float cheekGlossPower { get; set; }

        public int eyebrowId { get; set; }

        public Color eyebrowColor { get; set; }

        public int noseId { get; set; }

        public PupilInfo[] pupil { get; set; }

        public int hlUpId { get; set; }

        public Color hlUpColor { get; set; }

        public int hlDownId { get; set; }

        public Color hlDownColor { get; set; }

        public int whiteId { get; set; }

        public Color whiteBaseColor { get; set; }

        public Color whiteSubColor { get; set; }

        public float pupilWidth { get; set; }

        public float pupilHeight { get; set; }

        public float pupilX { get; set; }

        public float pupilY { get; set; }

        public float hlUpY { get; set; }

        public float hlDownY { get; set; }

        public int eyelineUpId { get; set; }

        public float eyelineUpWeight { get; set; }

        public int eyelineDownId { get; set; }

        public Color eyelineColor { get; set; }

        public int moleId { get; set; }

        public Color moleColor { get; set; }

        public Vector4 moleLayout { get; set; }

        public int lipLineId { get; set; }

        public Color lipLineColor { get; set; }

        public float lipGlossPower { get; set; }

        public bool doubleTooth { get; set; }

        public ChaFileMakeup baseMakeup { get; set; }

        public byte foregroundEyes { get; set; }

        public byte foregroundEyebrow { get; set; }

        [IgnoreMember]
        public bool isPupilSameSetting
        {
            get
            {
                return this.pupil[0].id == this.pupil[1].id && !(this.pupil[0].baseColor != this.pupil[1].baseColor) && (!(this.pupil[0].subColor != this.pupil[1].subColor) && this.pupil[0].gradMaskId == this.pupil[1].gradMaskId) && (pupil[0].gradBlend == (double)this.pupil[1].gradBlend && pupil[0].gradOffsetY == (double)this.pupil[1].gradOffsetY && pupil[0].gradScale == (double)this.pupil[1].gradScale);
            }
        }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileFaceVersion;
            this.shapeValueFace = new float[ChaFileDefine.cf_headshapename.Length];
            for (int index = 0; index < this.shapeValueFace.Length; ++index)
                this.shapeValueFace[index] = ChaFileDefine.cf_faceInitValue[index];
            this.headId = 0;
            this.skinId = 0;
            this.detailId = 0;
            this.detailPower = 0.0f;
            this.cheekGlossPower = 0.0f;
            this.eyebrowId = 0;
            this.eyebrowColor = Color.white;
            this.noseId = 0;
            this.pupil = new PupilInfo[2];
            for (int index = 0; index < this.pupil.Length; ++index)
                this.pupil[index] = new PupilInfo();
            this.hlUpId = 0;
            this.hlUpColor = Color.white;
            this.hlDownId = 0;
            this.hlDownColor = Color.white;
            this.whiteId = 0;
            this.whiteBaseColor = Color.white;
            this.whiteSubColor = Color.white;
            this.pupilWidth = 0.9f;
            this.pupilHeight = 0.9f;
            this.pupilX = 0.5f;
            this.pupilY = 0.5f;
            this.hlUpY = 0.5f;
            this.hlDownY = 0.5f;
            this.eyelineUpId = 0;
            this.eyelineUpWeight = 1f;
            this.eyelineDownId = 0;
            this.eyelineColor = Color.white;
            this.moleId = 0;
            this.moleColor = Color.white;
            this.moleLayout = new Vector4(0.5f, 0.5f, 0.0f, 0.5f);
            this.lipLineId = 0;
            this.lipLineColor = Color.white;
            this.lipGlossPower = 0.0f;
            this.doubleTooth = false;
            this.baseMakeup = new ChaFileMakeup();
            this.foregroundEyes = 0;
            this.foregroundEyebrow = 0;
        }

        public void ComplementWithVersion()
        {
            if (this.version.CompareTo(new Version("0.0.1")) == -1)
            {
                for (int index = 0; index < 2; ++index)
                {
                    this.pupil[index].gradOffsetY = Mathf.InverseLerp(-0.5f, 0.5f, this.pupil[index].gradOffsetY);
                    this.pupil[index].gradScale = Mathf.InverseLerp(-1f, 1f, this.pupil[index].gradScale);
                }
                Vector4 zero = Vector4.zero;
                zero.x = Mathf.InverseLerp(-0.25f, 0.25f, this.moleLayout.x);
                zero.y = Mathf.InverseLerp(-0.3f, 0.3f, this.moleLayout.y);
                zero.w = Mathf.InverseLerp(0.0f, 0.7f, this.moleLayout.w);
                this.moleLayout = zero;
            }
            if (this.version.CompareTo(new Version("0.0.2")) == -1)
            {
                this.hlUpY = 0.5f;
                this.hlDownY = 0.5f;
            }
            this.version = ChaFileDefine.ChaFileFaceVersion;
        }

        [MessagePackObject(true)]
        public class PupilInfo
        {
            public PupilInfo()
            {
                this.MemberInit();
            }

            public int id { get; set; }

            public Color baseColor { get; set; }

            public Color subColor { get; set; }

            public int gradMaskId { get; set; }

            public float gradBlend { get; set; }

            public float gradOffsetY { get; set; }

            public float gradScale { get; set; }

            public void MemberInit()
            {
                this.id = 0;
                this.baseColor = Color.black;
                this.subColor = Color.white;
                this.gradMaskId = 0;
                this.gradBlend = 0.0f;
                this.gradOffsetY = 0.0f;
                this.gradScale = 0.0f;
            }

            public void Copy(PupilInfo src)
            {
                this.id = src.id;
                this.baseColor = src.baseColor;
                this.subColor = src.subColor;
                this.gradMaskId = src.gradMaskId;
                this.gradBlend = src.gradBlend;
                this.gradOffsetY = src.gradOffsetY;
                this.gradScale = src.gradScale;
            }
        }
    }
}

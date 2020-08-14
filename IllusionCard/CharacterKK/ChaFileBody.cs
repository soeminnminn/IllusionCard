using System;
using UnityEngine;
using MessagePack;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileBody
    {
        public ChaFileBody()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public float[] shapeValueBody { get; set; }

        public float bustSoftness { get; set; }

        public float bustWeight { get; set; }

        public int skinId { get; set; }

        public int detailId { get; set; }

        public float detailPower { get; set; }

        public Color skinMainColor { get; set; }

        public Color skinSubColor { get; set; }

        public float skinGlossPower { get; set; }

        public int[] paintId { get; set; }

        public Color[] paintColor { get; set; }

        public int[] paintLayoutId { get; set; }

        public Vector4[] paintLayout { get; set; }

        public int sunburnId { get; set; }

        public Color sunburnColor { get; set; }

        public int nipId { get; set; }

        public Color nipColor { get; set; }

        public float nipGlossPower { get; set; }

        public float areolaSize { get; set; }

        public int underhairId { get; set; }

        public Color underhairColor { get; set; }

        public Color nailColor { get; set; }

        public float nailGlossPower { get; set; }

        public bool drawAddLine { get; set; }

        public int typeBone { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileBodyVersion;
            this.shapeValueBody = new float[ChaFileDefine.cf_bodyshapename.Length];
            for (int index = 0; index < this.shapeValueBody.Length; ++index)
                this.shapeValueBody[index] = 0.5f;
            this.bustSoftness = 0.5f;
            this.bustWeight = 0.5f;
            this.skinId = 0;
            this.detailId = 0;
            this.detailPower = 0.0f;
            this.skinMainColor = Color.white;
            this.skinSubColor = Color.white;
            this.skinGlossPower = 0.0f;
            this.paintId = new int[2];
            this.paintColor = new Color[2];
            this.paintLayoutId = new int[2];
            this.paintLayout = new Vector4[2];
            for (int index = 0; index < 2; ++index)
                this.paintLayout[index] = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
            this.sunburnId = 0;
            this.sunburnColor = Color.white;
            this.nipId = 0;
            this.nipColor = Color.white;
            this.nipGlossPower = 0.5f;
            this.areolaSize = 0.5f;
            this.underhairId = 0;
            this.underhairColor = Color.white;
            this.nailColor = Color.white;
            this.nailGlossPower = 0.5f;
            this.drawAddLine = true;
            this.typeBone = 0;
        }

        public void ComplementWithVersion()
        {
            if (this.version.CompareTo(new Version("0.0.1")) == -1)
            {
                for (int index = 0; index < this.paintLayout.Length; ++index)
                    this.paintLayout[index] = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
                this.paintLayoutId = new int[2];
            }
            if (this.version.CompareTo(new Version("0.0.2")) == -1)
                this.nipGlossPower = 0.5f;
            this.version = ChaFileDefine.ChaFileBodyVersion;
        }
    }
}

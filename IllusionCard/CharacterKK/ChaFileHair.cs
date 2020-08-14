using System;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileHair
    {
        public ChaFileHair()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public PartsInfo[] parts { get; set; }

        public int kind { get; set; }

        public int glossId { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileHairVersion;
            this.parts = new PartsInfo[Enum.GetValues(typeof(ChaFileDefine.HairKind)).Length];
            for (int index = 0; index < this.parts.Length; ++index)
                this.parts[index] = new PartsInfo();
            this.kind = 0;
            this.glossId = 0;
        }

        public void ComplementWithVersion()
        {
            if (this.version.CompareTo(new Version("0.0.3")) == -1)
            {
                PartsInfo[] partsInfoArray = new PartsInfo[Enum.GetValues(typeof(ChaFileDefine.HairKind)).Length];
                for (int index = 0; index < partsInfoArray.Length; ++index)
                    partsInfoArray[index] = this.parts[index];
                this.parts = partsInfoArray;
            }
            if (this.version.CompareTo(new Version("0.0.2")) == -1)
            {
                for (int index = 0; index < this.parts.Length; ++index)
                {
                    Color[] colorArray = new Color[4];
                    Array.Copy(parts[index].acsColor, colorArray, this.parts[index].acsColor.Length);
                    this.parts[index].acsColor = colorArray;
                }
            }
            if (this.version.CompareTo(new Version("0.0.4")) == -1)
            {
                foreach (PartsInfo part in this.parts)
                {
                    part.acsColor[0].a = 1f;
                    part.acsColor[1].a = 1f;
                    part.acsColor[2].a = 1f;
                    part.outlineColor = Color.black;
                }
            }
            this.version = ChaFileDefine.ChaFileHairVersion;
        }

        [MessagePackObject(true)]
        public class PartsInfo
        {
            public PartsInfo()
            {
                this.MemberInit();
            }

            public int id { get; set; }

            public Color baseColor { get; set; }

            public Color startColor { get; set; }

            public Color endColor { get; set; }

            public float length { get; set; }

            public Vector3 pos { get; set; }

            public Vector3 rot { get; set; }

            public Vector3 scl { get; set; }

            public Color[] acsColor { get; set; }

            public Color outlineColor { get; set; }

            public bool noShake { get; set; }

            public void MemberInit()
            {
                this.id = 0;
                this.baseColor = Color.white;
                this.startColor = Color.white;
                this.endColor = Color.white;
                this.length = 0.0f;
                this.pos = Vector3.zero;
                this.rot = Vector3.zero;
                this.scl = Vector3.one;
                this.acsColor = new Color[4];
                for (int index = 0; index < this.acsColor.Length; ++index)
                    this.acsColor[index] = Color.white;
                this.outlineColor = Color.black;
                this.noShake = false;
            }
        }
    }
}

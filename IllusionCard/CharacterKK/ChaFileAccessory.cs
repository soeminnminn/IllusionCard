using System;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileAccessory
    {
        public ChaFileAccessory()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public PartsInfo[] parts { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileAccessoryVersion;
            this.parts = new PartsInfo[20];
            for (int index = 0; index < this.parts.Length; ++index)
                this.parts[index] = new PartsInfo();
        }

        public void ComplementWithVersion()
        {
            if (this.version.CompareTo(new Version("0.0.1")) == -1)
            {
                for (int index = 0; index < this.parts.Length; ++index)
                {
                    Color[] colorArray = new Color[4];
                    Array.Copy(parts[index].color, colorArray, this.parts[index].color.Length);
                    this.parts[index].color = colorArray;
                }
            }
            this.version = ChaFileDefine.ChaFileAccessoryVersion;
        }

        [MessagePackObject(true)]
        public class PartsInfo
        {
            public PartsInfo()
            {
                this.MemberInit();
            }

            public int type { get; set; }

            public int id { get; set; }

            public string parentKey { get; set; }

            public Vector3[,] addMove { get; set; }

            public Color[] color { get; set; }

            public int hideCategory { get; set; }

            public bool noShake { get; set; }

            [IgnoreMember]
            public bool partsOfHead { get; set; }

            public void MemberInit()
            {
                this.type = 120;
                this.id = 0;
                this.parentKey = string.Empty;
                this.addMove = new Vector3[2, 3];
                for (int index = 0; index < 2; ++index)
                {
                    this.addMove[index, 0] = Vector3.zero;
                    this.addMove[index, 1] = Vector3.zero;
                    this.addMove[index, 2] = Vector3.one;
                }
                this.color = new Color[4];
                for (int index = 0; index < this.color.Length; ++index)
                    this.color[index] = Color.white;
                this.hideCategory = 0;
                this.partsOfHead = false;
                this.noShake = false;
            }
        }
    }
}

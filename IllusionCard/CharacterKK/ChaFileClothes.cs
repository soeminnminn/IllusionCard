using System;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileClothes
    {
        public ChaFileClothes()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public PartsInfo[] parts { get; set; }

        public int[] subPartsId { get; set; }

        public bool[] hideBraOpt { get; set; }

        public bool[] hideShortsOpt { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileClothesVersion;
            this.parts = new PartsInfo[Enum.GetValues(typeof(ChaFileDefine.ClothesKind)).Length];
            for (int index = 0; index < this.parts.Length; ++index)
                this.parts[index] = new PartsInfo();
            this.subPartsId = new int[Enum.GetValues(typeof(ChaFileDefine.ClothesSubKind)).Length];
            this.hideBraOpt = new bool[2];
            this.hideShortsOpt = new bool[2];
        }

        public void ComplementWithVersion()
        {
            if (this.version.CompareTo(new Version("0.0.1")) == -1)
            {
                this.hideBraOpt = new bool[2];
                this.hideShortsOpt = new bool[2];
            }
            for (int index = 0; index < this.parts.Length; ++index)
            {
                if (this.parts[index] != null && this.parts[index].hideOpt == null)
                    this.parts[index].hideOpt = new bool[2];
            }
            this.version = ChaFileDefine.ChaFileClothesVersion;
        }

        [MessagePackObject(true)]
        public class PartsInfo
        {
            public PartsInfo()
            {
                this.MemberInit();
            }

            public int id { get; set; }

            public ColorInfo[] colorInfo { get; set; }

            public int emblemeId { get; set; }

            public int emblemeId2 { get; set; }

            public bool[] hideOpt { get; set; }

            public int sleevesType { get; set; }

            public void MemberInit()
            {
                this.id = 0;
                this.colorInfo = new ColorInfo[4];
                for (int index = 0; index < this.colorInfo.Length; ++index)
                    this.colorInfo[index] = new ColorInfo();
                this.emblemeId = 0;
                this.emblemeId2 = 0;
                this.hideOpt = new bool[2];
                this.sleevesType = 0;
            }

            [MessagePackObject(true)]
            public class ColorInfo
            {
                public ColorInfo()
                {
                    this.MemberInit();
                }

                public Color baseColor { get; set; }

                public int pattern { get; set; }

                public Vector2 tiling { get; set; }

                public Color patternColor { get; set; }

                public void MemberInit()
                {
                    this.baseColor = Color.white;
                    this.pattern = 0;
                    this.tiling = Vector2.zero;
                    this.patternColor = Color.white;
                }
            }
        }
    }
}

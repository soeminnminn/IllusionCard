using System;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileMakeup
    {
        public ChaFileMakeup()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public int eyeshadowId { get; set; }

        public Color eyeshadowColor { get; set; }

        public int cheekId { get; set; }

        public Color cheekColor { get; set; }

        public int lipId { get; set; }

        public Color lipColor { get; set; }

        public int[] paintId { get; set; }

        public Color[] paintColor { get; set; }

        public Vector4[] paintLayout { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileMakeupVersion;
            this.eyeshadowId = 0;
            this.eyeshadowColor = Color.white;
            this.cheekId = 0;
            this.cheekColor = Color.white;
            this.lipId = 0;
            this.lipColor = Color.white;
            this.paintId = new int[2];
            this.paintColor = new Color[2];
            this.paintLayout = new Vector4[2];
            for (int index = 0; index < 2; ++index)
                this.paintLayout[index] = new Vector4(0.5f, 0.5f, 0.5f, 0.7f);
        }

        public void ComplementWithVersion()
        {
            this.version = ChaFileDefine.ChaFileMakeupVersion;
        }
    }
}

using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
    [MessagePackObject(true)]
    public class ChaFileParameter2
    {
        [IgnoreMember]
        public static readonly string BlockName = "Parameter2";

        public Version version { get; set; }

        public int personality { get; set; }

        public float voiceRate { get; set; }

        [IgnoreMember]
        public float voicePitch
        {
            get
            {
                return Mathf.Lerp(0.94f, 1.06f, this.voiceRate);
            }
        }

        public byte trait { get; set; }

        public byte mind { get; set; }

        public byte hAttribute { get; set; }

        public ChaFileParameter2()
        {
            this.MemberInit();
        }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileParameterVersion2;
            this.personality = 0;
            this.voiceRate = 0.5f;
            this.trait = 0;
            this.mind = 0;
            this.hAttribute = 0;
        }

        public void Copy(ChaFileParameter2 src)
        {
            this.version = src.version;
            this.personality = src.personality;
            this.voiceRate = src.voiceRate;
            this.trait = src.trait;
            this.mind = src.mind;
            this.hAttribute = src.hAttribute;
        }

        public void ComplementWithVersion()
        {
            this.version = ChaFileDefine.ChaFileParameterVersion2;
        }
    }
}

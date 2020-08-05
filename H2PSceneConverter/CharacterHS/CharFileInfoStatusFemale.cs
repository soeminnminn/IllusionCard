// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoStatusFemale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoStatusFemale : CharFileInfoStatus
    {
        public byte[] siruLv;
        public float nipStand;
        public float hohoAkaRate;
        public bool disableShapeBustL;
        public bool disableShapeBustR;
        public bool disableShapeNipL;
        public bool disableShapeNipR;
        public byte tearsLv;
        public bool hideEyesHighlight;

        public CharFileInfoStatusFemale()
        {
            this.siruLv = new byte[Enum.GetValues(typeof(CharDefine.SiruParts)).Length];
            this.clothesState = new byte[Enum.GetNames(typeof(CharDefine.ClothesStateKindFemale)).Length];
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
            for (int index = 0; index < this.siruLv.Length; ++index)
                this.siruLv[index] = 0;
            this.nipStand = 0.0f;
            this.hohoAkaRate = 0.0f;
            this.tearsLv = 0;
            this.hideEyesHighlight = false;
            this.disableShapeBustL = false;
            this.disableShapeBustR = false;
            this.disableShapeNipL = false;
            this.disableShapeNipR = false;
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            bw.Write(this.clothesState.Length);
            for (int index = 0; index < this.clothesState.Length; ++index)
                bw.Write(this.clothesState[index]);
            bw.Write(this.siruLv.Length);
            for (int index = 0; index < this.siruLv.Length; ++index)
                bw.Write(this.siruLv[index]);
            bw.Write(this.nipStand);
            bw.Write(this.hohoAkaRate);
            bw.Write(this.tearsLv);
            bw.Write(this.disableShapeBustL);
            bw.Write(this.disableShapeBustR);
            bw.Write(this.disableShapeNipL);
            bw.Write(this.disableShapeNipR);
            bw.Write(this.hideEyesHighlight);
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int statusVer)
        {
            int num1 = br.ReadInt32();
            for (int index = 0; index < num1; ++index)
                this.clothesState[index] = br.ReadByte();
            int num2 = br.ReadInt32();
            for (int index = 0; index < num2; ++index)
                this.siruLv[index] = br.ReadByte();
            this.nipStand = CharFile.ClampEx(br.ReadSingle(), 0.0f, 1f);
            this.hohoAkaRate = br.ReadSingle();
            this.tearsLv = br.ReadByte();
            this.disableShapeBustL = br.ReadBoolean();
            if (3 <= statusVer)
            {
                this.disableShapeBustR = br.ReadBoolean();
                this.disableShapeNipL = br.ReadBoolean();
                this.disableShapeNipR = br.ReadBoolean();
            }
            this.hideEyesHighlight = br.ReadBoolean();
            return true;
        }
    }
}

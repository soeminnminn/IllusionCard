// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoParameterFemale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoParameterFemale : CharFileInfoParameter
    {
        public State nowState = State.Blank;
        public State calcState = State.Blank;
        public bool[][] genericVoice = new bool[4][];
        public bool genericFlag = true;
        public bool genericBefore = true;
        public bool[] inviteVoice = new bool[5];
        public List<int> map = new List<int>();
        public bool InitParameter = true;
        public int favor;
        public int lewdness;
        public int aversion;
        public int slavery;
        public int broken;
        public bool lockNowState;
        public bool lockBroken;
        public int dirty;
        public int tiredness;
        public int toilette;
        public int libido;
        public int alertness;
        public byte escapeFlag;
        public bool escapeExperienced;
        public bool firstHFlag;
        public int hCount;
        public int resistH;
        public int resistPain;
        public int resistAnal;
        public int usedItem;
        public int characteristic;
        public int impression;
        public int attribute;

        public CharFileInfoParameterFemale()
        {
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
            for (int index = 0; index < 4; ++index)
                this.genericVoice[index] = new bool[3];
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            bw.Write(this.favor);
            bw.Write(this.lewdness);
            bw.Write(this.aversion);
            bw.Write(this.slavery);
            bw.Write(this.broken);
            bw.Write((int)this.nowState);
            bw.Write(this.lockNowState);
            bw.Write(this.lockBroken);
            bw.Write(this.dirty);
            bw.Write(this.tiredness);
            bw.Write(this.toilette);
            bw.Write(this.libido);
            bw.Write(this.alertness);
            bw.Write((int)this.calcState);
            bw.Write(this.escapeFlag);
            bw.Write(this.escapeExperienced);
            bw.Write(this.firstHFlag);
            bw.Write(this.hCount);
            bw.Write(this.map.Count);
            for (int index = 0; index < this.map.Count; ++index)
                bw.Write(this.map[index]);
            bw.Write(this.resistH);
            bw.Write(this.resistPain);
            bw.Write(this.resistAnal);
            bw.Write(this.usedItem);
            bw.Write(this.characteristic);
            bw.Write(this.impression);
            bw.Write(this.attribute);
            for (int index1 = 0; index1 < 4; ++index1)
            {
                for (int index2 = 0; index2 < 3; ++index2)
                    bw.Write(this.genericVoice[index1][index2]);
            }
            bw.Write(this.genericFlag);
            bw.Write(this.genericBefore);
            for (int index = 0; index < 5; ++index)
                bw.Write(this.inviteVoice[index]);
            bw.Write(this.InitParameter);
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int parameterVer)
        {
            if (2 <= parameterVer)
            {
                this.favor = br.ReadInt32();
                this.lewdness = br.ReadInt32();
                this.aversion = br.ReadInt32();
                this.slavery = br.ReadInt32();
                this.broken = br.ReadInt32();
                this.nowState = (State)br.ReadInt32();
                this.lockNowState = br.ReadBoolean();
                this.lockBroken = br.ReadBoolean();
                this.dirty = br.ReadInt32();
                this.tiredness = br.ReadInt32();
                this.toilette = br.ReadInt32();
                this.libido = br.ReadInt32();
                this.alertness = br.ReadInt32();
                this.calcState = (State)br.ReadInt32();
                this.escapeFlag = br.ReadByte();
                if (4 <= parameterVer)
                    this.escapeExperienced = br.ReadBoolean();
                this.firstHFlag = br.ReadBoolean();
                this.hCount = br.ReadInt32();
                int num = br.ReadInt32();
                this.map.Clear();
                for (int index = 0; index < num; ++index)
                    this.map.Add(br.ReadInt32());
                this.resistH = br.ReadInt32();
                this.resistPain = br.ReadInt32();
                this.resistAnal = br.ReadInt32();
                this.usedItem = br.ReadInt32();
                this.characteristic = br.ReadInt32();
                this.impression = br.ReadInt32();
                this.attribute = br.ReadInt32();
            }
            if (3 <= parameterVer)
            {
                for (int index1 = 0; index1 < 4; ++index1)
                {
                    for (int index2 = 0; index2 < 3; ++index2)
                        this.genericVoice[index1][index2] = br.ReadBoolean();
                }
                this.genericFlag = br.ReadBoolean();
            }
            if (4 <= parameterVer)
            {
                this.genericBefore = br.ReadBoolean();
                for (int index = 0; index < 5; ++index)
                    this.inviteVoice[index] = br.ReadBoolean();
            }
            if (5 <= parameterVer)
                this.InitParameter = br.ReadBoolean();
            return true;
        }

        public enum State
        {
            Favor,
            Lewdness,
            Aversion,
            Slavery,
            Broken,
            Blank,
        }

        public enum Characteristic
        {
            None,
            Kireizuki,
            Monogusa,
            Tukareyasui,
            Tukaresirazu,
            Hinnyo,
            Glassheart,
            Fukutu,
            Yokkyufuman,
        }

        public enum Impression
        {
            None,
            Tanoshii,
            Sukikamo,
            Hitomebore,
            kyoumiari,
            Miryokuteki,
            Dakaretai,
            Hanasizurai,
            Nigate,
            Kirai,
            Sijisaretai,
            Meireisaretai,
            Sakaraenai,
            Tententen,
        }

        public enum Attribute
        {
            None,
            Seiyokuousei,
            Esu,
            Emu,
            MuneBinkan,
            SiriBinkan,
            KokanBinkan,
            Kissyowai,
            Sarerunoyowai,
            Hannozuki,
            Keppekisyou,
            Hteikouari,
            Sabisigariya,
        }
    }
}

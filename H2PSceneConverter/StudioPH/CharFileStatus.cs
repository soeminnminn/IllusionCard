using System;
using System.IO;
using CharacterHS;

namespace StudioPH
{
    public class CharFileStatus
    {
        public string name = "キャラ";
        public float eyesOpen = 1f;
        public float eyesOpenMax = 1f;
        public float eyesFixed = -1f;
        public float mouthOpenMax = 1f;
        public float mouthFixed = -1f;
        public float eyesTargetRate = 1f;
        public int neckLookPtn = 2;
        public float neckTargetRate = 1f;
        public bool eyesBlink = true;
        public byte[] clothesState;
        public bool[] showAccessory;
        public CharDefine.CoordinateType coordinateType;
        public int eyesPtn;
        public float eyesOpenMin;
        public int mouthPtn;
        public float mouthOpen;
        public float mouthOpenMin;
        public byte tongueState;
        public int eyesLookPtn;
        public int eyesTargetNo;
        public int neckTargetNo;
        public bool disableShapeMouth;
        public byte[] siruLv;
        public float nipStand;
        public float hohoAkaRate;
        public bool disableShapeBustL;
        public bool disableShapeBustR;
        public bool disableShapeNipL;
        public bool disableShapeNipR;
        public float tearsLv;
        public bool hideEyesHighlight;

        public CharFileStatus()
        {
            this.showAccessory = new bool[10];
            this.siruLv = new byte[Enum.GetValues(typeof(CharDefine.SiruParts)).Length];
            this.clothesState = new byte[Enum.GetNames(typeof(CharDefine.ClothesStateKindFemale)).Length];
            this.MemberInitialize();
        }

        protected void MemberInitialize()
        {
            for (int index = 0; index < this.clothesState.Length; ++index)
                this.clothesState[index] = 0;
            for (int index = 0; index < this.showAccessory.Length; ++index)
                this.showAccessory[index] = true;
            for (int index = 0; index < this.siruLv.Length; ++index)
                this.siruLv[index] = 0;
            this.nipStand = 0.0f;
            this.hohoAkaRate = 0.0f;
            this.tearsLv = 0.0f;
            this.hideEyesHighlight = false;
            this.disableShapeBustL = false;
            this.disableShapeBustR = false;
            this.disableShapeNipL = false;
            this.disableShapeNipR = false;
        }

        public void Save(BinaryWriter _bw)
        {
            _bw.Write((int)this.coordinateType);
            _bw.Write(10);
            for (int index = 0; index < this.showAccessory.Length; ++index)
                _bw.Write(this.showAccessory[index]);
            _bw.Write(this.eyesPtn);
            _bw.Write(this.eyesOpen);
            _bw.Write(this.eyesOpenMin);
            _bw.Write(this.eyesOpenMax);
            _bw.Write(this.eyesFixed);
            _bw.Write(this.mouthPtn);
            _bw.Write(this.mouthOpen);
            _bw.Write(this.mouthOpenMin);
            _bw.Write(this.mouthOpenMax);
            _bw.Write(this.mouthFixed);
            _bw.Write(this.tongueState);
            _bw.Write(this.eyesLookPtn);
            _bw.Write(this.eyesTargetNo);
            _bw.Write(this.eyesTargetRate);
            _bw.Write(this.neckLookPtn);
            _bw.Write(this.neckTargetNo);
            _bw.Write(this.neckTargetRate);
            _bw.Write(this.eyesBlink);
            _bw.Write(this.disableShapeMouth);
            this.SaveSub(_bw);
            _bw.Write(this.name);
        }

        public void Load(BinaryReader _br, Version _version)
        {
            this.coordinateType = (CharDefine.CoordinateType)_br.ReadInt32();
            int num = _br.ReadInt32();
            for (int index = 0; index < num; ++index)
                this.showAccessory[index] = _br.ReadBoolean();
            this.eyesPtn = _br.ReadInt32();
            this.eyesOpen = _br.ReadSingle();
            this.eyesOpenMin = _br.ReadSingle();
            this.eyesOpenMax = _br.ReadSingle();
            this.eyesFixed = _br.ReadSingle();
            this.mouthPtn = _br.ReadInt32();
            this.mouthOpen = _br.ReadSingle();
            this.mouthOpenMin = _br.ReadSingle();
            this.mouthOpenMax = _br.ReadSingle();
            this.mouthFixed = _br.ReadSingle();
            this.tongueState = _br.ReadByte();
            this.eyesLookPtn = _br.ReadInt32();
            this.eyesTargetNo = _br.ReadInt32();
            this.eyesTargetRate = _br.ReadSingle();
            this.neckLookPtn = _br.ReadInt32();
            this.neckTargetNo = _br.ReadInt32();
            this.neckTargetRate = _br.ReadSingle();
            this.eyesBlink = _br.ReadBoolean();
            this.disableShapeMouth = _br.ReadBoolean();
            this.LoadSub(_br, _version);
            if (_version.CompareTo(new Version(0, 1, 4)) < 0)
                return;
            this.name = _br.ReadString();
        }

        protected void SaveSub(BinaryWriter bw)
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
        }

        protected void LoadSub(BinaryReader br, Version _version)
        {
            int num1 = br.ReadInt32();
            for (int index = 0; index < num1; ++index)
                this.clothesState[index] = br.ReadByte();
            int num2 = br.ReadInt32();
            for (int index = 0; index < num2; ++index)
                this.siruLv[index] = br.ReadByte();
            this.nipStand = CharFile.ClampEx(br.ReadSingle(), 0.0f, 1f);
            this.hohoAkaRate = br.ReadSingle();
            this.tearsLv = br.ReadSingle();
            this.disableShapeBustL = br.ReadBoolean();
            this.disableShapeBustR = br.ReadBoolean();
            this.disableShapeNipL = br.ReadBoolean();
            this.disableShapeNipR = br.ReadBoolean();
            this.hideEyesHighlight = br.ReadBoolean();
        }
    }
}

// Decompiled with JetBrains decompiler
// Type: StudioHS.OICharInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CharacterHS;

namespace StudioHS
{
    public class OICharInfo : ObjectInfo
    {
        public AnimeInfo animeInfo = new AnimeInfo();
        public int[] handPtn = new int[2];
        public bool lipSync = true;
        public bool[] activeIK = new bool[5]
        {
          true, true, true, true, true
        };
        public KinematicMode kinematicMode;
        public float mouthOpen;
        public bool enableIK;
        public bool enableFK;
        public bool[] activeFK;
        public bool[] expression;
        public float animeSpeed;
        public float animePattern;
        public bool animeOptionVisible;
        public bool isAnimeForceLoop;
        public VoiceCtrl voiceCtrl;
        public float skinRate;
        public float nipple;
        public byte[] siru;
        public bool visibleSimple;
        public HSColorSet simpleColor;
        public bool visibleSon;
        public float[] animeOptionParam;
        public byte[] neckByteData;
        public byte[] eyesByteData;
        public float animeNormalizedTime;
        public Dictionary<int, TreeNodeObject.TreeState> dicAccessGroup;
        public Dictionary<int, TreeNodeObject.TreeState> dicAccessNo;

        public OICharInfo(CharFile _charFile, int _key)
          : base(_key)
        {
            bool[] flagArray = new bool[7];
            flagArray[1] = true;
            flagArray[3] = true;
            this.activeFK = flagArray;
            this.expression = new bool[4];
            this.animeSpeed = 1f;
            this.animeOptionVisible = true;
            this.voiceCtrl = new VoiceCtrl();
            this.siru = new byte[5];
            this.animeOptionParam = new float[2];
            this.sex = _charFile == null ? 0 : (int)_charFile.customInfo.sex;
            this.charFile = _charFile;
            this.bones = new Dictionary<int, OIBoneInfo>();
            this.ikTarget = new Dictionary<int, OIIKTargetInfo>();
            this.child = new Dictionary<int, List<ObjectInfo>>();
            int length = Enum.GetNames(typeof(AccessoryPoint)).Length;
            for (int index = 0; index < length; ++index)
                this.child[index] = new List<ObjectInfo>();
            if (this.sex == 0)
            {
                this.simpleColor = new HSColorSet();
                this.simpleColor.SetDiffuseRGBA(Color.blue);
            }
            this.dicAccessGroup = new Dictionary<int, TreeNodeObject.TreeState>();
            this.dicAccessNo = new Dictionary<int, TreeNodeObject.TreeState>();
        }

        public override int kind
        {
            get
            {
                return 0;
            }
        }

        public int sex { get; private set; }

        public CharFile charFile { get; private set; }

        public Dictionary<int, OIBoneInfo> bones { get; private set; }

        public Dictionary<int, OIIKTargetInfo> ikTarget { get; private set; }

        public Dictionary<int, List<ObjectInfo>> child { get; private set; }

        public LookAtTargetInfo lookAtTarget { get; set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.sex);
            this.charFile.SaveWithoutPNG(_writer);
            int count1 = this.bones.Count;
            _writer.Write(count1);
            foreach (KeyValuePair<int, OIBoneInfo> bone in this.bones)
            {
                int key = bone.Key;
                _writer.Write(key);
                bone.Value.Save(_writer, _version);
            }
            int count2 = this.ikTarget.Count;
            _writer.Write(count2);
            foreach (KeyValuePair<int, OIIKTargetInfo> keyValuePair in this.ikTarget)
            {
                int key = keyValuePair.Key;
                _writer.Write(key);
                keyValuePair.Value.Save(_writer, _version);
            }
            int count3 = this.child.Count;
            _writer.Write(count3);
            foreach (KeyValuePair<int, List<ObjectInfo>> keyValuePair in this.child)
            {
                int key = keyValuePair.Key;
                _writer.Write(key);
                int count4 = keyValuePair.Value.Count;
                _writer.Write(count4);
                for (int index = 0; index < count4; ++index)
                    keyValuePair.Value[index].Save(_writer, _version);
            }
            _writer.Write((int)this.kinematicMode);
            _writer.Write(this.animeInfo.group);
            _writer.Write(this.animeInfo.category);
            _writer.Write(this.animeInfo.no);
            for (int index = 0; index < 2; ++index)
                _writer.Write(this.handPtn[index]);
            _writer.Write(this.skinRate);
            _writer.Write(this.nipple);
            _writer.Write(this.siru);
            _writer.Write(this.mouthOpen);
            _writer.Write(this.lipSync);
            this.lookAtTarget.Save(_writer, _version);
            _writer.Write(this.enableIK);
            for (int index = 0; index < 5; ++index)
                _writer.Write(this.activeIK[index]);
            _writer.Write(this.enableFK);
            for (int index = 0; index < 7; ++index)
                _writer.Write(this.activeFK[index]);
            for (int index = 0; index < 4; ++index)
                _writer.Write(this.expression[index]);
            _writer.Write(this.animeSpeed);
            _writer.Write(this.animePattern);
            _writer.Write(this.animeOptionVisible);
            _writer.Write(this.isAnimeForceLoop);
            this.voiceCtrl.Save(_writer, _version);
            if (this.sex == 0)
            {
                _writer.Write(this.visibleSimple);
                _writer.Write(1);
                this.simpleColor.Save(_writer);
                _writer.Write(this.visibleSon);
                _writer.Write(this.animeOptionParam[0]);
                _writer.Write(this.animeOptionParam[1]);
            }
            _writer.Write(this.neckByteData.Length);
            _writer.Write(this.neckByteData);
            _writer.Write(this.eyesByteData.Length);
            _writer.Write(this.eyesByteData);
            _writer.Write(this.animeNormalizedTime);
            int num1 = this.dicAccessGroup == null ? 0 : this.dicAccessGroup.Count;
            _writer.Write(num1);
            if (num1 != 0)
            {
                foreach (KeyValuePair<int, TreeNodeObject.TreeState> keyValuePair in this.dicAccessGroup)
                {
                    _writer.Write(keyValuePair.Key);
                    _writer.Write((int)keyValuePair.Value);
                }
            }
            int num2 = this.dicAccessNo == null ? 0 : this.dicAccessNo.Count;
            _writer.Write(num2);
            if (num2 == 0)
                return;
            foreach (KeyValuePair<int, TreeNodeObject.TreeState> keyValuePair in this.dicAccessNo)
            {
                _writer.Write(keyValuePair.Key);
                _writer.Write((int)keyValuePair.Value);
            }
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.sex = _reader.ReadInt32();
            this.charFile = this.sex != 1 ? (CharFile)new CharMaleFile() : (CharFile)new CharFemaleFile();
            this.charFile.Load(_reader, true, false);
            int num1 = _reader.ReadInt32();
            for (int index1 = 0; index1 < num1; ++index1)
            {
                int index2 = _reader.ReadInt32();
                this.bones[index2] = new OIBoneInfo(-1);
                this.bones[index2].Load(_reader, _version, _import, true);
            }
            int num2 = _reader.ReadInt32();
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = _reader.ReadInt32();
                this.ikTarget[index2] = new OIIKTargetInfo(-1);
                this.ikTarget[index2].Load(_reader, _version, _import, true);
            }
            int num3 = _reader.ReadInt32();
            for (int index1 = 0; index1 < num3; ++index1)
            {
                int index2 = _reader.ReadInt32();
                ObjectInfoAssist.LoadChild(_reader, _version, this.child[index2], _import);
            }
            this.kinematicMode = (KinematicMode)_reader.ReadInt32();
            this.animeInfo.group = _reader.ReadInt32();
            this.animeInfo.category = _reader.ReadInt32();
            this.animeInfo.no = _reader.ReadInt32();
            for (int index = 0; index < 2; ++index)
                this.handPtn[index] = _reader.ReadInt32();
            this.skinRate = _reader.ReadSingle();
            this.nipple = _reader.ReadSingle();
            if (_version.CompareTo(new Version(0, 1, 3)) >= 0)
                this.siru = _reader.ReadBytes(5);
            if (_version.CompareTo(new Version(0, 1, 1)) >= 0)
                this.mouthOpen = _reader.ReadSingle();
            this.lipSync = _reader.ReadBoolean();
            if (this.lookAtTarget == null)
                this.lookAtTarget = new LookAtTargetInfo(-1);
            this.lookAtTarget.Load(_reader, _version, _import, true);
            this.enableIK = _reader.ReadBoolean();
            for (int index = 0; index < 5; ++index)
                this.activeIK[index] = _reader.ReadBoolean();
            this.enableFK = _reader.ReadBoolean();
            for (int index = 0; index < 7; ++index)
                this.activeFK[index] = _reader.ReadBoolean();
            for (int index = 0; index < 4; ++index)
                this.expression[index] = _reader.ReadBoolean();
            this.animeSpeed = _reader.ReadSingle();
            this.animePattern = _reader.ReadSingle();
            if (_version.CompareTo(new Version(0, 1, 1)) >= 0)
                this.animeOptionVisible = _reader.ReadBoolean();
            if (_version.CompareTo(new Version(0, 1, 5)) >= 0)
                this.isAnimeForceLoop = _reader.ReadBoolean();
            this.voiceCtrl.Load(_reader, _version);
            if (this.sex == 0)
            {
                this.visibleSimple = _reader.ReadBoolean();
                int version = _reader.ReadInt32();
                if (this.simpleColor == null)
                    this.simpleColor = new HSColorSet();
                this.simpleColor.Load(_reader, version);
                this.visibleSon = _reader.ReadBoolean();
                if (_version.CompareTo(new Version(0, 1, 2)) >= 0)
                {
                    this.animeOptionParam[0] = _reader.ReadSingle();
                    this.animeOptionParam[1] = _reader.ReadSingle();
                }
            }
            int count1 = _reader.ReadInt32();
            this.neckByteData = _reader.ReadBytes(count1);
            if (_version.CompareTo(new Version(0, 1, 4)) >= 0)
            {
                int count2 = _reader.ReadInt32();
                this.eyesByteData = _reader.ReadBytes(count2);
            }
            this.animeNormalizedTime = _reader.ReadSingle();
            if (_version.CompareTo(new Version(1, 0, 2)) < 0)
                return;
            int num4 = _reader.ReadInt32();
            if (num4 != 0)
                this.dicAccessGroup = new Dictionary<int, TreeNodeObject.TreeState>();
            for (int index = 0; index < num4; ++index)
                this.dicAccessGroup[_reader.ReadInt32()] = (TreeNodeObject.TreeState)_reader.ReadInt32();
            int num5 = _reader.ReadInt32();
            if (num5 != 0)
                this.dicAccessNo = new Dictionary<int, TreeNodeObject.TreeState>();
            for (int index = 0; index < num5; ++index)
                this.dicAccessNo[_reader.ReadInt32()] = (TreeNodeObject.TreeState)_reader.ReadInt32();
        }

        public enum AccessoryPoint
        {
            AP_Head,
            AP_Megane,
            AP_Nose,
            AP_Mouth,
            AP_Earring_L,
            AP_Earring_R,
            AP_Neck,
            AP_Chest,
            AP_Waist,
            AP_Tikubi_L,
            AP_Tikubi_R,
            AP_Shoulder_L,
            AP_Shoulder_R,
            AP_Arm_L,
            AP_Arm_R,
            AP_Wrist_L,
            AP_Wrist_R,
            AP_Hand_L_NEO,
            AP_Hand_R,
            AP_Index_L,
            AP_Middle_L,
            AP_Ring_L,
            AP_Index_R,
            AP_Middle_R,
            AP_Ring_R,
            AP_Leg_L,
            AP_Leg_R,
            AP_Ankle_L,
            AP_Ankle_R,
        }

        public enum IKTargetEN
        {
            Body,
            LeftShoulder,
            LeftArmChain,
            LeftHand,
            RightShoulder,
            RightArmChain,
            RightHand,
            LeftThigh,
            LeftLegChain,
            LeftFoot,
            RightThigh,
            RightLegChain,
            RightFoot,
        }

        public enum KinematicMode
        {
            None,
            FK,
            IK,
        }

        public class AnimeInfo
        {
            public int category = 2;
            public int no = 11;
            public int group;

            public void Set(int _group, int _category, int _no)
            {
                this.group = _group;
                this.category = _category;
                this.no = _no;
            }

            public bool exist
            {
                get
                {
                    return this.group != -1 & this.category != -1 & this.no != -1;
                }
            }

            public void Copy(AnimeInfo _src)
            {
                this.group = _src.group;
                this.category = _src.category;
                this.no = _src.no;
            }
        }
    }
}

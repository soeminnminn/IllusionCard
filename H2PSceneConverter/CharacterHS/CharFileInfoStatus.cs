// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoStatus
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public abstract class CharFileInfoStatus : BlockControlBase
    {
        public float eyesOpen = 1f;
        public float eyesOpenMax = 1f;
        public float eyesFixed = -1f;
        public float mouthOpenMax = 1f;
        public float mouthFixed = -1f;
        public float eyesTargetRate = 1f;
        public float neckTargetRate = 1f;
        public bool eyesBlink = true;
        public int statusLoadVersion;
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
        public int neckLookPtn;
        public int neckTargetNo;
        public bool disableShapeMouth;

        public CharFileInfoStatus()
          : base("ステータス情報", 4)
        {
            this.showAccessory = new bool[10];
        }

        protected void MemberInitialize()
        {
            for (int index = 0; index < this.clothesState.Length; ++index)
                this.clothesState[index] = 0;
            for (int index = 0; index < this.showAccessory.Length; ++index)
                this.showAccessory[index] = true;
        }

        public override byte[] SaveBytes()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(memoryStream))
                {
                    bw.Write((int)this.coordinateType);
                    bw.Write(10);
                    for (int index = 0; index < this.showAccessory.Length; ++index)
                        bw.Write(this.showAccessory[index]);
                    bw.Write(this.eyesPtn);
                    bw.Write(this.eyesOpen);
                    bw.Write(this.eyesOpenMin);
                    bw.Write(this.eyesOpenMax);
                    bw.Write(this.eyesFixed);
                    bw.Write(this.mouthPtn);
                    bw.Write(this.mouthOpen);
                    bw.Write(this.mouthOpenMin);
                    bw.Write(this.mouthOpenMax);
                    bw.Write(this.mouthFixed);
                    bw.Write(this.tongueState);
                    bw.Write(this.eyesLookPtn);
                    bw.Write(this.eyesTargetNo);
                    bw.Write(this.eyesTargetRate);
                    bw.Write(this.neckLookPtn);
                    bw.Write(this.neckTargetNo);
                    bw.Write(this.neckTargetRate);
                    bw.Write(this.eyesBlink);
                    bw.Write(this.disableShapeMouth);
                    this.SaveSub(bw);
                    return memoryStream.ToArray();
                }
            }
        }

        public override bool LoadBytes(byte[] data, int statusVer)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(memoryStream))
                {
                    this.coordinateType = (CharDefine.CoordinateType)br.ReadInt32();
                    int num = br.ReadInt32();
                    for (int index = 0; index < num; ++index)
                        this.showAccessory[index] = br.ReadBoolean();
                    this.eyesPtn = br.ReadInt32();
                    this.eyesOpen = br.ReadSingle();
                    if (4 <= statusVer)
                    {
                        this.eyesOpenMin = br.ReadSingle();
                        this.eyesOpenMax = br.ReadSingle();
                    }
                    this.eyesFixed = br.ReadSingle();
                    this.mouthPtn = br.ReadInt32();
                    this.mouthOpen = br.ReadSingle();
                    if (4 <= statusVer)
                    {
                        this.mouthOpenMin = br.ReadSingle();
                        this.mouthOpenMax = br.ReadSingle();
                    }
                    this.mouthFixed = br.ReadSingle();
                    this.tongueState = br.ReadByte();
                    this.eyesLookPtn = br.ReadInt32();
                    this.eyesTargetNo = br.ReadInt32();
                    this.eyesTargetRate = br.ReadSingle();
                    this.neckLookPtn = br.ReadInt32();
                    this.neckTargetNo = br.ReadInt32();
                    this.neckTargetRate = br.ReadSingle();
                    this.eyesBlink = br.ReadBoolean();
                    if (2 <= statusVer)
                        this.disableShapeMouth = br.ReadBoolean();
                    return this.LoadSub(br, statusVer);
                }
            }
        }

        protected abstract bool SaveSub(BinaryWriter bw);

        protected abstract bool LoadSub(BinaryReader br, int statusVer);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileStatus
    {
        [IgnoreMember]
        public static readonly string BlockName = "Status";

        public ChaFileStatus()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public int coordinateType { get; set; }

        public int backCoordinateType { get; set; }

        public byte[] clothesState { get; set; }

        public byte shoesType { get; set; }

        public bool[] showAccessory { get; set; }

        public int eyebrowPtn { get; set; }

        public float eyebrowOpenMax { get; set; }

        public int eyesPtn { get; set; }

        public float eyesOpenMax { get; set; }

        public bool eyesBlink { get; set; }

        public bool eyesYure { get; set; }

        public int mouthPtn { get; set; }

        public float mouthOpenMax { get; set; }

        public bool mouthFixed { get; set; }

        public bool mouthAdjustWidth { get; set; }

        public byte tongueState { get; set; }

        public int eyesLookPtn { get; set; }

        public int eyesTargetType { get; set; }

        public float eyesTargetAngle { get; set; }

        public float eyesTargetRange { get; set; }

        public float eyesTargetRate { get; set; }

        public int neckLookPtn { get; set; }

        public int neckTargetType { get; set; }

        public float neckTargetAngle { get; set; }

        public float neckTargetRange { get; set; }

        public float neckTargetRate { get; set; }

        public bool disableMouthShapeMask { get; set; }

        public bool[,] disableBustShapeMask { get; set; }

        public float nipStandRate { get; set; }

        public float skinTuyaRate { get; set; }

        public float hohoAkaRate { get; set; }

        public byte tearsLv { get; set; }

        public bool hideEyesHighlight { get; set; }

        public byte[] siruLv { get; set; }

        public bool visibleSon { get; set; }

        public bool visibleSonAlways { get; set; }

        public bool visibleHeadAlways { get; set; }

        public bool visibleBodyAlways { get; set; }

        public bool visibleSimple { get; set; }

        public bool visibleGomu { get; set; }

        public Color simpleColor { get; set; }

        public bool[] enableShapeHand { get; set; }

        public int[,] shapeHandPtn { get; set; }

        public float[] shapeHandBlendValue { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileStatusVersion;
            this.coordinateType = 0;
            this.backCoordinateType = 0;
            this.clothesState = new byte[Enum.GetValues(typeof(ChaFileDefine.ClothesKind)).Length];
            this.shoesType = 0;
            this.showAccessory = new bool[20];
            for (int index = 0; index < this.showAccessory.Length; ++index)
                this.showAccessory[index] = true;
            this.eyebrowPtn = 0;
            this.eyebrowOpenMax = 1f;
            this.eyesPtn = 0;
            this.eyesOpenMax = 1f;
            this.eyesBlink = true;
            this.eyesYure = false;
            this.mouthPtn = 0;
            this.mouthOpenMax = 1f;
            this.mouthFixed = false;
            this.mouthAdjustWidth = true;
            this.tongueState = 0;
            this.eyesLookPtn = 0;
            this.eyesTargetType = 0;
            this.eyesTargetAngle = 0.0f;
            this.eyesTargetRange = 1f;
            this.eyesTargetRate = 1f;
            this.neckLookPtn = 0;
            this.neckTargetType = 0;
            this.neckTargetAngle = 0.0f;
            this.neckTargetRange = 1f;
            this.neckTargetRate = 1f;
            this.disableMouthShapeMask = false;
            this.disableBustShapeMask = new bool[2, ChaFileDefine.cf_BustShapeMaskID.Length];
            this.nipStandRate = 0.0f;
            this.skinTuyaRate = 0.0f;
            this.hohoAkaRate = 0.0f;
            this.tearsLv = 0;
            this.hideEyesHighlight = false;
            this.siruLv = new byte[Enum.GetValues(typeof(ChaFileDefine.SiruParts)).Length];
            this.visibleSon = false;
            this.visibleSonAlways = true;
            this.visibleHeadAlways = true;
            this.visibleBodyAlways = true;
            this.visibleSimple = false;
            this.visibleGomu = false;
            this.simpleColor = new Color(0.188f, 0.286f, 0.8f, 0.5f);
            this.enableShapeHand = new bool[2];
            this.shapeHandPtn = new int[2, 2];
            this.shapeHandBlendValue = new float[2];
        }

        public void Copy(ChaFileStatus src)
        {
            this.coordinateType = src.coordinateType;
            this.backCoordinateType = src.backCoordinateType;
            this.clothesState = src.clothesState;
            this.shoesType = src.shoesType;
            this.showAccessory = src.showAccessory;
            this.eyebrowPtn = src.eyebrowPtn;
            this.eyebrowOpenMax = src.eyebrowOpenMax;
            this.eyesPtn = src.eyesPtn;
            this.eyesOpenMax = src.eyesOpenMax;
            this.eyesBlink = src.eyesBlink;
            this.eyesYure = src.eyesYure;
            this.mouthPtn = src.mouthPtn;
            this.mouthOpenMax = src.mouthOpenMax;
            this.mouthFixed = src.mouthFixed;
            this.mouthAdjustWidth = src.mouthAdjustWidth;
            this.tongueState = src.tongueState;
            this.eyesLookPtn = src.eyesLookPtn;
            this.eyesTargetType = src.eyesTargetType;
            this.eyesTargetAngle = src.eyesTargetAngle;
            this.eyesTargetRange = src.eyesTargetRange;
            this.eyesTargetRate = src.eyesTargetRate;
            this.neckLookPtn = src.neckLookPtn;
            this.neckTargetType = src.neckTargetType;
            this.neckTargetAngle = src.neckTargetAngle;
            this.neckTargetRange = src.neckTargetRange;
            this.neckTargetRate = src.neckTargetRate;
            this.disableMouthShapeMask = src.disableMouthShapeMask;
            this.disableBustShapeMask = src.disableBustShapeMask;
            this.nipStandRate = src.nipStandRate;
            this.skinTuyaRate = src.skinTuyaRate;
            this.hohoAkaRate = src.hohoAkaRate;
            this.tearsLv = src.tearsLv;
            this.hideEyesHighlight = src.hideEyesHighlight;
            this.siruLv = src.siruLv;
            this.visibleSon = src.visibleSon;
            this.visibleSonAlways = src.visibleSonAlways;
            this.visibleHeadAlways = src.visibleHeadAlways;
            this.visibleBodyAlways = src.visibleBodyAlways;
            this.visibleSimple = src.visibleSimple;
            this.visibleGomu = src.visibleGomu;
            this.simpleColor = src.simpleColor;
            this.enableShapeHand = src.enableShapeHand;
            this.shapeHandPtn = src.shapeHandPtn;
            this.shapeHandBlendValue = src.shapeHandBlendValue;
        }

        public void ComplementWithVersion()
        {
            if (this.enableShapeHand == null)
            {
                this.enableShapeHand = new bool[2];
                for (int index = 0; index < 2; ++index)
                    this.enableShapeHand[index] = false;
                this.shapeHandPtn = new int[2, 2];
                for (int index1 = 0; index1 < 2; ++index1)
                {
                    for (int index2 = 0; index2 < 2; ++index2)
                        this.shapeHandPtn[index1, index2] = 0;
                }
                this.shapeHandBlendValue = new float[2];
                for (int index = 0; index < 2; ++index)
                    this.shapeHandBlendValue[index] = 0.0f;
            }
            this.version = ChaFileDefine.ChaFileStatusVersion;
        }
    }
}

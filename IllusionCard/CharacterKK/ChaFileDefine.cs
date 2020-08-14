using System;
using UnityEngine;

namespace CharacterKK
{
    public static class ChaFileDefine
    {
        public static readonly Version ChaFileVersion = new Version("0.0.0");
        public static readonly Version ChaFileCustomVersion = new Version("0.0.0");
        public static readonly Version ChaFileFaceVersion = new Version("0.0.2");
        public static readonly Version ChaFileBodyVersion = new Version("0.0.2");
        public static readonly Version ChaFileHairVersion = new Version("0.0.4");
        public static readonly Version ChaFileCoordinateVersion = new Version("0.0.0");
        public static readonly Version ChaFileClothesVersion = new Version("0.0.1");
        public static readonly Version ChaFileAccessoryVersion = new Version("0.0.2");
        public static readonly Version ChaFileMakeupVersion = new Version("0.0.0");
        public static readonly Version ChaFileParameterVersion = new Version("0.0.5");
        public static readonly Version ChaFileStatusVersion = new Version("0.0.0");
        public static readonly string[] cf_bodyshapename = new string[44]
        {
            "身長",
            "頭サイズ",
            "首周り幅",
            "首周り奥",
            "胸サイズ",
            "胸上下位置",
            "胸の左右開き",
            "胸の左右位置",
            "胸上下角度",
            "胸の尖り",
            "胸形状",
            "乳輪の膨らみ",
            "乳首太さ",
            "乳首立ち",
            "胴体肩周り幅",
            "胴体肩周り奥",
            "胴体上幅",
            "胴体上奥",
            "胴体下幅",
            "胴体下奥",
            "ウエスト位置",
            "腹部",
            "腰上幅",
            "腰上奥",
            "腰下幅",
            "腰下奥",
            "尻",
            "尻角度",
            "太もも上幅",
            "太もも上奥",
            "太もも下幅",
            "太もも下奥",
            "膝下幅",
            "膝下奥",
            "ふくらはぎ",
            "足首幅",
            "足首奥",
            "肩幅",
            "肩奥",
            "上腕幅",
            "上腕奥",
            "肘周り幅",
            "肘周り奥",
            "前腕"
        };
        public static readonly int[] cf_BustShapeMaskID = new int[9]
        {
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13
        };
        public static readonly int[] cf_ShapeMaskBust = new int[6]
        {
            0,
            1,
            2,
            3,
            4,
            5
        };
        public static readonly int[] cf_ShapeMaskNip = new int[2]
        {
            6,
            7
        };
        public static readonly int cf_ShapeMaskNipStand = 8;
        public static readonly int[] cf_CategoryWaist = new int[6]
        {
            18,
            19,
            22,
            23,
            24,
            25
        };
        public static readonly string[] cf_headshapename = new string[52]
        {
            "顔全体横幅",
            "顔上部前後",
            "顔上部上下",
            "顔上部サイズ",
            "顔下部前後",
            "顔下部横幅",
            "顎下部上下",
            "顎下部奥行",
            "顎上下",
            "顎幅",
            "顎前後",
            "顎先上下",
            "顎先前後",
            "顎先幅",
            "頬骨幅",
            "頬骨前後",
            "頬幅",
            "頬前後",
            "頬上下",
            "眉毛上下",
            "眉毛横位置",
            "眉毛角度Z軸",
            "眉毛内側形状",
            "眉毛外側形状",
            "上まぶた形状１",
            "上まぶた形状２",
            "上まぶた形状３",
            "下まぶた形状１",
            "下まぶた形状２",
            "下まぶた形状３",
            "目上下",
            "目横位置",
            "目前後",
            "目の角度",
            "目の縦幅",
            "目の横幅",
            "目頭左右位置",
            "目尻上下位置",
            "鼻先高さ",
            "鼻上下",
            "鼻筋高さ",
            "口上下",
            "口横幅",
            "口前後",
            "口形状上",
            "口形状下",
            "口形状口角",
            "耳サイズ",
            "耳角度Y軸",
            "耳角度Z軸",
            "耳上部形状",
            "耳下部形状"
        };
        public static readonly int[] cf_MouthShapeMaskID = new int[7]
        {
            4,
            41,
            42,
            43,
            44,
            45,
            46
        };
        public static readonly float[,] cf_MouthShapeDefault = new float[3, 7]
        {
            {
              0.5f,
              0.5f,
              0.0f,
              0.5f,
              0.0f,
              0.0f,
              0.5f
            },
            {
              0.5f,
              0.5f,
              0.0f,
              0.5f,
              0.0f,
              0.0f,
              0.5f
            },
            {
              0.5f,
              0.0f,
              0.0f,
              0.5f,
              0.0f,
              0.0f,
              0.5f
            }
        };
        public static readonly float[] cf_faceInitValue = new float[52]
        {
            0.5f,
            0.0f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.0f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.0f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.0f,
            0.0f,
            0.5f,
            0.0f,
            0.0f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.0f,
            0.0f
        };
        public static readonly Color[] defClothesSubColor = new Color[4]
        {
            new Color(0.882f, 0.882f, 0.882f),
            new Color(0.376f, 0.431f, 0.529f),
            new Color(0.882f, 0.882f, 0.882f),
            new Color(0.784f, 0.352f, 0.352f)
        };
        public const int AccessoryCategoryTypeNone = 120;
        public const int AccessoryColorNum = 4;
        public const int AccessoryCorrectNum = 2;
        public const int AccessorySlotNum = 20;
        public const int CustomPaintNum = 2;
        public const int EmblemNum = 2;
        public const float VoicePitchMin = 0.94f;
        public const float VoicePitchMax = 1.06f;
        public const int ProductNo = 100;
        public const string CharaFileMark = "【KoiKatuChara】";
        public const string ClothesFileMark = "【KoiKatuClothes】";
        public const string CharaFileFemaleDir = "chara/female/";
        public const string CharaFileMaleDir = "chara/male/";
        public const string CoordinateFileDir = "coordinate/";
        public const int LoadError_Tag = -1;
        public const int LoadError_Version = -2;
        public const int LoadError_ProductNo = -3;
        public const int LoadError_EndOfStream = -4;
        public const int LoadError_OnlyPNG = -5;
        public const int LoadError_FileNotExist = -6;
        public const int LoadError_ETC = -999;
        public const int DefHeadID = 0;
        public const int DefHairBackID = 0;
        public const int DefHairFrontID = 1;
        public const int DefHairSideID = 0;
        public const int DefHairOptionID = 0;
        public const int DefClothesTopID = 0;
        public const int DefClothesBotID = 0;
        public const int DefClothesBraID = 0;
        public const int DefClothesFShortsID = 0;
        public const int DefClothesMShortsID = 0;
        public const int DefClothesGlovesID = 0;
        public const int DefClothesPanstID = 0;
        public const int DefClothesSocksID = 0;
        public const int DefClothesShoes_InnerID = 0;
        public const int DefClothesShoes_OuterID = 0;
        public const int DefClothesSailorPA_ID = 0;
        public const int DefClothesSailorPB_ID = 0;
        public const int DefClothesSailorPC_ID = 1;
        public const int DefClothesJacketPA_ID = 0;
        public const int DefClothesJacketPB_ID = 1;
        public const int DefClothesJacketPC_ID = 1;
        public const float cf_ShapeLowMin = 0.2f;
        public const float cf_ShapeLowMax = 0.8f;

        public static string GetBloodTypeStr(int bloodType)
        {
            string[] strArray = new string[4]
            {
              "Ａ型",
              "Ｂ型",
              "Ｏ型",
              "ＡＢ型"
            };
            return MathfEx.RangeEqualOn<int>(0, bloodType, strArray.Length - 1) ? strArray[bloodType] : "不明";
        }

        public enum BodyShapeIdx
        {
            Height,
            HeadSize,
            NeckW,
            NeckZ,
            BustSize,
            BustY,
            BustRotX,
            BustX,
            BustRotY,
            BustSharp,
            BustForm,
            AreolaBulge,
            NipWeight,
            NipStand,
            BodyShoulderW,
            BodyShoulderZ,
            BodyUpW,
            BodyUpZ,
            BodyLowW,
            BodyLowZ,
            WaistY,
            Belly,
            WaistUpW,
            WaistUpZ,
            WaistLowW,
            WaistLowZ,
            Hip,
            HipRotX,
            ThighUpW,
            ThighUpZ,
            ThighLowW,
            ThighLowZ,
            KneeLowW,
            KneeLowZ,
            Calf,
            AnkleW,
            AnkleZ,
            ShoulderW,
            ShoulderZ,
            ArmUpW,
            ArmUpZ,
            ElbowW,
            ElbowZ,
            ArmLow,
        }

        public enum FaceShapeIdx
        {
            FaceBaseW,
            FaceUpZ,
            FaceUpY,
            FaceUpSize,
            FaceLowZ,
            FaceLowW,
            ChinLowY,
            ChinLowZ,
            ChinY,
            ChinW,
            ChinZ,
            ChinTipY,
            ChinTipZ,
            ChinTipW,
            CheekBoneW,
            CheekBoneZ,
            CheekW,
            CheekZ,
            CheekY,
            EyebrowY,
            EyebrowX,
            EyebrowRotZ,
            EyebrowInForm,
            EyebrowOutForm,
            EyelidsUpForm1,
            EyelidsUpForm2,
            EyelidsUpForm3,
            EyelidsLowForm1,
            EyelidsLowForm2,
            EyelidsLowForm3,
            EyeY,
            EyeX,
            EyeZ,
            EyeTilt,
            EyeH,
            EyeW,
            EyeInX,
            EyeOutY,
            NoseTipH,
            NoseY,
            NoseBridgeH,
            MouthY,
            MouthW,
            MouthZ,
            MouthUpForm,
            MouthLowForm,
            MouthCornerForm,
            EarSize,
            EarRotY,
            EarRotZ,
            EarUpForm,
            EarLowForm,
        }

        public enum HairKind
        {
            back,
            front,
            side,
            option,
        }

        public enum ClothesKind
        {
            top,
            bot,
            bra,
            shorts,
            gloves,
            panst,
            socks,
            shoes_inner,
            shoes_outer,
        }

        public enum ClothesSubKind
        {
            partsA,
            partsB,
            partsC,
        }

        public enum CoordinateType
        {
            School01,
            School02,
            Gym,
            Swim,
            Club,
            Plain,
            Pajamas,
        }

        public enum SiruParts
        {
            SiruKao,
            SiruFrontUp,
            SiruFrontDown,
            SiruBackUp,
            SiruBackDown,
        }
    }
}

using System;

namespace CharacterPH
{
    public enum ACCESSORY_ATTACH
    {
        NONE = -1, // 0xFFFFFFFF
        AP_Head = 0,
        AP_Megane = 1,
        AP_Earring_L = 2,
        AP_Earring_R = 3,
        AP_Mouth = 4,
        AP_Nose = 5,
        AP_Neck = 6,
        AP_Chest = 7,
        AP_Wrist_L = 8,
        AP_Wrist_R = 9,
        AP_Arm_L = 10, // 0x0000000A
        AP_Arm_R = 11, // 0x0000000B
        AP_Index_L = 12, // 0x0000000C
        AP_Index_R = 13, // 0x0000000D
        AP_Middle_L = 14, // 0x0000000E
        AP_Middle_R = 15, // 0x0000000F
        AP_Ring_L = 16, // 0x00000010
        AP_Ring_R = 17, // 0x00000011
        AP_Leg_L = 18, // 0x00000012
        AP_Leg_R = 19, // 0x00000013
        AP_Ankle_L = 20, // 0x00000014
        AP_Ankle_R = 21, // 0x00000015
        AP_Tikubi_L = 22, // 0x00000016
        AP_Tikubi_R = 23, // 0x00000017
        AP_Waist = 24, // 0x00000018
        AP_Shoulder_L = 25, // 0x00000019
        AP_Shoulder_R = 26, // 0x0000001A
        AP_Hand_L = 27, // 0x0000001B
        AP_Hand_R = 28, // 0x0000001C
        SKINNING = 29, // 0x0000001D
        NUM = 30, // 0x0000001E
    }

    public enum ACCESSORY_ATTACHTYPE
    {
        NONE = -1, // 0xFFFFFFFF
        HEAD = 0,
        EAR = 1,
        GLASSES = 2,
        MOUTH = 3,
        NOSE = 4,
        NECK = 5,
        CHEST = 6,
        WRIST = 7,
        ARM = 8,
        FINGER = 9,
        LEG = 10, // 0x0000000A
        ANKLE = 11, // 0x0000000B
        NIP = 12, // 0x0000000C
        WAIST = 13, // 0x0000000D
        SHOULDER = 14, // 0x0000000E
        HAND = 15, // 0x0000000F
        SKINNING = 16, // 0x00000010
    }

    public enum ACCESSORY_TYPE
    {
        NONE = -1, // 0xFFFFFFFF
        HEAD = 0,
        EAR = 1,
        GLASSES = 2,
        FACE = 3,
        NECK = 4,
        SHOULDER = 5,
        CHEST = 6,
        WAIST = 7,
        BACK = 8,
        ARM = 9,
        HAND = 10, // 0x0000000A
        LEG = 11, // 0x0000000B
        NUM = 12, // 0x0000000C
    }

    public enum COLOR_TYPE
    {
        NONE,
        HAIR,
        PBR1,
        PBR2,
        ALLOY,
        ALLOY_HSV,
        EYE,
        EYEHIGHLIGHT,
    }

    public enum CUSTOM_DATA_VERSION
    {
        UNKNOWN = -1, // 0xFFFFFFFF
        DEBUG_00 = 0,
        DEBUG_01 = 1,
        DEBUG_02 = 2,
        DEBUG_03 = 3,
        DEBUG_04 = 4,
        DEBUG_05 = 5,
        DEBUG_06 = 6,
        DEBUG_07 = 7,
        TRIAL = 8,
        DEBUG_09 = 9,
        DEBUG_10 = 10, // 0x0000000A
        NEW = 10, // 0x0000000A
        NEXT = 11, // 0x0000000B
    }

    public enum EYE
    {
        L = 1,
        R = 2,
        LR = 3,
    }

    public enum GAG_ITEM
    {
        NONE,
        BALLGAG,
        GUMTAPE,
    }

    public enum HAIR_TYPE
    {
        BACK,
        FRONT,
        SIDE,
        NUM,
    }

    public enum HEROINE
    {
        RITSUKO,
        AKIKO,
        YUKIKO,
        MARIKO,
        NUM,
    }

    public enum LOADFILTER
    {
        HAIR = 1,
        FACE = 2,
        BODY = 4,
        WEAR = 8,
        ACCE = 16, // 0x00000010
    }

    public enum LOAD_MSG
    {
        PERFECT = 0,
        DO_NOT_LOAD = 1,
        ISOMERISM = 2,
        VER_SEXYBEACH = 4,
        VER_HONEYSELECT = 8,
    }

    public enum MALE_ID
    {
        HERO,
        KOUICHI,
        MOB_A,
        MOB_B,
        MOB_C,
        NUM,
    }

    public enum MALE_SHOW
    {
        CLOTHING,
        NUDE,
        ONECOLOR,
        TIN,
        HIDE,
        NUM,
    }

    public enum SEX
    {
        FEMALE,
        MALE,
    }

    public enum SPERM_POS
    {
        FACE,
        BUST,
        BACK,
        CROTCH,
        HIP,
        NUM,
    }

    public enum TONGUE_TYPE
    {
        FACE,
        BODY,
    }

    public enum VISITOR
    {
        NONE = -1, // 0xFFFFFFFF
        RITSUKO = 0,
        AKIKO = 1,
        YUKIKO = 2,
        MARIKO = 3,
        KOUICHI = 999, // 0x000003E7
    }

    public enum WEAR_SHOW
    {
        ALL,
        HALF,
        HIDE,
    }

    public enum WEAR_SHOW_TYPE
    {
        TOPUPPER,
        TOPLOWER,
        BOTTOM,
        BRA,
        SHORTS,
        SWIMUPPER,
        SWIMLOWER,
        SWIM_TOPUPPER,
        SWIM_TOPLOWER,
        SWIM_BOTTOM,
        GLOVE,
        PANST,
        SOCKS,
        SHOES,
        NUM,
    }

    public enum WEAR_TYPE
    {
        TOP,
        BOTTOM,
        BRA,
        SHORTS,
        SWIM,
        SWIM_TOP,
        SWIM_BOTTOM,
        GLOVE,
        PANST,
        SOCKS,
        SHOES,
        NUM,
    }
}

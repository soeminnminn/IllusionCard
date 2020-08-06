using System;
namespace StudioNeoV2
{
    public class Info
    {
        public static Info Instance
        {
            get => new Info();
        }

        public int[] AccessoryPointsIndex
        {
            get
            {
                // TODO : Not Implemented
                throw new NotImplementedException();
            }
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
    }
}

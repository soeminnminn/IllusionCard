using System;

namespace SexyBeachPR
{
    public static class CharDefine
    {
        public static readonly int[] FemaleShapeBodyAdd = new int[7]
        {
            1, 3, 4, 5, 6, 7, 8
        };

        public const int ListFileMax = 99;
        public const string ResourcesFemaleDir = "Prefabs/chara/female/";
        public const string ResourcesMaleDir = "Prefabs/chara/male/";
        public const string ResourcesAccessoryDir = "Prefabs/chara/accessory/";

        public const int MaleShepeFaceNum = 67;
        public const int MaleShepeBodyNum = 21;
        public const int FemaleShepeFaceNum = 67;
        public const int FemaleShepeBodyNum = 32;
        public const int CoordinateNum = 2;
        public const int AccessorySlotNum = 5;
        
        public const int CharaFileVersion = 1;
        public const int PreviewVersion = 1;
        public const int CustomVersion = 5;
        public const int ClothesVersion = 2;
        public const int FacePngVersion = 100;
        
        public const string CharaFileFemaleDir = "chara/female/";
        public const string CharaFileMaleDir = "chara/male/";
        
        public const string CharaFileFemaleMark = "【PremiumResortCharaFemale】";
        public const string CharaFileMaleMark = "【PremiumResortCharaMale】";

        public enum BlockId
        {
            BID_Preview,
            BID_Custom,
            BID_Clothes,
            BID_FacePng,
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

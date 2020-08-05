using System;

namespace SexyBeachPR
{
    public class CharFemaleBody : CharBody
    {
        public byte[] siruNowLv = new byte[Enum.GetValues(typeof(CharDefine.SiruParts)).Length];
        protected byte[] siruNewLv = new byte[Enum.GetValues(typeof(CharDefine.SiruParts)).Length];
        protected string[] clothesFavorite = new string[5];
        protected int[] clothesDefence = new int[5];
        protected int nowTopsCode;

        public CharFemaleClothes charClothes { get; protected set; }

        public CharFemaleCustom charCustom { get; protected set; }

        protected override void Init(CharaListInfo info, byte sex, int id, int no)
        {
            base.Init(info, sex, id, no);
        }
    }
}

using System;

namespace SexyBeachPR
{
    [Serializable]
    public class CharMaleBody : CharBody
    {
        public CharMaleClothes charClothes { get; protected set; }

        public CharMaleCustom charCustom { get; protected set; }

        protected override void Init(CharaListInfo info, byte sex, int id, int no)
        {
            base.Init(info, sex, id, no);
            this.charClothes = new CharMaleClothes(this);
            this.charCustom = new CharMaleCustom(this);
        }
    }
}

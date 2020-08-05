using System;

namespace SexyBeachPR
{
    [Serializable]
    public class CharBody : CharSave
    {
        protected CharFemaleBody female;
        protected CharMaleBody male;

        public CharaListInfo ListInfo { get; protected set; }

        public int LoadNo { get; protected set; }

        public float voiceCorrectValue { protected set; get; }

        protected override void Init(CharaListInfo info, byte sex, int id, int no)
        {
            base.Init(info, sex, id, no);
            this.ListInfo = info;
            this.LoadNo = no;

            if (sex == 0)
            {
                this.male = this as CharMale;
            }
            else
            {
                this.female = this as CharFemale;
            }
        }

        public void SetVoiceCorrectValue(float value)
        {
            this.voiceCorrectValue = value;
        }

        public CharCustom GetCharCustomInstance()
        {
            return this.Sex == 0 ? male.charCustom : (CharCustom)this.female.charCustom;
        }

        public CharClothes GetCharClothesInstance()
        {
            return this.Sex == 0 ? male.charClothes : (CharClothes)this.female.charClothes;
        }
    }
}

using System;
using System.Collections.Generic;

namespace SexyBeachPR
{
    [Serializable]
    public class CharFemale : CharFemaleBody
    {
        public List<string> lstFavorite = new List<string>();

        public int charID { get; private set; }

        public int charType { get; private set; }

        public void Initialize(CharaListInfo info, byte sex, int id, int no)
        {
            this.Init(info, sex, id, no);
            
            CharIdInfo info1 = new CharIdInfo();
            if (!info.GetCharInfoID(id, info1))
                return;
            
            this.charID = id;
            this.charType = info1.Type;
            
            if (this.charCustom != null)
            {
                this.charCustom.name = info1.Name;
                this.charCustom.personality = info1.InitialPersonality;
                if (this.charType == 0)
                    this.charCustom.requiredDefence = info1.RequiredDefence;
            }
            
            if (this.charType != 0)
                return;

            PersonalityIdInfo info2 = new PersonalityIdInfo();
            info.GetPersonalityInfo(info1.InitialPersonality, info2);
            using (List<string>.Enumerator enumerator = info1.lstDislikeType.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    this.lstFavorite.Add(enumerator.Current);
            }
        }
    }
}

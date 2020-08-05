using System;

namespace SexyBeachPR
{
    [Serializable]
    public class CharMale : CharMaleBody
    {
        public void Initialize(CharaListInfo info, byte sex, int id, int no)
        {
            this.Init(info, sex, id, no);
        }
    }
}

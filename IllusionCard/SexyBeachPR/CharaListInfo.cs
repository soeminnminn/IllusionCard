using System;
using System.Collections.Generic;

namespace SexyBeachPR
{
    public class CharaListInfo
    {
        private Dictionary<int, CharIdInfo> dictCharIdInfo = new Dictionary<int, CharIdInfo>();
        private Dictionary<int, PersonalityIdInfo> dictPersonalityIdInfo = new Dictionary<int, PersonalityIdInfo>();

        public static CharaListInfo Instance
        {
            get => new CharaListInfo();
        }

        public Dictionary<int, CharIdInfo> GetCharIdInfo()
        {
            return this.dictCharIdInfo;
        }

        public Dictionary<int, PersonalityIdInfo> GetPersonalityIdInfo()
        {
            return this.dictPersonalityIdInfo;
        }

        public bool GetCharInfoID(int id, CharIdInfo info)
        {
            if (!this.dictCharIdInfo.ContainsKey(id))
                return false;
            info.Copy(this.dictCharIdInfo[id]);
            return true;
        }

        public bool GetPersonalityInfo(int personality, PersonalityIdInfo info)
        {
            if (!this.dictPersonalityIdInfo.ContainsKey(personality))
                return false;
            info.Copy(this.dictPersonalityIdInfo[personality]);
            return true;
        }

        public bool IsPersonality(int personality)
        {
            return this.dictPersonalityIdInfo.ContainsKey(personality);
        }
    }
}

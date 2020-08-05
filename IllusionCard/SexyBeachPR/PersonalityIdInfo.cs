using System;

namespace SexyBeachPR
{
    public class PersonalityIdInfo
    {
        public string Name = string.Empty;
        public bool enableCustom = true;
        public string samplVoice01 = string.Empty;
        public string samplVoice02 = string.Empty;
        public int charId = 14;
        private const int min = 7;
        public int Id;
        public float voiceCorrect;

        public void Set(string[] data)
        {
            if (data.Length < 7)
                return;
            this.Id = int.Parse(data[0]);
            this.Name = data[1];
            this.voiceCorrect = float.Parse(data[2]);
            this.enableCustom = int.Parse(data[3]) != 0;
            this.samplVoice01 = data[4];
            this.samplVoice02 = data[5];
            this.charId = int.Parse(data[6]);
        }

        public void Copy(PersonalityIdInfo src)
        {
            this.Id = src.Id;
            this.Name = src.Name;
            this.voiceCorrect = src.voiceCorrect;
            this.enableCustom = src.enableCustom;
            this.samplVoice01 = src.samplVoice01;
            this.samplVoice02 = src.samplVoice02;
            this.charId = src.charId;
        }
    }
}

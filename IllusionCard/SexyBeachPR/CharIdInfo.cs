using System;
using System.Collections.Generic;

namespace SexyBeachPR
{
    public class CharIdInfo
    {
        public string Name = string.Empty;
        public string DefaultFile = string.Empty;
        public List<string> lstDislikeType = new List<string>();
        public Dictionary<int, List<int>> dictClearCos = new Dictionary<int, List<int>>();
        private const int min = 11;
        public int Id;
        public int Type;
        public int Love;
        public int InitialPersonality;
        public int Staff;
        public bool FixedAppearance;
        public int RequiredDefence;

        public void Set(string[] data)
        {
            if (data.Length < 11)
                return;
            this.Id = int.Parse(data[0]);
            this.Name = data[1];
            this.Type = int.Parse(data[2]);
            this.Love = int.Parse(data[3]);
            this.InitialPersonality = int.Parse(data[4]);
            this.Staff = int.Parse(data[5]);
            this.DefaultFile = data[6];
            this.FixedAppearance = int.Parse(data[7]) != 0;
            this.RequiredDefence = int.Parse(data[8]);
            string str1 = data[9];
            char[] chArray1 = new char[1] { '/' };
            foreach (string str2 in str1.Split(chArray1))
                this.lstDislikeType.Add(str2);
            if (!("0" != data[10]))
                return;
            string str3 = data[10];
            char[] chArray2 = new char[1] { '/' };
            foreach (string str2 in str3.Split(chArray2))
            {
                char[] chArray3 = new char[1] { '&' };
                string[] strArray = str2.Split(chArray3);
                if (strArray.Length == 2)
                {
                    int key = int.Parse(strArray[0]);
                    int num = int.Parse(strArray[1]);
                    if (!this.dictClearCos.ContainsKey(key))
                        this.dictClearCos[key] = new List<int>();
                    this.dictClearCos[key].Add(num);
                }
            }
        }

        public void Copy(CharIdInfo src)
        {
            this.Id = src.Id;
            this.Name = src.Name;
            this.Type = src.Type;
            this.Love = src.Love;
            this.InitialPersonality = src.InitialPersonality;
            this.Staff = src.Staff;
            this.DefaultFile = src.DefaultFile;
            this.FixedAppearance = src.FixedAppearance;
            this.RequiredDefence = src.RequiredDefence;
            this.lstDislikeType.Clear();
            using (List<string>.Enumerator enumerator = src.lstDislikeType.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    this.lstDislikeType.Add(enumerator.Current);
            }
            this.dictClearCos.Clear();
            using (Dictionary<int, List<int>>.Enumerator enumerator = src.dictClearCos.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<int, List<int>> current = enumerator.Current;
                    this.dictClearCos[current.Key] = current.Value;
                }
            }
        }
    }
}

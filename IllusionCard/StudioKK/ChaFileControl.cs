using System;
using System.IO;
using CharacterKK;

namespace StudioKK
{
    public class ChaFileControl : ChaFile
    {
        public bool SaveCharaFile(Stream st, bool savePng)
        {
            using (BinaryWriter bw = new BinaryWriter(st))
                return this.SaveCharaFile(bw, savePng);
        }

        public bool SaveCharaFile(BinaryWriter bw, bool savePng)
        {
            return this.SaveFile(bw, savePng);
        }

        public bool LoadCharaFile(string filename, byte sex = 255, bool noLoadPng = false, bool noLoadStatus = true)
        {
            this.charaFileName = Path.GetFileName(filename);
            using (FileStream fileStream = new FileStream(this.ConvertCharaFilePath(filename, sex, false), FileMode.Open, FileAccess.Read))
                return this.LoadCharaFile(fileStream, noLoadPng, noLoadStatus);
        }

        public bool LoadCharaFile(Stream st, bool noLoadPng = false, bool noLoadStatus = true)
        {
            using (BinaryReader br = new BinaryReader(st))
                return this.LoadCharaFile(br, noLoadPng, noLoadStatus);
        }

        public bool LoadCharaFile(BinaryReader br, bool noLoadPng = false, bool noLoadStatus = true)
        {
            bool flag = this.LoadFile(br, noLoadPng, noLoadStatus);
            return flag;
        }

        public string ConvertCharaFilePath(string path, byte _sex, bool newFile = false)
        {
            byte num = _sex != byte.MaxValue ? _sex : this.parameter.sex;
            string str1 = string.Empty;
            string path1 = string.Empty;
            if (path != string.Empty)
            {
                str1 = Path.GetDirectoryName(path);
                path1 = Path.GetFileName(path);
            }
            string str2 = !(str1 == string.Empty) ? str1 + "/" : UserData.Path + (num != 0 ? "chara/female/" : "chara/male/");
            if (path1 == string.Empty)
                path1 = newFile || this.charaFileName == string.Empty ? (num != 0 ? "KoiKatu_F_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") : "KoiKatu_M_" + DateTime.Now.ToString("yyyyMMddHHmmssfff")) : this.charaFileName;
            return string.IsNullOrEmpty(Path.GetExtension(path1)) ? str2 + Path.GetFileNameWithoutExtension(path1) + ".png" : str2 + path1;
        }
    }
}

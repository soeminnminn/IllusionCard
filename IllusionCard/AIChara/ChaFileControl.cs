using System;
using System.IO;

namespace AIChara
{
    public class ChaFileControl : ChaFile
    {
        public bool skipRangeCheck = false;

        public bool SaveCharaFile(string filename, byte sex = 255, bool newFile = false)
        {
            string path = this.ConvertCharaFilePath(filename, sex, newFile);
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            this.charaFileName = Path.GetFileName(path);
            string userId = this.userID;
            string dataId = this.dataID;
            if (this.userID != GameSystem.Instance.UserUUID)
                this.dataID = YS_Assist.CreateUUID();
            else if (!File.Exists(path))
                this.dataID = YS_Assist.CreateUUID();
            this.userID = GameSystem.Instance.UserUUID;
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                int num = this.SaveCharaFile(fileStream, true) ? 1 : 0;
                this.userID = userId;
                this.dataID = dataId;
                return num != 0;
            }
        }

        public bool SaveCharaFile(Stream st, bool savePng)
        {
            using (BinaryWriter bw = new BinaryWriter(st))
                return this.SaveCharaFile(bw, savePng);
        }

        public bool SaveCharaFile(BinaryWriter bw, bool savePng)
        {
            return this.SaveFile(bw, savePng, (int)GameSystem.Instance.language);
        }

        public bool LoadCharaFile(string filename, byte sex = 255, bool noLoadPng = false, bool noLoadStatus = true)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            this.charaFileName = Path.GetFileName(filename);
            string path = this.ConvertCharaFilePath(filename, sex, false);
            if (!File.Exists(path))
                return false;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return this.LoadCharaFile(fileStream, noLoadPng, noLoadStatus);
        }

        public bool LoadCharaFile(Stream st, bool noLoadPng = false, bool noLoadStatus = true)
        {
            using (BinaryReader br = new BinaryReader(st))
                return this.LoadCharaFile(br, noLoadPng, noLoadStatus);
        }

        public bool LoadCharaFile(BinaryReader br, bool noLoadPng = false, bool noLoadStatus = true)
        {
            int num = this.LoadFile(br, (int)GameSystem.Instance.language, noLoadPng, noLoadStatus) ? 1 : 0;
            if (this.skipRangeCheck)
                return num != 0;
            return num != 0;
        }

        public string ConvertCharaFilePath(string path, byte _sex, bool newFile = false)
        {
            byte num = byte.MaxValue == _sex ? this.parameter.sex : _sex;
            if (!(path != ""))
                return "";
            string directoryName = Path.GetDirectoryName(path);
            string path1 = Path.GetFileName(path);
            string str = !(directoryName == "") ? directoryName + "/" : UserData.Path + (num == (byte)0 ? "chara/male/" : "chara/female/");
            if (path1 == "")
                path1 = newFile || this.charaFileName == "" ? (num != (byte)0 ? "HS2ChaF_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") : "HS2ChaM_" + DateTime.Now.ToString("yyyyMMddHHmmssfff")) : this.charaFileName;
            return string.IsNullOrEmpty(Path.GetExtension(path1)) ? str + Path.GetFileNameWithoutExtension(path1) + ".png" : str + path1;
        }
    }
}

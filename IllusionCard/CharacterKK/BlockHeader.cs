using System;
using System.Collections.Generic;
using MessagePack;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class BlockHeader
    {
        public BlockHeader()
        {
            this.lstInfo = new List<Info>();
        }

        public List<Info> lstInfo { get; set; }

        public Info SearchInfo(string name)
        {
            return this.lstInfo.Find(n => n.name == name);
        }

        [MessagePackObject(true)]
        public class Info
        {
            public Info()
            {
                this.name = string.Empty;
                this.version = string.Empty;
                this.pos = 0L;
                this.size = 0L;
            }

            public string name { get; set; }

            public string version { get; set; }

            public long pos { get; set; }

            public long size { get; set; }
        }
    }
}

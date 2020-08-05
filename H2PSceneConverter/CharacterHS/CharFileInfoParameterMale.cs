// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFileInfoParameterMale
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace CharacterHS
{
    [Serializable]
    public class CharFileInfoParameterMale : CharFileInfoParameter
    {
        public CharFileInfoParameterMale()
        {
            this.MemberInitialize();
        }

        private new void MemberInitialize()
        {
            base.MemberInitialize();
        }

        protected override bool SaveSub(BinaryWriter bw)
        {
            return true;
        }

        protected override bool LoadSub(BinaryReader br, int parameterVer)
        {
            return true;
        }
    }
}

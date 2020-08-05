// Decompiled with JetBrains decompiler
// Type: CharacterHS.BlockControlBase
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;

namespace CharacterHS
{
    [Serializable]
    public abstract class BlockControlBase
    {
        public readonly string tagName = string.Empty;
        public readonly int version;

        public BlockControlBase(string _tagName, int _version)
        {
            this.tagName = _tagName;
            this.version = _version;
        }

        public abstract byte[] SaveBytes();

        public abstract bool LoadBytes(byte[] data, int version);
    }
}

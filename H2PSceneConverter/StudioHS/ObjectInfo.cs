// Decompiled with JetBrains decompiler
// Type: StudioHS.ObjectInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;

namespace StudioHS
{
    public abstract class ObjectInfo
    {
        public ObjectInfo(int _key)
        {
            this.dicKey = _key;
            this.changeAmount = new ChangeAmount();
            this.treeState = TreeNodeObject.TreeState.Close;
            this.visible = true;
        }

        public int dicKey { get; private set; }

        public abstract int kind { get; }

        public ChangeAmount changeAmount { get; protected set; }

        public TreeNodeObject.TreeState treeState { get; set; }

        public bool visible { get; set; }

        public virtual void Save(BinaryWriter _writer, Version _version)
        {
            _writer.Write(this.kind);
            _writer.Write(this.dicKey);
            this.changeAmount.Save(_writer);
            _writer.Write((int)this.treeState);
            _writer.Write(this.visible);
        }

        public virtual void Load(BinaryReader _reader, Version _version, bool _import, bool _other = true)
        {
            if (!_import)
                this.dicKey = _reader.ReadInt32();
            else
                _reader.ReadInt32();
            this.changeAmount.Load(_reader);
            if (this.dicKey != -1)
            {
                int num = _import ? 1 : 0;
            }
            if (_other && _version.CompareTo(new Version(1, 0, 1)) >= 0)
                this.treeState = (TreeNodeObject.TreeState)_reader.ReadInt32();
            if (!_other || _version.CompareTo(new Version(1, 0, 2)) < 0)
                return;
            this.visible = _reader.ReadBoolean();
        }
    }
}

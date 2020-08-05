using System;
using System.Collections.Generic;
using System.IO;

namespace StudioNeoV2
{
    public abstract class ObjectInfo
    {
        public int dicKey { get; private set; }

        public abstract int kind { get; }

        public ChangeAmount changeAmount { get; protected set; }

        public TreeNodeObject.TreeState treeState { get; set; }

        public bool visible { get; set; }

        public virtual int[] kinds
        {
            get
            {
                return new int[1] { this.kind };
            }
        }

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
                this.dicKey = Studio.SetNewIndex(_reader.ReadInt32());
            else
                _reader.ReadInt32();
            this.changeAmount.Load(_reader);
            if (_other)
                this.treeState = (TreeNodeObject.TreeState)_reader.ReadInt32();
            if (!_other)
                return;
            this.visible = _reader.ReadBoolean();
        }

        public ObjectInfo(int _key)
        {
            this.dicKey = _key;
            this.changeAmount = new ChangeAmount();
            this.treeState = TreeNodeObject.TreeState.Close;
            this.visible = true;
        }
    }
}

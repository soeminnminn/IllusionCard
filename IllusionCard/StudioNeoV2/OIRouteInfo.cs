using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioNeoV2
{
    public class OIRouteInfo : ObjectInfo
    {
        public string name = "";
        public bool loop = true;
        public bool visibleLine = true;
        public Color color = Color.blue;
        public bool active;
        public Orient orient;

        public override int kind
        {
            get
            {
                return 4;
            }
        }

        public List<ObjectInfo> child { get; private set; }

        public List<OIRoutePointInfo> route { get; private set; }

        public override void Save(BinaryWriter _writer, Version _version)
        {
            base.Save(_writer, _version);
            _writer.Write(this.name);
            int count1 = this.child.Count;
            _writer.Write(count1);
            for (int index = 0; index < count1; ++index)
                this.child[index].Save(_writer, _version);
            int count2 = this.route.Count;
            _writer.Write(count2);
            for (int index = 0; index < count2; ++index)
                this.route[index].Save(_writer, _version);
            _writer.Write(this.active);
            _writer.Write(this.loop);
            _writer.Write(this.visibleLine);
            _writer.Write((int)this.orient);
            _writer.Write(JsonUtility.ToJson(color));
        }

        public override void Load(BinaryReader _reader, Version _version, bool _import, bool _tree = true)
        {
            base.Load(_reader, _version, _import, true);
            this.name = _reader.ReadString();
            ObjectInfoAssist.LoadChild(_reader, _version, this.child, _import);
            int num = _reader.ReadInt32();
            for (int index = 0; index < num; ++index)
            {
                OIRoutePointInfo oiRoutePointInfo = new OIRoutePointInfo(-1);
                oiRoutePointInfo.Load(_reader, _version, false, true);
                this.route.Add(oiRoutePointInfo);
            }
            this.active = _reader.ReadBoolean();
            this.loop = _reader.ReadBoolean();
            this.visibleLine = _reader.ReadBoolean();
            this.orient = (Orient)_reader.ReadInt32();
            this.color = JsonUtility.FromJson<Color>(_reader.ReadString());
        }

        public OIRouteInfo(int _key)
          : base(_key)
        {
            this.name = "ルート";
            this.treeState = TreeNodeObject.TreeState.Open;
            this.child = new List<ObjectInfo>();
            this.route = new List<OIRoutePointInfo>();
        }

        public enum Orient
        {
            None,
            XY,
            Y,
        }

        public enum Connection
        {
            Line,
            Curve,
        }
    }
}

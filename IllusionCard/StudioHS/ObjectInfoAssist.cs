// Decompiled with JetBrains decompiler
// Type: StudioHS.ObjectInfoAssist
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace StudioHS
{
    public static class ObjectInfoAssist
    {
        public static void LoadChild(BinaryReader _reader, Version _version, List<ObjectInfo> _list, bool _import)
        {
            int num = _reader.ReadInt32();
            for (int index = 0; index < num; ++index)
            {
                switch (_reader.ReadInt32())
                {
                    case 0:
                        OICharInfo oiCharInfo = new OICharInfo(null, -1);
                        oiCharInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiCharInfo);
                        break;
                    case 1:
                        OIItemInfo oiItemInfo = new OIItemInfo(-1, -1);
                        oiItemInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiItemInfo);
                        break;
                    case 2:
                        OILightInfo oiLightInfo = new OILightInfo(-1, -1);
                        oiLightInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiLightInfo);
                        break;
                    case 3:
                        OIFolderInfo oiFolderInfo = new OIFolderInfo(-1);
                        oiFolderInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiFolderInfo);
                        break;
                    case 4:
                        OIPathMoveInfo oiPathMoveInfo = new OIPathMoveInfo(-1);
                        oiPathMoveInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiPathMoveInfo);
                        break;
                }
            }
        }
    }
}

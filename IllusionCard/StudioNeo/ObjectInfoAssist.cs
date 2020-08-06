using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioNeo
{
    public abstract class ObjectInfoAssist
    {
        public static void LoadChild(BinaryReader _reader, Version _version, List<ObjectInfo> _list, bool _import)
        {
            int num1 = _reader.ReadInt32();
            for (int index = 0; index < num1; ++index)
            {
                int num2 = _reader.ReadInt32();
                switch (num2)
                {
                    case 0:
                        OICharInfo oiCharInfo = new OICharInfo(null, _import ? Studio.GetNewIndex() : -1);
                        oiCharInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiCharInfo);
                        break;
                    case 1:
                        OIItemInfo oiItemInfo = new OIItemInfo(-1, _import ? Studio.GetNewIndex() : -1);
                        oiItemInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiItemInfo);
                        break;
                    case 2:
                        OILightInfo oiLightInfo = new OILightInfo(-1, _import ? Studio.GetNewIndex() : -1);
                        oiLightInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiLightInfo);
                        break;
                    case 3:
                        OIFolderInfo oiFolderInfo = new OIFolderInfo(_import ? Studio.GetNewIndex() : -1);
                        oiFolderInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiFolderInfo);
                        break;
                    case 4:
                        OIPathMoveInfo oiPathMoveInfo = new OIPathMoveInfo(_import ? Studio.GetNewIndex() : -1);
                        oiPathMoveInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiPathMoveInfo);
                        break;
                    default:
                        Debug.LogWarning(string.Format("おかしい情報が入っている : {0}", num2));
                        break;
                }
            }
        }
    }
}

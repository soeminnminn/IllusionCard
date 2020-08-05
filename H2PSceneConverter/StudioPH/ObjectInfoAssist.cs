using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioPH
{
    public static class ObjectInfoAssist
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
                        _list.Add(oiCharInfo);
                        break;
                    case 1:
                        OIItemInfo oiItemInfo = new OIItemInfo(-1, _import ? Studio.GetNewIndex() : -1);
                        oiItemInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiItemInfo);
                        break;
                    case 2:
                        OILightInfo oiLightInfo = new OILightInfo(-1, _import ? Studio.GetNewIndex() : -1);
                        oiLightInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiLightInfo);
                        break;
                    case 3:
                        OIFolderInfo oiFolderInfo = new OIFolderInfo(_import ? Studio.GetNewIndex() : -1);
                        oiFolderInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiFolderInfo);
                        break;
                    default:
                        Debug.LogWarning(string.Format("おかしい情報が入っている : {0}", num2));
                        break;
                }
            }
        }
    }
}

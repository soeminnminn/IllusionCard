using System;
using System.Collections.Generic;
using System.IO;

namespace StudioKK
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
                        OICharInfo oiCharInfo = new OICharInfo(null, _import ? Studio.GetNewIndex() : -1);
                        oiCharInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiCharInfo);
                        break;
                    case 1:
                        OIItemInfo oiItemInfo = new OIItemInfo(-1, -1, -1, _import ? Studio.GetNewIndex() : -1);
                        oiItemInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiItemInfo);
                        break;
                    case 2:
                        OILightInfo oiLightInfo = new OILightInfo(-1, _import ? Studio.GetNewIndex() : -1);
                        oiLightInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiLightInfo);
                        break;
                    case 3:
                        OIFolderInfo oiFolderInfo = new OIFolderInfo(_import ? Studio.GetNewIndex() : -1);
                        oiFolderInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiFolderInfo);
                        break;
                    case 4:
                        OIRouteInfo oiRouteInfo = new OIRouteInfo(_import ? Studio.GetNewIndex() : -1);
                        oiRouteInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiRouteInfo);
                        break;
                    case 5:
                        OICameraInfo oiCameraInfo = new OICameraInfo(_import ? Studio.GetNewIndex() : -1);
                        oiCameraInfo.Load(_reader, _version, _import, true);
                        _list.Add((ObjectInfo)oiCameraInfo);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

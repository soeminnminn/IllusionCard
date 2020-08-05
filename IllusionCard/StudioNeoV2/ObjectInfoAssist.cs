using System;
using System.Collections.Generic;
using System.IO;

namespace StudioNeoV2
{
    public static class ObjectInfoAssist
    {
        public static void LoadChild(
          BinaryReader _reader,
          Version _version,
          List<ObjectInfo> _list,
          bool _import)
        {
            int num = _reader.ReadInt32();
            for (int index = 0; index < num; ++index)
            {
                switch (_reader.ReadInt32())
                {
                    case 0:
                        OICharInfo oiCharInfo = new OICharInfo(null, !_import ? -1 : Studio.GetNewIndex());
                        oiCharInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiCharInfo);
                        break;
                    case 1:
                        OIItemInfo oiItemInfo = new OIItemInfo(-1, -1, -1, !_import ? -1 : Studio.GetNewIndex());
                        oiItemInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiItemInfo);
                        break;
                    case 2:
                        OILightInfo oiLightInfo = new OILightInfo(-1, !_import ? -1 : Studio.GetNewIndex());
                        oiLightInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiLightInfo);
                        break;
                    case 3:
                        OIFolderInfo oiFolderInfo = new OIFolderInfo(!_import ? -1 : Studio.GetNewIndex());
                        oiFolderInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiFolderInfo);
                        break;
                    case 4:
                        OIRouteInfo oiRouteInfo = new OIRouteInfo(!_import ? -1 : Studio.GetNewIndex());
                        oiRouteInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiRouteInfo);
                        break;
                    case 5:
                        OICameraInfo oiCameraInfo = new OICameraInfo(!_import ? -1 : Studio.GetNewIndex());
                        oiCameraInfo.Load(_reader, _version, _import, true);
                        _list.Add(oiCameraInfo);
                        break;
                }
            }
        }

        private static void FindLoop(ref List<ObjectInfo> _list, ObjectInfo _src, int _kind)
        {
            if (_src == null)
                return;
            if (_src.kind == _kind)
                _list.Add(_src);
            switch (_src.kind)
            {
                case 0:
                    using (Dictionary<int, List<ObjectInfo>>.Enumerator enumerator = (_src as OICharInfo).child.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            foreach (ObjectInfo _src1 in enumerator.Current.Value)
                                ObjectInfoAssist.FindLoop(ref _list, _src1, _kind);
                        }
                        break;
                    }
                case 1:
                    using (List<ObjectInfo>.Enumerator enumerator = (_src as OIItemInfo).child.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ObjectInfo current = enumerator.Current;
                            ObjectInfoAssist.FindLoop(ref _list, current, _kind);
                        }
                        break;
                    }
                case 3:
                    using (List<ObjectInfo>.Enumerator enumerator = (_src as OIFolderInfo).child.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ObjectInfo current = enumerator.Current;
                            ObjectInfoAssist.FindLoop(ref _list, current, _kind);
                        }
                        break;
                    }
                case 4:
                    using (List<ObjectInfo>.Enumerator enumerator = (_src as OIRouteInfo).child.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ObjectInfo current = enumerator.Current;
                            ObjectInfoAssist.FindLoop(ref _list, current, _kind);
                        }
                        break;
                    }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

namespace StudioKK
{
    public class SceneInfo
    {
        private readonly Version m_Version = new Version(1, 0, 4, 2);
        public ChangeAmount caMap = new ChangeAmount();
        public CameraLightCtrl.LightInfo charaLight = new CameraLightCtrl.LightInfo();
        public CameraLightCtrl.MapLightInfo mapLight = new CameraLightCtrl.MapLightInfo();
        public BGMCtrl bgmCtrl = new BGMCtrl();
        public ENVCtrl envCtrl = new ENVCtrl();
        public OutsideSoundCtrl outsideSoundCtrl = new OutsideSoundCtrl();
        public string background = string.Empty;
        public string frame = string.Empty;
        public Dictionary<int, ObjectInfo> dicObject;
        public int map;
        public int sunLightType;
        public bool mapOption;
        public int aceNo;
        public float aceBlend;
        public bool enableAOE;
        public Color aoeColor;
        public float aoeRadius;
        public bool enableBloom;
        public float bloomIntensity;
        public float bloomBlur;
        public float bloomThreshold;
        public bool enableDepth;
        public float depthFocalSize;
        public float depthAperture;
        public bool enableVignette;
        public bool enableFog;
        public Color fogColor;
        public float fogHeight;
        public float fogStartDistance;
        public bool enableSunShafts;
        public Color sunThresholdColor;
        public Color sunColor;
        public int sunCaster;
        public bool enableShadow;
        public bool faceNormal;
        public bool faceShadow;
        public float lineColorG;
        public Color ambientShadow;
        public float lineWidthG;
        public int rampG;
        public float ambientShadowG;
        public CameraControl.CameraData cameraSaveData;
        public CameraControl.CameraData[] cameraData;
        private HashSet<int> hashIndex;
        private int lightCount;

        public SceneInfo()
        {
            this.dicObject = new Dictionary<int, ObjectInfo>();
            this.cameraData = new CameraControl.CameraData[10];
            for (int index = 0; index < this.cameraData.Length; ++index)
                this.cameraData[index] = new CameraControl.CameraData();
            this.hashIndex = new HashSet<int>();
            this.Init();
        }

        public Version version
        {
            get
            {
                return this.m_Version;
            }
        }

        public Dictionary<int, ObjectInfo> dicImport { get; private set; }

        public Dictionary<int, int> dicChangeKey { get; private set; }

        public bool isLightCheck
        {
            get
            {
                return this.lightCount < 2;
            }
        }

        public bool isLightLimitOver
        {
            get
            {
                return this.lightCount > 2;
            }
        }

        public Version dataVersion { get; set; }

        public void Init()
        {
            this.dicObject.Clear();
            this.map = -1;
            this.caMap.Reset();
            this.sunLightType = 0;
            this.mapOption = true;
            this.aceNo = 0;
            this.aceBlend = 0.0f;
            this.enableAOE = true;
            this.aoeColor = Utility.ConvertColor(180, 180, 180);
            this.aoeRadius = 0.1f;
            this.enableBloom = true;
            this.bloomIntensity = 0.4f;
            this.bloomThreshold = 0.6f;
            this.bloomBlur = 0.8f;
            this.enableDepth = false;
            this.depthFocalSize = 0.95f;
            this.depthAperture = 0.6f;
            this.enableVignette = true;
            this.enableFog = false;
            this.fogColor = Utility.ConvertColor(137, 193, 221);
            this.fogHeight = 1f;
            this.fogStartDistance = 0.0f;
            this.enableSunShafts = false;
            this.sunThresholdColor = Utility.ConvertColor(128, 128, 128);
            this.sunColor = Utility.ConvertColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);
            this.sunCaster = -1;
            this.enableShadow = true;
            this.faceNormal = false;
            this.faceShadow = false;
            this.lineColorG = 1f;
            this.ambientShadow = Utility.ConvertColor(128, 128, 128);
            this.lineWidthG = 0.307f;
            this.rampG = 1;
            this.ambientShadowG = 0.26f;
            this.cameraSaveData = null;
            this.cameraData = new CameraControl.CameraData[10];
            for (int index = 0; index < 10; ++index)
                this.cameraData[index] = new CameraControl.CameraData();
            this.charaLight.Init();
            this.mapLight.Init();
            this.bgmCtrl.play = false;
            this.bgmCtrl.repeat = BGMCtrl.Repeat.All;
            this.bgmCtrl.no = 0;
            this.envCtrl.play = false;
            this.envCtrl.repeat = BGMCtrl.Repeat.All;
            this.envCtrl.no = 0;
            this.outsideSoundCtrl.play = false;
            this.outsideSoundCtrl.repeat = BGMCtrl.Repeat.All;
            this.outsideSoundCtrl.fileName = string.Empty;
            this.background = string.Empty;
            this.frame = string.Empty;
            this.hashIndex.Clear();
            this.lightCount = 0;
            this.dataVersion = this.m_Version;
        }

        public int GetNewIndex()
        {
            for (int n = 0; MathfEx.RangeEqualOn(0, n, int.MaxValue); ++n)
            {
                if (!this.hashIndex.Contains(n))
                {
                    this.hashIndex.Add(n);
                    return n;
                }
            }
            return -1;
        }

        public int CheckNewIndex()
        {
            for (int n = -1; MathfEx.RangeEqualOn(0, n, int.MaxValue); ++n)
            {
                if (!this.hashIndex.Contains(n))
                    return n;
            }
            return -1;
        }

        public bool SetNewIndex(int _index)
        {
            return this.hashIndex.Add(_index);
        }

        public bool DeleteIndex(int _index)
        {
            return this.hashIndex.Remove(_index);
        }

        public void AddLight()
        {
            ++this.lightCount;
        }

        public void DeleteLight()
        {
            --this.lightCount;
        }

        public bool Save(string _path)
        {
            using (FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter _writer = new BinaryWriter(fileStream))
                {
                    byte[] pngScreen = PngAssist.CreatePngScreen(320, 180);
                    _writer.Write(pngScreen);
                    _writer.Write(this.m_Version.ToString());
                    this.Save(_writer, this.dicObject);
                    _writer.Write(this.map);
                    this.caMap.Save(_writer);
                    _writer.Write(this.sunLightType);
                    _writer.Write(this.mapOption);
                    _writer.Write(this.aceNo);
                    _writer.Write(this.aceBlend);
                    _writer.Write(this.enableAOE);
                    _writer.Write(JsonUtility.ToJson(aoeColor));
                    _writer.Write(this.aoeRadius);
                    _writer.Write(this.enableBloom);
                    _writer.Write(this.bloomIntensity);
                    _writer.Write(this.bloomBlur);
                    _writer.Write(this.bloomThreshold);
                    _writer.Write(this.enableDepth);
                    _writer.Write(this.depthFocalSize);
                    _writer.Write(this.depthAperture);
                    _writer.Write(this.enableVignette);
                    _writer.Write(this.enableFog);
                    _writer.Write(JsonUtility.ToJson(fogColor));
                    _writer.Write(this.fogHeight);
                    _writer.Write(this.fogStartDistance);
                    _writer.Write(this.enableSunShafts);
                    _writer.Write(JsonUtility.ToJson(sunThresholdColor));
                    _writer.Write(JsonUtility.ToJson(sunColor));
                    _writer.Write(this.sunCaster);
                    _writer.Write(this.enableShadow);
                    _writer.Write(this.faceNormal);
                    _writer.Write(this.faceShadow);
                    _writer.Write(this.lineColorG);
                    _writer.Write(JsonUtility.ToJson(ambientShadow));
                    _writer.Write(this.lineWidthG);
                    _writer.Write(this.rampG);
                    _writer.Write(this.ambientShadowG);
                    this.cameraSaveData.Save(_writer);
                    for (int index = 0; index < 10; ++index)
                        this.cameraData[index].Save(_writer);
                    this.charaLight.Save(_writer, this.m_Version);
                    this.mapLight.Save(_writer, this.m_Version);
                    this.bgmCtrl.Save(_writer, this.m_Version);
                    this.envCtrl.Save(_writer, this.m_Version);
                    this.outsideSoundCtrl.Save(_writer, this.m_Version);
                    _writer.Write(this.background);
                    _writer.Write(this.frame);
                    _writer.Write("【KStudio】");
                }
            }
            return true;
        }

        public void Save(BinaryWriter _writer, Dictionary<int, ObjectInfo> _dicObject)
        {
            int count = _dicObject.Count;
            _writer.Write(count);
            foreach (KeyValuePair<int, ObjectInfo> keyValuePair in _dicObject)
            {
                _writer.Write(keyValuePair.Key);
                keyValuePair.Value.Save(_writer, this.m_Version);
            }
        }

        public bool Load(string _path)
        {
            return this.Load(_path, out Version _);
        }

        public bool Load(string _path, out Version _dataVersion)
        {
            using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    PngAssist.SkipPng(binaryReader);
                    this.dataVersion = new Version(binaryReader.ReadString());
                    int num1 = binaryReader.ReadInt32();
                    for (int index = 0; index < num1; ++index)
                    {
                        int key = binaryReader.ReadInt32();
                        int num2 = binaryReader.ReadInt32();
                        ObjectInfo objectInfo = null;
                        switch (num2)
                        {
                            case 0:
                                objectInfo = new OICharInfo(null, -1);
                                break;
                            case 1:
                                objectInfo = new OIItemInfo(-1, -1, -1, -1);
                                break;
                            case 2:
                                objectInfo = new OILightInfo(-1, -1);
                                break;
                            case 3:
                                objectInfo = new OIFolderInfo(-1);
                                break;
                            case 4:
                                objectInfo = new OIRouteInfo(-1);
                                break;
                            case 5:
                                objectInfo = new OICameraInfo(-1);
                                break;
                        }
                        objectInfo.Load(binaryReader, this.dataVersion, false, true);
                        this.dicObject.Add(key, objectInfo);
                        this.hashIndex.Add(key);
                    }
                    this.map = binaryReader.ReadInt32();
                    this.caMap.Load(binaryReader);
                    this.sunLightType = binaryReader.ReadInt32();
                    this.mapOption = binaryReader.ReadBoolean();
                    this.aceNo = binaryReader.ReadInt32();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 2)) >= 0)
                        this.aceBlend = binaryReader.ReadSingle();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 1)) <= 0)
                    {
                        binaryReader.ReadBoolean();
                        double num2 = binaryReader.ReadSingle();
                        binaryReader.ReadString();
                    }
                    if (this.dataVersion.CompareTo(new Version(0, 0, 2)) >= 0)
                    {
                        this.enableAOE = binaryReader.ReadBoolean();
                        this.aoeColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                        this.aoeRadius = binaryReader.ReadSingle();
                    }
                    this.enableBloom = binaryReader.ReadBoolean();
                    this.bloomIntensity = binaryReader.ReadSingle();
                    this.bloomBlur = binaryReader.ReadSingle();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 2)) >= 0)
                        this.bloomThreshold = binaryReader.ReadSingle();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 1)) <= 0)
                        binaryReader.ReadBoolean();
                    this.enableDepth = binaryReader.ReadBoolean();
                    this.depthFocalSize = binaryReader.ReadSingle();
                    this.depthAperture = binaryReader.ReadSingle();
                    this.enableVignette = binaryReader.ReadBoolean();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 1)) <= 0)
                    {
                        double num3 = binaryReader.ReadSingle();
                    }
                    this.enableFog = binaryReader.ReadBoolean();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 2)) >= 0)
                    {
                        this.fogColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                        this.fogHeight = binaryReader.ReadSingle();
                        this.fogStartDistance = binaryReader.ReadSingle();
                    }
                    this.enableSunShafts = binaryReader.ReadBoolean();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 2)) >= 0)
                    {
                        this.sunThresholdColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                        this.sunColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    }
                    if (this.dataVersion.CompareTo(new Version(0, 0, 4)) >= 0)
                        this.sunCaster = binaryReader.ReadInt32();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 2)) >= 0)
                        this.enableShadow = binaryReader.ReadBoolean();
                    if (this.dataVersion.CompareTo(new Version(0, 0, 4)) >= 0)
                    {
                        this.faceNormal = binaryReader.ReadBoolean();
                        this.faceShadow = binaryReader.ReadBoolean();
                        this.lineColorG = binaryReader.ReadSingle();
                        this.ambientShadow = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    }
                    if (this.dataVersion.CompareTo(new Version(0, 0, 5)) >= 0)
                    {
                        this.lineWidthG = binaryReader.ReadSingle();
                        this.rampG = binaryReader.ReadInt32();
                        this.ambientShadowG = binaryReader.ReadSingle();
                    }
                    if (this.cameraSaveData == null)
                        this.cameraSaveData = new CameraControl.CameraData();
                    this.cameraSaveData.Load(binaryReader);
                    for (int index = 0; index < 10; ++index)
                    {
                        CameraControl.CameraData cameraData = new CameraControl.CameraData();
                        cameraData.Load(binaryReader);
                        this.cameraData[index] = cameraData;
                    }
                    this.charaLight.Load(binaryReader, this.dataVersion);
                    this.mapLight.Load(binaryReader, this.dataVersion);
                    this.bgmCtrl.Load(binaryReader, this.dataVersion);
                    this.envCtrl.Load(binaryReader, this.dataVersion);
                    this.outsideSoundCtrl.Load(binaryReader, this.dataVersion);
                    this.background = binaryReader.ReadString();
                    this.frame = binaryReader.ReadString();
                    _dataVersion = this.dataVersion;
                }
            }
            return true;
        }

        public bool Import(string _path)
        {
            using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    PngAssist.SkipPng(binaryReader);
                    Version _version = new Version(binaryReader.ReadString());
                    this.Import(binaryReader, _version);
                }
            }
            return true;
        }

        public void Import(BinaryReader _reader, Version _version)
        {
            this.dicImport = new Dictionary<int, ObjectInfo>();
            this.dicChangeKey = new Dictionary<int, int>();
            int num1 = _reader.ReadInt32();
            for (int index = 0; index < num1; ++index)
            {
                int num2 = _reader.ReadInt32();
                int num3 = _reader.ReadInt32();
                ObjectInfo objectInfo = null;
                bool flag = false;
                switch (num3)
                {
                    case 0:
                        objectInfo = new OICharInfo(null, Studio.GetNewIndex());
                        break;
                    case 1:
                        objectInfo = new OIItemInfo(-1, -1, -1, Studio.GetNewIndex());
                        break;
                    case 2:
                        flag = !this.isLightCheck;
                        objectInfo = new OILightInfo(-1, Studio.GetNewIndex());
                        break;
                    case 3:
                        objectInfo = new OIFolderInfo(Studio.GetNewIndex());
                        break;
                    case 4:
                        objectInfo = new OIRouteInfo(Studio.GetNewIndex());
                        break;
                    case 5:
                        objectInfo = new OICameraInfo(Studio.GetNewIndex());
                        break;
                }
                objectInfo.Load(_reader, _version, true, true);
                if (!flag)
                {
                    this.dicObject.Add(objectInfo.dicKey, objectInfo);
                    this.dicImport.Add(objectInfo.dicKey, objectInfo);
                    this.dicChangeKey.Add(objectInfo.dicKey, num2);
                }
            }
        }
    }
}

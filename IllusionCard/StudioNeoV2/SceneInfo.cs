using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;

namespace StudioNeoV2
{
    public class SceneInfo
    {
        private readonly Version m_Version = new Version(1, 1, 1);

        public MapInfo mapInfo = new MapInfo();
        public SkyInfo skyInfo = new SkyInfo();
        public CameraLightCtrl.LightInfo charaLight = new CameraLightCtrl.LightInfo();
        public CameraLightCtrl.MapLightInfo mapLight = new CameraLightCtrl.MapLightInfo();
        public BGMCtrl bgmCtrl = new BGMCtrl();
        public ENVCtrl envCtrl = new ENVCtrl();
        public OutsideSoundCtrl outsideSoundCtrl = new OutsideSoundCtrl();
        public string background = "";
        public string frame = "";
        public Dictionary<int, ObjectInfo> dicObject;
        public int cgLookupTexture;
        public float cgBlend;
        public int cgSaturation;
        public int cgBrightness;
        public int cgContrast;
        public bool enableAmbientOcclusion;
        public float aoIntensity;
        public float aoThicknessModeifier;
        public Color aoColor;
        public bool enableBloom;
        public float bloomIntensity;
        public float bloomThreshold;
        public float bloomSoftKnee;
        public bool bloomClamp;
        public float bloomDiffusion;
        public Color bloomColor;
        public bool enableDepth;
        public int depthForcus;
        public float depthFocalSize;
        public float depthAperture;
        public bool enableVignette;
        public float vignetteIntensity;
        public bool enableSSR;
        public bool enableReflectionProbe;
        public int reflectionProbeCubemap;
        public float reflectionProbeIntensity;
        public bool enableFog;
        public bool fogExcludeFarPixels;
        public float fogHeight;
        public float fogHeightDensity;
        public Color fogColor;
        public float fogDensity;
        public bool enableSunShafts;
        public int sunCaster;
        public Color sunThresholdColor;
        public Color sunColor;
        public float sunDistanceFallOff;
        public float sunBlurSize;
        public float sunIntensity;
        public bool enableShadow;
        public Color environmentLightingSkyColor;
        public Color environmentLightingEquatorColor;
        public Color environmentLightingGroundColor;
        public CameraControl.CameraData cameraSaveData;
        public CameraControl.CameraData[] cameraData;
        private HashSet<int> hashIndex;
        private int lightCount;

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

        public SceneInfo()
        {
            this.dicObject = new Dictionary<int, ObjectInfo>();
            if (this.mapInfo == null)
                this.mapInfo = new MapInfo();
            this.cameraData = new CameraControl.CameraData[10];
            for (int index = 0; index < this.cameraData.Length; ++index)
                this.cameraData[index] = new CameraControl.CameraData();
            this.hashIndex = new HashSet<int>();
            this.Init();
        }

        public void Init()
        {
            this.dicObject.Clear();
            this.mapInfo.no = -1;
            this.mapInfo.ca.Reset();
            this.mapInfo.option = true;
            this.mapInfo.light = true;
            this.cgLookupTexture = ScreenEffectDefine.ColorGradingLookupTexture;
            this.cgBlend = ScreenEffectDefine.ColorGradingBlend;
            this.cgSaturation = ScreenEffectDefine.ColorGradingSaturation;
            this.cgBrightness = ScreenEffectDefine.ColorGradingBrightness;
            this.cgContrast = ScreenEffectDefine.ColorGradingContrast;
            this.enableAmbientOcclusion = ScreenEffectDefine.AmbientOcclusion;
            this.aoIntensity = ScreenEffectDefine.AmbientOcclusionIntensity;
            this.aoThicknessModeifier = ScreenEffectDefine.AmbientOcclusionThicknessModeifier;
            this.aoColor = ScreenEffectDefine.AmbientOcclusionColor;
            this.enableBloom = ScreenEffectDefine.Bloom;
            this.bloomIntensity = ScreenEffectDefine.BloomIntensity;
            this.bloomThreshold = ScreenEffectDefine.BloomThreshold;
            this.bloomSoftKnee = ScreenEffectDefine.BloomSoftKnee;
            this.bloomClamp = ScreenEffectDefine.BloomClamp;
            this.bloomDiffusion = ScreenEffectDefine.BloomDiffusion;
            this.bloomColor = ScreenEffectDefine.BloomColor;
            this.enableDepth = ScreenEffectDefine.DepthOfField;
            this.depthForcus = ScreenEffectDefine.DepthOfFieldForcus;
            this.depthFocalSize = ScreenEffectDefine.DepthOfFieldFocalSize;
            this.depthAperture = ScreenEffectDefine.DepthOfFieldAperture;
            this.enableVignette = ScreenEffectDefine.Vignette;
            this.vignetteIntensity = ScreenEffectDefine.VignetteIntensity;
            this.enableSSR = ScreenEffectDefine.ScreenSpaceReflections;
            this.enableReflectionProbe = ScreenEffectDefine.ReflectionProbe;
            this.reflectionProbeCubemap = ScreenEffectDefine.ReflectionProbeCubemap;
            this.reflectionProbeIntensity = ScreenEffectDefine.ReflectionProbeIntensity;
            this.enableFog = ScreenEffectDefine.Fog;
            this.fogExcludeFarPixels = ScreenEffectDefine.FogExcludeFarPixels;
            this.fogHeight = ScreenEffectDefine.FogHeight;
            this.fogHeightDensity = ScreenEffectDefine.FogHeightDensity;
            this.fogColor = ScreenEffectDefine.FogColor;
            this.fogDensity = ScreenEffectDefine.FogDensity;
            this.enableSunShafts = ScreenEffectDefine.SunShaft;
            this.sunCaster = ScreenEffectDefine.SunShaftCaster;
            this.sunThresholdColor = ScreenEffectDefine.SunShaftThresholdColor;
            this.sunColor = ScreenEffectDefine.SunShaftShaftsColor;
            this.sunDistanceFallOff = ScreenEffectDefine.SunShaftDistanceFallOff;
            this.sunBlurSize = ScreenEffectDefine.SunShaftBlurSize;
            this.sunIntensity = ScreenEffectDefine.SunShaftIntensity;
            this.enableShadow = true;
            this.environmentLightingSkyColor = ScreenEffectDefine.EnvironmentLightingSkyColor;
            this.environmentLightingEquatorColor = ScreenEffectDefine.EnvironmentLightingEquatorColor;
            this.environmentLightingGroundColor = ScreenEffectDefine.EnvironmentLightingGroundColor;
            this.skyInfo.Enable = false;
            this.skyInfo.Pattern = 0;
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
            this.outsideSoundCtrl.fileName = "";
            this.background = "";
            this.frame = "";
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
                    _writer.Write(this.mapInfo.no);
                    this.mapInfo.ca.Save(_writer);
                    _writer.Write(this.mapInfo.option);
                    _writer.Write(this.mapInfo.light);
                    _writer.Write(this.cgLookupTexture);
                    _writer.Write(this.cgBlend);
                    _writer.Write(this.cgSaturation);
                    _writer.Write(this.cgBrightness);
                    _writer.Write(this.cgContrast);
                    _writer.Write(this.enableAmbientOcclusion);
                    _writer.Write(this.aoIntensity);
                    _writer.Write(this.aoThicknessModeifier);
                    _writer.Write(JsonUtility.ToJson(aoColor));
                    _writer.Write(this.enableBloom);
                    _writer.Write(this.bloomIntensity);
                    _writer.Write(this.bloomThreshold);
                    _writer.Write(this.bloomSoftKnee);
                    _writer.Write(this.bloomClamp);
                    _writer.Write(this.bloomDiffusion);
                    _writer.Write(JsonUtility.ToJson(bloomColor));
                    _writer.Write(this.enableDepth);
                    _writer.Write(this.depthForcus);
                    _writer.Write(this.depthFocalSize);
                    _writer.Write(this.depthAperture);
                    _writer.Write(this.enableVignette);
                    _writer.Write(this.vignetteIntensity);
                    _writer.Write(this.enableSSR);
                    _writer.Write(this.enableReflectionProbe);
                    _writer.Write(this.reflectionProbeCubemap);
                    _writer.Write(this.reflectionProbeIntensity);
                    _writer.Write(this.enableFog);
                    _writer.Write(this.fogExcludeFarPixels);
                    _writer.Write(this.fogHeight);
                    _writer.Write(this.fogHeightDensity);
                    _writer.Write(JsonUtility.ToJson(fogColor));
                    _writer.Write(this.fogDensity);
                    _writer.Write(this.enableSunShafts);
                    _writer.Write(this.sunCaster);
                    _writer.Write(JsonUtility.ToJson(sunThresholdColor));
                    _writer.Write(JsonUtility.ToJson(sunColor));
                    _writer.Write(this.sunDistanceFallOff);
                    _writer.Write(this.sunBlurSize);
                    _writer.Write(this.sunIntensity);
                    _writer.Write(this.enableShadow);
                    _writer.Write(JsonUtility.ToJson(environmentLightingSkyColor));
                    _writer.Write(JsonUtility.ToJson(environmentLightingEquatorColor));
                    _writer.Write(JsonUtility.ToJson(environmentLightingGroundColor));
                    byte[] buffer = MessagePackSerializer.Serialize(this.skyInfo);
                    _writer.Write(buffer.Length);
                    _writer.Write(buffer);
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
                    _writer.Write("【StudioNEOV2】");
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
                    this.mapInfo.no = binaryReader.ReadInt32();
                    this.mapInfo.ca.Load(binaryReader);
                    this.mapInfo.option = binaryReader.ReadBoolean();
                    if (this.dataVersion.CompareTo(new Version(1, 1, 0)) >= 0)
                        this.mapInfo.light = binaryReader.ReadBoolean();
                    this.cgLookupTexture = binaryReader.ReadInt32();
                    this.cgBlend = binaryReader.ReadSingle();
                    this.cgSaturation = binaryReader.ReadInt32();
                    this.cgBrightness = binaryReader.ReadInt32();
                    this.cgContrast = binaryReader.ReadInt32();
                    this.enableAmbientOcclusion = binaryReader.ReadBoolean();
                    this.aoIntensity = binaryReader.ReadSingle();
                    this.aoThicknessModeifier = binaryReader.ReadSingle();
                    this.aoColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.enableBloom = binaryReader.ReadBoolean();
                    this.bloomIntensity = binaryReader.ReadSingle();
                    this.bloomThreshold = binaryReader.ReadSingle();
                    this.bloomSoftKnee = binaryReader.ReadSingle();
                    this.bloomClamp = binaryReader.ReadBoolean();
                    this.bloomDiffusion = binaryReader.ReadSingle();
                    this.bloomColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.enableDepth = binaryReader.ReadBoolean();
                    this.depthForcus = binaryReader.ReadInt32();
                    this.depthFocalSize = binaryReader.ReadSingle();
                    this.depthAperture = binaryReader.ReadSingle();
                    this.enableVignette = binaryReader.ReadBoolean();
                    if (this.dataVersion.CompareTo(new Version(1, 1, 1)) >= 0)
                        this.vignetteIntensity = binaryReader.ReadSingle();
                    this.enableSSR = binaryReader.ReadBoolean();
                    this.enableReflectionProbe = binaryReader.ReadBoolean();
                    this.reflectionProbeCubemap = binaryReader.ReadInt32();
                    this.reflectionProbeIntensity = binaryReader.ReadSingle();
                    this.enableFog = binaryReader.ReadBoolean();
                    this.fogExcludeFarPixels = binaryReader.ReadBoolean();
                    this.fogHeight = binaryReader.ReadSingle();
                    this.fogHeightDensity = binaryReader.ReadSingle();
                    this.fogColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.fogDensity = binaryReader.ReadSingle();
                    this.enableSunShafts = binaryReader.ReadBoolean();
                    this.sunCaster = binaryReader.ReadInt32();
                    this.sunThresholdColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.sunColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.sunDistanceFallOff = binaryReader.ReadSingle();
                    this.sunBlurSize = binaryReader.ReadSingle();
                    this.sunIntensity = binaryReader.ReadSingle();
                    this.enableShadow = binaryReader.ReadBoolean();
                    this.environmentLightingSkyColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.environmentLightingEquatorColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    this.environmentLightingGroundColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
                    if (this.dataVersion.CompareTo(new Version(1, 1, 0)) >= 0)
                        this.skyInfo = MessagePackSerializer.Deserialize<SkyInfo>(binaryReader.ReadBytes(binaryReader.ReadInt32()));
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
                    if (this.dataVersion.CompareTo(new Version(1, 1, 0)) <= 0 && !string.IsNullOrEmpty(this.background))
                        this.background = BackgroundListAssist.GetFilePath(this.background, false);
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
                switch (num3)
                {
                    case 0:
                        objectInfo = new OICharInfo(null, Studio.GetNewIndex());
                        break;
                    case 1:
                        objectInfo = new OIItemInfo(-1, -1, -1, Studio.GetNewIndex());
                        break;
                    case 2:
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
                this.dicObject.Add(objectInfo.dicKey, objectInfo);
                this.dicImport.Add(objectInfo.dicKey, objectInfo);
                this.dicChangeKey.Add(objectInfo.dicKey, num2);
            }
        }

        public class MapInfo
        {
            public int no = -1;
            public ChangeAmount ca = new ChangeAmount();
            public bool option = true;
            public bool light = true;
        }

        [MessagePackObject(true)]
        public class SkyInfo
        {
            public bool Enable { get; set; }

            public int Pattern { get; set; }

            public SkyInfo()
            {
                this.Enable = false;
                this.Pattern = 0;
            }
        }
    }
}

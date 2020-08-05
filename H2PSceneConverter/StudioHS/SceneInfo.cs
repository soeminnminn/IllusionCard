// Decompiled with JetBrains decompiler
// Type: StudioHS.SceneInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using CharacterHS;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioHS
{
    public class SceneInfo
    {
        private readonly Version m_Version = new Version(1, 0, 4);

        public ChangeAmount caMap = new ChangeAmount();
        public HSColorSet ssaoColor = new HSColorSet();
        public HSColorSet cameraLightColor = new HSColorSet();
        public float[] cameraLightRot = new float[2];
        public bool cameraLightShadow = true;
        public BGMCtrl bgmCtrl = new BGMCtrl();
        public ENVCtrl envCtrl = new ENVCtrl();
        public OutsideSoundCtrl outsideSoundCtrl = new OutsideSoundCtrl();
        public string background = string.Empty;
        public Dictionary<int, ObjectInfo> dicObject;
        public int map;
        public bool enableSSAO;
        public float ssaoIntensity;
        public bool enableBloom;
        public float bloomIntensity;
        public float bloomBlur;
        public bool enableSSR;
        public bool enableDepth;
        public float depthFocalSize;
        public float depthAperture;
        public bool enableVignette;
        public float vignetteVignetting;
        public bool enableFog;
        public bool enableSunShafts;
        public CameraControl.CameraData cameraSaveData;
        public CameraControl.CameraData[] cameraData;
        public float cameraLightIntensity;
        private HashSet<int> hashIndex;

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

        public void Init()
        {
            this.dicObject.Clear();
            this.map = -1;
            this.caMap.Reset();
            this.enableSSAO = true;
            this.ssaoIntensity = 1.27f;
            this.ssaoColor.SetDiffuseRGB(Utility.ConvertColor(23, 11, 11));
            this.enableBloom = true;
            this.bloomIntensity = 1.3f;
            this.bloomBlur = 1.5f;
            this.enableSSR = true;
            this.enableDepth = true;
            this.depthFocalSize = 1f;
            this.depthAperture = 0.7f;
            this.enableVignette = true;
            this.vignetteVignetting = 0.255f;
            this.enableFog = true;
            this.enableSunShafts = false;
            this.cameraSaveData = null;
            this.cameraData = new CameraControl.CameraData[10];
            this.cameraLightColor.SetDiffuseRGB(Utility.ConvertColor(220, 210, 204));
            this.cameraLightIntensity = 1f;
            this.cameraLightRot[0] = 0.0f;
            this.cameraLightRot[1] = 0.0f;
            this.cameraLightShadow = true;
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
            this.hashIndex.Clear();
        }

        public int GetNewIndex()
        {
            for (int index = 0; MathfEx.RangeEqualOn(0, index, int.MaxValue); ++index)
            {
                if (!this.hashIndex.Contains(index))
                {
                    this.hashIndex.Add(index);
                    return index;
                }
            }
            return -1;
        }

        public int CheckNewIndex()
        {
            for (int index = -1; MathfEx.RangeEqualOn(0, index, int.MaxValue); ++index)
            {
                if (!this.hashIndex.Contains(index))
                    return index;
            }
            return -1;
        }

        public bool SetNewIndex(int _index)
        {
            return this.hashIndex.Add(_index);
        }

        public void DeleteIndex(int _index)
        {
            this.hashIndex.Remove(_index);
        }

        public bool Save(string _path)
        {
            using (FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    byte[] pngScreen = PngAssist.CreatePngScreen(320, 180);
                    binaryWriter.Write(pngScreen);
                    binaryWriter.Write(this.m_Version.ToString());
                    this.Save(binaryWriter, this.dicObject);
                    binaryWriter.Write(this.map);
                    this.caMap.Save(binaryWriter);
                    binaryWriter.Write(this.enableSSAO);
                    binaryWriter.Write(this.ssaoIntensity);
                    binaryWriter.Write(1);
                    this.ssaoColor.Save(binaryWriter);
                    binaryWriter.Write(this.enableBloom);
                    binaryWriter.Write(this.bloomIntensity);
                    binaryWriter.Write(this.bloomBlur);
                    binaryWriter.Write(this.enableSSR);
                    binaryWriter.Write(this.enableDepth);
                    binaryWriter.Write(this.depthFocalSize);
                    binaryWriter.Write(this.depthAperture);
                    binaryWriter.Write(this.enableVignette);
                    binaryWriter.Write(this.vignetteVignetting);
                    binaryWriter.Write(this.enableFog);
                    binaryWriter.Write(this.enableSunShafts);
                    this.cameraSaveData.Save(binaryWriter);
                    for (int index = 0; index < 10; ++index)
                        this.cameraData[index].Save(binaryWriter);
                    this.cameraLightColor.Save(binaryWriter);
                    binaryWriter.Write(this.cameraLightIntensity);
                    binaryWriter.Write(this.cameraLightRot[0]);
                    binaryWriter.Write(this.cameraLightRot[1]);
                    binaryWriter.Write(this.cameraLightShadow);
                    this.bgmCtrl.Save(binaryWriter, this.m_Version);
                    this.envCtrl.Save(binaryWriter, this.m_Version);
                    this.outsideSoundCtrl.Save(binaryWriter, this.m_Version);
                    binaryWriter.Write(this.background);
                    binaryWriter.Write("【-neo-】");
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
            SceneInfo.logSave("l00");
            using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                SceneInfo.logSave("l01");
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    SceneInfo.logSave("l02");
                    long size = 0;
                    PngAssist.CheckPngData(binaryReader, ref size, true);
                    Version _version = new Version(binaryReader.ReadString());
                    int num1 = binaryReader.ReadInt32();
                    for (int index = 0; index < num1; ++index)
                    {
                        SceneInfo.logSave("l03");
                        int key = binaryReader.ReadInt32();
                        int num2 = binaryReader.ReadInt32();
                        ObjectInfo objectInfo = null;
                        switch (num2)
                        {
                            case 0:
                                SceneInfo.logSave("l030");
                                objectInfo = new OICharInfo(null, -1);
                                break;
                            case 1:
                                SceneInfo.logSave("l031");
                                objectInfo = new OIItemInfo(-1, -1);
                                break;
                            case 2:
                                SceneInfo.logSave("l032");
                                objectInfo = new OILightInfo(-1, -1);
                                break;
                            case 3:
                                SceneInfo.logSave("l033");
                                objectInfo = new OIFolderInfo(-1);
                                break;
                            default:
                                SceneInfo.logSave("l04" + num2);
                                break;
                        }
                        SceneInfo.logSave("l04");
                        objectInfo.Load(binaryReader, _version, false, true);
                        SceneInfo.logSave("l05");
                        this.dicObject.Add(key, objectInfo);
                        this.hashIndex.Add(key);
                    }
                    this.map = binaryReader.ReadInt32();
                    if (_version.CompareTo(new Version(1, 0, 3)) >= 0)
                        this.caMap.Load(binaryReader);
                    this.enableSSAO = binaryReader.ReadBoolean();
                    this.ssaoIntensity = binaryReader.ReadSingle();
                    int version = binaryReader.ReadInt32();
                    this.ssaoColor.Load(binaryReader, version);
                    this.enableBloom = binaryReader.ReadBoolean();
                    this.bloomIntensity = binaryReader.ReadSingle();
                    this.bloomBlur = binaryReader.ReadSingle();
                    this.enableSSR = binaryReader.ReadBoolean();
                    this.enableDepth = binaryReader.ReadBoolean();
                    this.depthFocalSize = binaryReader.ReadSingle();
                    this.depthAperture = binaryReader.ReadSingle();
                    this.enableVignette = binaryReader.ReadBoolean();
                    this.vignetteVignetting = binaryReader.ReadSingle();
                    this.enableFog = binaryReader.ReadBoolean();
                    this.enableSunShafts = binaryReader.ReadBoolean();
                    if (this.cameraSaveData == null)
                        this.cameraSaveData = new CameraControl.CameraData();
                    this.cameraSaveData.Load(binaryReader);
                    for (int index = 0; index < 10; ++index)
                    {
                        CameraControl.CameraData cameraData = new CameraControl.CameraData();
                        cameraData.Load(binaryReader);
                        this.cameraData[index] = cameraData;
                    }
                    this.cameraLightColor.Load(binaryReader, version);
                    this.cameraLightIntensity = binaryReader.ReadSingle();
                    if (_version.CompareTo(new Version(0, 1, 3)) >= 0)
                    {
                        this.cameraLightRot[0] = binaryReader.ReadSingle();
                        this.cameraLightRot[1] = binaryReader.ReadSingle();
                    }
                    if (_version.CompareTo(new Version(1, 0, 1)) >= 0)
                        this.cameraLightShadow = binaryReader.ReadBoolean();
                    this.bgmCtrl.Load(binaryReader, _version);
                    this.envCtrl.Load(binaryReader, _version);
                    this.outsideSoundCtrl.Load(binaryReader, _version);
                    if (_version.CompareTo(new Version(1, 0, 3)) >= 0)
                        this.background = binaryReader.ReadString();
                    _dataVersion = _version;
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
                    long size = 0;
                    PngAssist.CheckPngData(binaryReader, ref size, true);
                    Version _version = new Version(binaryReader.ReadString());
                    this.Import(binaryReader, _version);
                }
            }
            return true;
        }

        public void Import(BinaryReader _reader, Version _version)
        {
        }

        public static void logSave(string txt)
        {
        }
    }
}

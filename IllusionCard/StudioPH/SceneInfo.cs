// Decompiled with JetBrains decompiler
// Type: StudioPH.SceneInfo
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioPH
{
    public class SceneInfo
    {
        protected readonly Version m_Version = new Version(1, 1, 0);
        public ChangeAmount caMap = new ChangeAmount();
        public float[] cameraLightRot = new float[2];
        public bool cameraLightShadow = true;
        public BGMCtrl bgmCtrl = new BGMCtrl();
        public ENVCtrl envCtrl = new ENVCtrl();
        public OutsideSoundCtrl outsideSoundCtrl = new OutsideSoundCtrl();
        public string background = string.Empty;
        public Dictionary<int, ObjectInfo> dicObject;
        public int map;
        public int atmosphere;
        public bool enableSSAO;
        public float ssaoIntensity;
        public float ssaoRadius;
        public Color ssaoColor;
        public bool enableBloom;
        public float bloomIntensity;
        public bool bloomDirt;
        public bool enableDepth;
        public float depthFocalSize;
        public float depthAperture;
        public bool enableVignette;
        public float vignetteVignetting;
        public bool enableEyeAdaptation;
        public float eyeAdaptationIntensity;
        public bool enableNoise;
        public float noiseIntensity;
        public CameraControl.CameraData cameraSaveData;
        public CameraControl.CameraData[] cameraData;
        public Color cameraLightColor;
        public float cameraLightIntensity;
        public int cameraMethod;
        public int skybox;
        protected HashSet<int> hashIndex;

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

        public virtual void Init()
        {
            this.dicObject.Clear();
            this.map = -1;
            this.caMap.Reset();
            this.atmosphere = 0;
            this.enableSSAO = true;
            this.ssaoIntensity = 1f;
            this.ssaoRadius = 1f;
            this.enableBloom = true;
            this.bloomIntensity = 1f;
            this.bloomDirt = false;
            this.enableDepth = false;
            this.depthFocalSize = 1f;
            this.depthAperture = 0.7f;
            this.enableVignette = true;
            this.vignetteVignetting = 1f;
            this.enableEyeAdaptation = false;
            this.eyeAdaptationIntensity = 0.5f;
            this.enableNoise = false;
            this.noiseIntensity = 1f;
            this.cameraSaveData = null;
            this.cameraData = new CameraControl.CameraData[10];
            for (int index = 0; index < 10; ++index)
                this.cameraData[index] = new CameraControl.CameraData();
            this.cameraLightIntensity = 1f;
            this.cameraLightRot[0] = 0.0f;
            this.cameraLightRot[1] = 0.0f;
            this.cameraLightShadow = true;
            this.cameraMethod = 0;
            this.bgmCtrl.play = false;
            this.bgmCtrl.no = 0;
            this.envCtrl.play = false;
            this.envCtrl.no = 0;
            this.outsideSoundCtrl.play = false;
            this.outsideSoundCtrl.fileName = string.Empty;
            this.background = string.Empty;
            this.skybox = -1;
            this.hashIndex.Clear();
        }

        public virtual int GetNewIndex()
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

        public virtual int CheckNewIndex()
        {
            for (int index = -1; MathfEx.RangeEqualOn<int>(0, index, int.MaxValue); ++index)
            {
                if (!this.hashIndex.Contains(index))
                    return index;
            }
            return -1;
        }

        public virtual bool SetNewIndex(int _index)
        {
            return this.hashIndex.Add(_index);
        }

        public virtual void DeleteIndex(int _index)
        {
            this.hashIndex.Remove(_index);
        }

        public virtual bool Save(string _path, byte[] buffer)
        {
            using (FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter _writer = new BinaryWriter(fileStream))
                {
                    _writer.Write(buffer);
                    _writer.Write(this.m_Version.ToString());
                    this.Save(_writer, this.dicObject);
                    _writer.Write(this.map);
                    this.caMap.Save(_writer);
                    _writer.Write(this.atmosphere);
                    _writer.Write(this.enableSSAO);
                    _writer.Write(this.ssaoIntensity);
                    _writer.Write(this.ssaoRadius);
                    _writer.Write(JsonUtility.ToJson(ssaoColor));
                    _writer.Write(this.enableBloom);
                    _writer.Write(this.bloomIntensity);
                    _writer.Write(this.bloomDirt);
                    _writer.Write(this.enableDepth);
                    _writer.Write(this.depthFocalSize);
                    _writer.Write(this.depthAperture);
                    _writer.Write(this.enableVignette);
                    _writer.Write(this.vignetteVignetting);
                    _writer.Write(this.enableEyeAdaptation);
                    _writer.Write(this.eyeAdaptationIntensity);
                    _writer.Write(this.enableNoise);
                    _writer.Write(this.noiseIntensity);
                    this.cameraSaveData.Save(_writer);
                    for (int index = 0; index < 10; ++index)
                        this.cameraData[index].Save(_writer);
                    _writer.Write(JsonUtility.ToJson(cameraLightColor));
                    _writer.Write(this.cameraLightIntensity);
                    _writer.Write(this.cameraLightRot[0]);
                    _writer.Write(this.cameraLightRot[1]);
                    _writer.Write(this.cameraLightShadow);
                    _writer.Write(this.cameraMethod);
                    this.bgmCtrl.Save(_writer, this.m_Version);
                    this.envCtrl.Save(_writer, this.m_Version);
                    this.outsideSoundCtrl.Save(_writer, this.m_Version);
                    _writer.Write(this.background);
                    _writer.Write(this.skybox);
                    _writer.Write("【PHStudio】");
                }
            }
            return true;
        }

        public virtual void Save(BinaryWriter _writer, Dictionary<int, ObjectInfo> _dicObject)
        {
            int count = _dicObject.Count;
            _writer.Write(count);
            foreach (KeyValuePair<int, ObjectInfo> keyValuePair in _dicObject)
            {
                _writer.Write(keyValuePair.Key);
                keyValuePair.Value.Save(_writer, this.m_Version);
            }
        }

        public virtual bool Load(string _path)
        {
            return this.Load(_path, out Version _);
        }

        public virtual bool Load(string _path, out Version _dataVersion)
        {
            using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader _reader = new BinaryReader(fileStream))
                {
                    PngAssist.SkipPng(_reader);

                    Version _version = new Version(_reader.ReadString());

                    int countInfo = _reader.ReadInt32();
                    for (int index = 0; index < countInfo; ++index)
                    {
                        int key = _reader.ReadInt32();
                        int infoType = _reader.ReadInt32();
                        ObjectInfo objectInfo = null;
                        switch (infoType)
                        {
                            case 0:
                                objectInfo = new OICharInfo(null, -1);
                                break;
                            case 1:
                                objectInfo = new OIItemInfo(-1, -1);
                                break;
                            case 2:
                                objectInfo = new OILightInfo(-1, -1);
                                break;
                            case 3:
                                objectInfo = new OIFolderInfo(-1);
                                break;
                        }

                        objectInfo.Load(_reader, _version, false, true);
                        this.dicObject.Add(key, objectInfo);
                        this.hashIndex.Add(key);
                    }

                    this.map = _reader.ReadInt32();
                    this.caMap.Load(_reader);

                    if (_version.CompareTo(new Version(0, 1, 3)) >= 0)
                        this.atmosphere = _reader.ReadInt32();

                    this.enableSSAO = _reader.ReadBoolean();
                    this.ssaoIntensity = _reader.ReadSingle();
                    this.ssaoRadius = _reader.ReadSingle();
                    this.ssaoColor = JsonUtility.FromJson<Color>(_reader.ReadString());
                    this.enableBloom = _reader.ReadBoolean();
                    this.bloomIntensity = _reader.ReadSingle();
                    this.bloomDirt = _reader.ReadBoolean();
                    this.enableDepth = _reader.ReadBoolean();
                    this.depthFocalSize = _reader.ReadSingle();
                    this.depthAperture = _reader.ReadSingle();
                    this.enableVignette = _reader.ReadBoolean();
                    this.vignetteVignetting = _reader.ReadSingle();
                    this.enableEyeAdaptation = _reader.ReadBoolean();
                    this.eyeAdaptationIntensity = _reader.ReadSingle();
                    this.enableNoise = _reader.ReadBoolean();
                    this.noiseIntensity = _reader.ReadSingle();

                    if (this.cameraSaveData == null)
                        this.cameraSaveData = new CameraControl.CameraData();

                    this.cameraSaveData.Load(_reader);
                    for (int index = 0; index < 10; ++index)
                    {
                        CameraControl.CameraData cameraData = new CameraControl.CameraData();
                        cameraData.Load(_reader);
                        this.cameraData[index] = cameraData;
                    }

                    this.cameraLightColor = JsonUtility.FromJson<Color>(_reader.ReadString());
                    this.cameraLightIntensity = _reader.ReadSingle();
                    this.cameraLightRot[0] = _reader.ReadSingle();
                    this.cameraLightRot[1] = _reader.ReadSingle();
                    this.cameraLightShadow = _reader.ReadBoolean();

                    if (_version.CompareTo(new Version(1, 1, 0)) >= 0)
                        this.cameraMethod = _reader.ReadInt32();

                    this.bgmCtrl.Load(_reader, _version);
                    this.envCtrl.Load(_reader, _version);
                    this.outsideSoundCtrl.Load(_reader, _version);
                    this.background = _reader.ReadString();

                    _dataVersion = _version;
                    if (_version.CompareTo(new Version(1, 1, 0)) >= 0)
                        this.skybox = _reader.ReadInt32();
                }
            }
            return true;
        }
    }
}

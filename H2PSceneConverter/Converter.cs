// Decompiled with JetBrains decompiler
// Type: H2PConverter.Converter
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace H2PConverter
{
    internal class Converter
    {
        public static void convert(string filenameFull, string dir)
        {
            try
            {
                StudioPH.SceneInfo sceneInfoPH = new StudioPH.SceneInfo();
                sceneInfoPH.Init();

                StudioHS.SceneInfo sceneInfoHS = new StudioHS.SceneInfo();
                sceneInfoHS.Init();
                
                sceneInfoHS.Load(filenameFull);
                sceneInfoHS.background = "";
                sceneInfoPH.bgmCtrl = new StudioPH.BGMCtrl();
                sceneInfoPH.background = "";

                try
                {
                    sceneInfoPH.cameraData = new StudioPH.CameraControl.CameraData[10];
                    for (int index = 0; index < sceneInfoPH.cameraData.Length; ++index)
                        sceneInfoPH.cameraData[index] = new StudioPH.CameraControl.CameraData();
                    sceneInfoPH.cameraData.Initialize();
                    
                    int index1 = 0;
                    foreach (StudioHS.CameraControl.CameraData cameraData in sceneInfoHS.cameraData)
                    {
                        sceneInfoPH.cameraData[index1].pos = cameraData.pos;
                        sceneInfoPH.cameraData[index1].rotate = cameraData.rotate;
                        sceneInfoPH.cameraData[index1].distance = cameraData.distance;
                        sceneInfoPH.cameraData[index1].parse = cameraData.parse;
                        ++index1;
                    }
                }
                catch (Exception)
                {
                }
                
                sceneInfoPH.cameraLightColor = sceneInfoHS.cameraLightColor.rgbaDiffuse;
                sceneInfoPH.cameraLightIntensity = sceneInfoHS.cameraLightIntensity;
                sceneInfoPH.cameraLightRot = sceneInfoHS.cameraLightRot;
                sceneInfoPH.cameraLightShadow = sceneInfoHS.cameraLightShadow;
                sceneInfoPH.cameraSaveData = new StudioPH.CameraControl.CameraData();
                sceneInfoPH.cameraSaveData.pos = sceneInfoHS.cameraSaveData.pos;
                sceneInfoPH.cameraSaveData.rotate = sceneInfoHS.cameraSaveData.rotate;
                sceneInfoPH.cameraSaveData.distance = sceneInfoHS.cameraSaveData.distance;
                sceneInfoPH.depthAperture = sceneInfoHS.depthAperture;
                sceneInfoPH.depthFocalSize = sceneInfoHS.depthFocalSize;
                sceneInfoPH.enableBloom = sceneInfoHS.enableBloom;
                sceneInfoPH.enableDepth = sceneInfoHS.enableDepth;
                sceneInfoPH.enableSSAO = sceneInfoHS.enableSSAO;
                sceneInfoPH.enableVignette = false;
                sceneInfoPH.envCtrl = new StudioPH.ENVCtrl();
                sceneInfoPH.ssaoColor = sceneInfoHS.ssaoColor.rgbaDiffuse;
                sceneInfoPH.ssaoIntensity = sceneInfoHS.ssaoIntensity;
                sceneInfoPH.vignetteVignetting = sceneInfoHS.vignetteVignetting;
                sceneInfoPH.dicObject = Converter.setDict(sceneInfoHS.dicObject);
                
                byte[] png = PngAssist.CreatePngScreen(320, 180);
                sceneInfoPH.Save(dir + "\\h2p.png", png);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
            GC.Collect();
        }

        public static Dictionary<int, StudioPH.ObjectInfo> setDict(Dictionary<int, StudioHS.ObjectInfo> srcDict)
        {
            Dictionary<int, StudioPH.ObjectInfo> dictionary1 = new Dictionary<int, StudioPH.ObjectInfo>();
            Dictionary<int, StudioPH.ObjectInfo> dictionary2 = new Dictionary<int, StudioPH.ObjectInfo>();
            
            foreach (KeyValuePair<int, StudioHS.ObjectInfo> keyValuePair in srcDict)
            {
                StudioHS.ObjectInfo s = keyValuePair.Value;
                StudioPH.ObjectInfo objectInfo = Converter.copyInfo(s);
                if (objectInfo != null)
                {
                    if (s.kind != 0)
                        dictionary1.Add(s.dicKey, objectInfo);
                    else
                        dictionary2.Add(s.dicKey, objectInfo);
                }
            }

            foreach (KeyValuePair<int, StudioPH.ObjectInfo> keyValuePair in dictionary2)
                dictionary1.Add(keyValuePair.Key, keyValuePair.Value);
            
            return dictionary1;
        }

        public static StudioPH.ObjectInfo copyInfo(StudioHS.ObjectInfo s)
        {
            StudioPH.ObjectInfo objectInfo = null;
            switch (s.kind)
            {
                case 0:
                    objectInfo = Converter.copyOICharInfo((StudioHS.OICharInfo)s);
                    break;
                case 1:
                    objectInfo = Converter.copyOIItemInfo((StudioHS.OIItemInfo)s);
                    break;
                case 2:
                    objectInfo = Converter.copyOILightInfo((StudioHS.OILightInfo)s);
                    break;
                case 3:
                    objectInfo = Converter.copyOIFolderInfo((StudioHS.OIFolderInfo)s);
                    break;
            }
            return objectInfo;
        }

        public static StudioPH.ObjectInfo copyOICharInfo(StudioHS.OICharInfo oiisrc)
        {
            try
            {
                Debug.Log("save3-1");
                CharacterPH.CustomParameter customParameter = new CharacterPH.CustomParameter(0);
                
                Debug.Log("save3-2");
                int dicKey = oiisrc.dicKey;
                StudioPH.OICharInfo oiCharInfo1 = new StudioPH.OICharInfo(customParameter, dicKey); // not used
                StudioPH.OICharInfo oiCharInfo2;
                if (oiisrc.sex == 1)
                {
                    Debug.Log("save3-3");
                    oiCharInfo2 = AddObjectFemale("UesugiForH2PConverter.png");
                }
                else
                {
                    Debug.Log("save3-3-2");
                    oiCharInfo2 = AddObjectMale("MaleForH2PConverter.png");
                }
                
                Debug.Log("save3-4");
                oiCharInfo2.visible = oiisrc.visible;
                oiCharInfo2.changeAmount.pos = oiisrc.changeAmount.pos;
                oiCharInfo2.changeAmount.rot = oiisrc.changeAmount.rot;
                oiCharInfo2.changeAmount.scale = oiisrc.changeAmount.scale;
                oiCharInfo2.changeAmount.scale = new Vector3(oiCharInfo2.changeAmount.scale.x, oiCharInfo2.changeAmount.scale.x, oiCharInfo2.changeAmount.scale.x);
                
                Debug.Log("save3-5");
                foreach (KeyValuePair<int, StudioHS.OIBoneInfo> bone in oiisrc.bones)
                {
                    Debug.Log("save3-5-1");
                    oiCharInfo2.bones[bone.Key] = new StudioPH.OIBoneInfo(bone.Value.dicKey);
                    oiCharInfo2.bones[bone.Key].level = bone.Value.level;
                    oiCharInfo2.bones[bone.Key].visible = bone.Value.visible;
                    oiCharInfo2.bones[bone.Key].changeAmount.pos = bone.Value.changeAmount.pos;
                    oiCharInfo2.bones[bone.Key].changeAmount.rot = bone.Value.changeAmount.rot;
                    oiCharInfo2.bones[bone.Key].changeAmount.scale = bone.Value.changeAmount.scale;
                    oiCharInfo2.bones[bone.Key].group = (StudioPH.OIBoneInfo.BoneGroup)bone.Value.group;
                }
                
                Debug.Log("save3-6");
                foreach (KeyValuePair<int, StudioHS.OIIKTargetInfo> keyValuePair in oiisrc.ikTarget)
                {
                    Debug.Log("save3-6-1");
                    oiCharInfo2.ikTarget[keyValuePair.Key] = new StudioPH.OIIKTargetInfo(keyValuePair.Value.dicKey);
                    oiCharInfo2.ikTarget[keyValuePair.Key].level = keyValuePair.Value.level;
                    oiCharInfo2.ikTarget[keyValuePair.Key].visible = keyValuePair.Value.visible;
                    oiCharInfo2.ikTarget[keyValuePair.Key].changeAmount.pos = keyValuePair.Value.changeAmount.pos;
                    oiCharInfo2.ikTarget[keyValuePair.Key].changeAmount.rot = keyValuePair.Value.changeAmount.rot;
                    oiCharInfo2.ikTarget[keyValuePair.Key].changeAmount.scale = keyValuePair.Value.changeAmount.scale;
                }
                
                Debug.Log("save3-7");
                oiCharInfo2.kinematicMode = (StudioPH.OICharInfo.KinematicMode)oiisrc.kinematicMode;
                
                Debug.Log("save3-8|" + oiisrc.animeInfo.group + "|" + oiisrc.animeInfo.category + "|" + oiisrc.animeInfo.no);
                if (oiisrc.animeInfo.group == 0 || oiisrc.animeInfo.group == 1)
                {
                    oiCharInfo2.animeInfo.group = oiisrc.animeInfo.group + 100;
                    oiCharInfo2.animeInfo.category = oiisrc.animeInfo.category + 100;
                    oiCharInfo2.animeInfo.no = oiisrc.animeInfo.no;
                    oiCharInfo2.animeSpeed = 0.0f;
                    oiCharInfo2.isAnimeForceLoop = false;
                }
                else
                {
                    oiCharInfo2.animeSpeed = 0.0f;
                    oiCharInfo2.isAnimeForceLoop = false;
                }
                
                Debug.Log("save3-9");
                oiCharInfo2.handPtn[0] = oiisrc.handPtn[0];
                oiCharInfo2.handPtn[1] = oiisrc.handPtn[1];
                oiCharInfo2.skinRate = oiisrc.skinRate;
                oiCharInfo2.nipple = oiisrc.nipple;
                oiCharInfo2.mouthOpen = oiisrc.mouthOpen;
                oiCharInfo2.faceOption = 0;
                oiCharInfo2.lipSync = false;
                
                Debug.Log("save3-10");
                try
                {
                    if (oiCharInfo2.lookAtTarget == null)
                        oiCharInfo2.lookAtTarget = new StudioPH.LookAtTargetInfo(oiisrc.lookAtTarget.dicKey);
                    oiCharInfo2.lookAtTarget.changeAmount.pos = oiisrc.lookAtTarget.changeAmount.pos;
                    oiCharInfo2.lookAtTarget.changeAmount.rot = oiisrc.lookAtTarget.changeAmount.rot;
                    oiCharInfo2.lookAtTarget.changeAmount.scale = oiisrc.lookAtTarget.changeAmount.scale;
                    oiCharInfo2.enableIK = oiisrc.enableIK;
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }

                Debug.Log("save3-11");
                for (int index = 0; index < 5; ++index)
                    oiCharInfo2.activeIK[index] = oiisrc.activeFK[index];
                
                Debug.Log("save3-12");
                oiCharInfo2.enableFK = oiisrc.enableFK;
                
                Debug.Log("save3-13");
                for (int index = 0; index < 7; ++index)
                    oiCharInfo2.activeFK[index] = oiisrc.activeFK[index];
                
                Debug.Log("save3-14");
                for (int index = 0; index < 4; ++index)
                    oiCharInfo2.expression[index] = oiisrc.expression[index];
                
                Debug.Log("save3-15");
                try
                {
                    byte[] neckByteData = oiisrc.neckByteData;
                    
                    Debug.Log("save3-15-2");
                    for (int index = 0; index < neckByteData.Length; ++index)
                        neckByteData[index] = 0;
                    
                    Debug.Log("save3-15-3");
                    oiCharInfo2.neckByteData = neckByteData;
                    oiCharInfo2.fileStatus.neckLookPtn = oiisrc.charFile.statusInfo.neckLookPtn > 3 ? 3 : oiisrc.charFile.statusInfo.neckLookPtn;
                    oiCharInfo2.fileStatus.neckTargetNo = oiisrc.charFile.statusInfo.neckTargetNo;
                    oiCharInfo2.fileStatus.neckTargetRate = oiisrc.charFile.statusInfo.neckTargetRate;
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }

                try
                {
                    Debug.Log("save3-15-4");
                    byte[] eyesByteData = oiisrc.eyesByteData;
                    
                    Debug.Log("save3-15-5");
                    for (int index = 0; index < eyesByteData.Length; ++index)
                        eyesByteData[index] = 0;
                    
                    Debug.Log("save3-16");
                    oiCharInfo2.eyesByteData = eyesByteData;
                    oiCharInfo2.fileStatus.eyesBlink = oiisrc.charFile.statusInfo.eyesBlink;
                    oiCharInfo2.fileStatus.eyesFixed = oiisrc.charFile.statusInfo.eyesFixed;
                    oiCharInfo2.fileStatus.eyesLookPtn = oiisrc.charFile.statusInfo.eyesLookPtn;
                    oiCharInfo2.fileStatus.eyesOpen = oiisrc.charFile.statusInfo.eyesOpen;
                    oiCharInfo2.fileStatus.eyesPtn = oiisrc.charFile.statusInfo.eyesPtn;
                    oiCharInfo2.fileStatus.eyesTargetNo = oiisrc.charFile.statusInfo.eyesTargetNo;
                    oiCharInfo2.fileStatus.eyesTargetRate = oiisrc.charFile.statusInfo.eyesTargetRate;
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
                
                foreach (KeyValuePair<int, StudioHS.TreeNodeObject.TreeState> keyValuePair in oiisrc.dicAccessGroup)
                {
                    Debug.Log("save3-6-1");
                    oiCharInfo2.dicAccessGroup[keyValuePair.Key] = keyValuePair.Value == StudioHS.TreeNodeObject.TreeState.Close ? StudioPH.TreeNodeObject.TreeState.Close : StudioPH.TreeNodeObject.TreeState.Open;
                }
                
                foreach (KeyValuePair<int, StudioHS.TreeNodeObject.TreeState> keyValuePair in oiisrc.dicAccessNo)
                {
                    Debug.Log("save3-6-1");
                    oiCharInfo2.dicAccessNo[keyValuePair.Key] = keyValuePair.Value == StudioHS.TreeNodeObject.TreeState.Close ? StudioPH.TreeNodeObject.TreeState.Close : StudioPH.TreeNodeObject.TreeState.Open;
                }
                
                foreach (KeyValuePair<int, List<StudioHS.ObjectInfo>> keyValuePair in oiisrc.child)
                {
                    List<StudioPH.ObjectInfo> objectInfoList = new List<StudioPH.ObjectInfo>();
                    foreach (StudioHS.ObjectInfo s in keyValuePair.Value)
                        objectInfoList.Add(Converter.copyInfo(s));
                    oiCharInfo2.child[keyValuePair.Key] = objectInfoList;
                }
                
                StudioPH.CharFileStatus fileStatus = oiCharInfo2.fileStatus;
                fileStatus.coordinateType = 0;
                
                for (int index = 0; index < 10; ++index)
                    fileStatus.showAccessory[index] = true;
                return oiCharInfo2;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
            return null;
        }

        public static StudioPH.ObjectInfo copyOIFolderInfo(StudioHS.OIFolderInfo oiisrc)
        {
            try
            {
                StudioPH.OIFolderInfo oiFolderInfo = new StudioPH.OIFolderInfo(oiisrc.dicKey);
                oiFolderInfo.visible = oiisrc.visible;
                oiFolderInfo.changeAmount.pos = oiisrc.changeAmount.pos;
                oiFolderInfo.changeAmount.rot = oiisrc.changeAmount.rot;
                oiFolderInfo.changeAmount.scale = oiisrc.changeAmount.scale;
                oiFolderInfo.name = oiisrc.name;

                foreach (StudioHS.ObjectInfo s in oiisrc.child)
                    oiFolderInfo.child.Add(Converter.copyInfo(s));
                
                return oiFolderInfo;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                return null;
            }
        }

        public static StudioPH.ObjectInfo copyOILightInfo(StudioHS.OILightInfo oiisrc)
        {
            try
            {
                StudioPH.OILightInfo oiLightInfo = new StudioPH.OILightInfo(oiisrc.no, oiisrc.dicKey);
                oiLightInfo.visible = oiisrc.visible;
                oiLightInfo.changeAmount.pos = oiisrc.changeAmount.pos;
                oiLightInfo.changeAmount.rot = oiisrc.changeAmount.rot;
                oiLightInfo.changeAmount.scale = oiisrc.changeAmount.scale;
                oiLightInfo.color = oiisrc.color;
                oiLightInfo.enable = oiisrc.enable;
                oiLightInfo.intensity = oiisrc.intensity;
                oiLightInfo.range = oiisrc.range;
                oiLightInfo.shadow = oiisrc.shadow;
                oiLightInfo.spotAngle = oiisrc.spotAngle;
                return oiLightInfo;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                return null;
            }
        }

        public static StudioPH.ObjectInfo copyOIItemInfo(StudioHS.OIItemInfo oiisrc)
        {
            try
            {
                StudioPH.OIItemInfo oiItemInfo = new StudioPH.OIItemInfo(oiisrc.no, oiisrc.dicKey);
                oiItemInfo.animeSpeed = oiisrc.animeSpeed;
                oiItemInfo.color.SetDiffuseRGBA(oiisrc.color.rgbaDiffuse);
                oiItemInfo.color2.SetDiffuseRGBA(oiisrc.color2.rgbaDiffuse);
                oiItemInfo.color.alpha = oiisrc.color.alpha;
                oiItemInfo.color.specularIntensity = oiisrc.color.specularIntensity;
                oiItemInfo.color.specularSharpness = oiisrc.color.specularSharpness;
                oiItemInfo.color2.alpha = oiisrc.color2.alpha;
                oiItemInfo.color2.specularIntensity = oiisrc.color2.specularIntensity;
                oiItemInfo.color2.specularSharpness = oiisrc.color2.specularSharpness;
                oiItemInfo.enableFK = oiisrc.enableFK;
                oiItemInfo.visible = oiisrc.visible;
                oiItemInfo.changeAmount.rot = oiisrc.changeAmount.rot;
                oiItemInfo.changeAmount.scale = oiisrc.changeAmount.scale;
                
                Vector3 back = Vector3.back;
                switch (oiisrc.no)
                {
                    case 270:
                        Vector3 vector3_1 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.1f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_1;
                        break;
                    case 271:
                        Vector3 vector3_2 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.12f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_2;
                        break;
                    case 272:
                        Vector3 vector3_3 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.1f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_3;
                        break;
                    case 273:
                        Vector3 vector3_4 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.09f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_4;
                        break;
                    case 274:
                        Vector3 vector3_5 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.2f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_5;
                        break;
                    case 275:
                        Vector3 vector3_6 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.05f * oiisrc.changeAmount.scale.y, 0.1f * oiisrc.changeAmount.scale.z);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_6;
                        break;
                    case 276:
                        Vector3 vector3_7 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.015f * oiisrc.changeAmount.scale.y, 0.07f * oiisrc.changeAmount.scale.z);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_7;
                        break;
                    case 277:
                        Vector3 vector3_8 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.02f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_8;
                        break;
                    case 278:
                        oiItemInfo.changeAmount.scale = new Vector3(oiisrc.changeAmount.scale.x * 1.2f, oiisrc.changeAmount.scale.x * 1.2f, oiisrc.changeAmount.scale.x * 1.2f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos;
                        break;
                    case 279:
                        Vector3 vector3_9 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.12f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_9;
                        break;
                    case 280:
                        Vector3 vector3_10 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.15f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_10;
                        break;
                    case 281:
                        Vector3 vector3_11 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.12f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_11;
                        break;
                    case 282:
                        Vector3 vector3_12 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.2f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_12;
                        break;
                    case 283:
                        Vector3 vector3_13 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.2f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_13;
                        break;
                    case 284:
                        Vector3 vector3_14 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.12f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_14;
                        break;
                    case 285:
                        Vector3 vector3_15 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.2f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_15;
                        break;
                    case 286:
                        Vector3 vector3_16 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.12f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_16;
                        break;
                    case 287:
                        Vector3 vector3_17 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.2f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_17;
                        break;
                    case 288:
                        Vector3 vector3_18 = Quaternion.Euler(oiisrc.changeAmount.rot) * new Vector3(0.0f, 0.12f * oiisrc.changeAmount.scale.y, 0.0f);
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos + vector3_18;
                        break;
                    default:
                        oiItemInfo.changeAmount.pos = oiisrc.changeAmount.pos;
                        break;
                }
                
                oiItemInfo.bones = new Dictionary<string, StudioPH.OIBoneInfo>();
                foreach (KeyValuePair<string, StudioHS.OIBoneInfo> bone in oiisrc.bones)
                {
                    StudioPH.OIBoneInfo oiBoneInfo = new StudioPH.OIBoneInfo(bone.Value.dicKey);
                    oiBoneInfo.changeAmount.pos = bone.Value.changeAmount.pos;
                    oiBoneInfo.changeAmount.rot = bone.Value.changeAmount.rot;
                    oiBoneInfo.changeAmount.scale = bone.Value.changeAmount.scale;
                }
                
                oiItemInfo.animeNormalizedTime = oiisrc.animeNormalizedTime;
                foreach (StudioHS.ObjectInfo s in oiisrc.child)
                    oiItemInfo.child.Add(Converter.copyInfo(s));
                
                return oiItemInfo;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                return null;
            }
        }

        private static StudioPH.OICharInfo AddObjectFemale(string name)
        {
            CharacterPH.CustomParameter customParameter = new CharacterPH.CustomParameter(CharacterPH.SEX.FEMALE);
            using (var resStream = OpenManifestResourceStream(Assembly.GetExecutingAssembly(), name))
            {
                customParameter.Load(new BinaryReader(resStream), true, false);
            }

            return new StudioPH.OICharInfo(customParameter, StudioPH.Studio.GetNewIndex());
        }

        private static StudioPH.OICharInfo AddObjectMale(string name)
        {
            CharacterPH.CustomParameter customParameter = new CharacterPH.CustomParameter(CharacterPH.SEX.MALE);
            using (var resStream = OpenManifestResourceStream(Assembly.GetExecutingAssembly(), name))
            {
                customParameter.Load(new BinaryReader(resStream), false, true);
            }

            return new StudioPH.OICharInfo(customParameter, StudioPH.Studio.GetNewIndex());
        }

        private static Stream OpenManifestResourceStream(Assembly assembly, string name)
        {
            var resNames = assembly.GetManifestResourceNames();
            string resourcePath = null;

            foreach (string str in resNames)
            {
                if (str.EndsWith("." + name))
                {
                    resourcePath = str;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(resourcePath))
            {
                return assembly.GetManifestResourceStream(resourcePath);
            }

            throw new KeyNotFoundException("`" + name + "` not found.");
        }
    }
}

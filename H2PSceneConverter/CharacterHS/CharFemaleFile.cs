// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFemaleFile
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System.IO;
using UnityEngine;

namespace CharacterHS
{
    public class CharFemaleFile : CharFile
    {
        public CharFemaleFile()
          : base("【HoneySelectCharaFemale】", "chara/female/")
        {
            CharFileInfoCustomFemale infoCustomFemale = new CharFileInfoCustomFemale();
            this.femaleCustomInfo = infoCustomFemale;
            this.customInfo = infoCustomFemale;
            CharFileInfoClothesFemale infoClothesFemale = new CharFileInfoClothesFemale();
            this.femaleClothesInfo = infoClothesFemale;
            this.clothesInfo = infoClothesFemale;
            CharFileInfoCoordinateFemale coordinateFemale = new CharFileInfoCoordinateFemale();
            this.femaleCoordinateInfo = coordinateFemale;
            this.coordinateInfo = coordinateFemale;
            CharFileInfoStatusFemale infoStatusFemale = new CharFileInfoStatusFemale();
            this.femaleStatusInfo = infoStatusFemale;
            this.statusInfo = infoStatusFemale;
            CharFileInfoParameterFemale infoParameterFemale = new CharFileInfoParameterFemale();
            this.femaleParameterInfo = infoParameterFemale;
            this.parameterInfo = infoParameterFemale;
        }

        public CharFileInfoCustomFemale femaleCustomInfo { get; private set; }

        public CharFileInfoClothesFemale femaleClothesInfo { get; private set; }

        public CharFileInfoCoordinateFemale femaleCoordinateInfo { get; private set; }

        public CharFileInfoStatusFemale femaleStatusInfo { get; private set; }

        public CharFileInfoParameterFemale femaleParameterInfo { get; private set; }

        public override bool LoadFromSBPR(string path)
        {
            CharSave_SexyBeachPR charSaveSexyBeachPr = new CharSave_SexyBeachPR(CharSave_SexyBeachPR.ConvertType.HoneySelect);
            if (!charSaveSexyBeachPr.LoadCharaFile(path))
                return false;
            CharSave_SexyBeachPR.SaveData savedata = charSaveSexyBeachPr.savedata;
            if (savedata.sex == 0)
                return false;
            this.charaFileName = savedata.fileName;
            this.charaFilePNG = savedata.pngData;
            this.femaleCustomInfo.personality = savedata.personality;
            this.femaleCustomInfo.name = savedata.name;
            this.femaleCustomInfo.headId = savedata.headId;
            for (int index = 0; index < savedata.shapeBody.Length; ++index)
                this.femaleCustomInfo.shapeValueBody[index] = ClampEx(savedata.shapeBody[index], 0.0f, 1f);
            for (int index = 0; index < savedata.shapeFace.Length; ++index)
                this.femaleCustomInfo.shapeValueFace[index] = ClampEx(savedata.shapeFace[index], 0.0f, 1f);
            for (int index = 0; index < this.femaleCustomInfo.hairId.Length; ++index)
            {
                this.femaleCustomInfo.hairId[index] = savedata.hairId[index];
                savedata.hairColor[index].CopyToHoneySelect(this.femaleCustomInfo.hairColor[index]);
                savedata.hairAcsColor[index].CopyToHoneySelect(this.femaleCustomInfo.hairAcsColor[index]);
            }
            this.femaleCustomInfo.hairType = savedata.hairType;
            this.femaleCustomInfo.texFaceId = savedata.texFaceId;
            savedata.skinColor.CopyToHoneySelect(this.femaleCustomInfo.skinColor);
            this.femaleCustomInfo.texEyeshadowId = savedata.texEyeshadowId;
            savedata.eyeshadowColor.CopyToHoneySelect(this.femaleCustomInfo.eyeshadowColor);
            this.femaleCustomInfo.texCheekId = savedata.texCheekId;
            savedata.cheekColor.CopyToHoneySelect(this.femaleCustomInfo.cheekColor);
            this.femaleCustomInfo.texLipId = savedata.texLipId;
            savedata.lipColor.CopyToHoneySelect(this.femaleCustomInfo.lipColor);
            this.femaleCustomInfo.texTattoo_fId = savedata.texTattoo_fId;
            savedata.tattoo_fColor.CopyToHoneySelect(this.femaleCustomInfo.tattoo_fColor);
            this.femaleCustomInfo.texMoleId = savedata.texMoleId;
            savedata.moleColor.CopyToHoneySelect(this.femaleCustomInfo.moleColor);
            this.femaleCustomInfo.matEyebrowId = savedata.matEyebrowId;
            savedata.eyebrowColor.CopyToHoneySelect(this.femaleCustomInfo.eyebrowColor);
            this.femaleCustomInfo.matEyelashesId = savedata.matEyelashesId;
            savedata.eyelashesColor.CopyToHoneySelect(this.femaleCustomInfo.eyelashesColor);
            this.femaleCustomInfo.matEyeLId = savedata.matEyeLId;
            savedata.eyeLColor.CopyToHoneySelect(this.femaleCustomInfo.eyeLColor);
            this.femaleCustomInfo.matEyeRId = savedata.matEyeRId;
            savedata.eyeRColor.CopyToHoneySelect(this.femaleCustomInfo.eyeRColor);
            this.femaleCustomInfo.matEyeHiId = savedata.matEyeHiId;
            savedata.eyeHiColor.CopyToHoneySelect(this.femaleCustomInfo.eyeHiColor);
            savedata.eyeWColor.CopyToHoneySelect(this.femaleCustomInfo.eyeWColor);
            this.femaleCustomInfo.texFaceDetailId = 0;
            this.femaleCustomInfo.faceDetailWeight = 0.5f;
            this.femaleCustomInfo.texBodyId = 0;
            this.femaleCustomInfo.texSunburnId = savedata.texSunburnId;
            savedata.sunburnColor.CopyToHoneySelect(this.femaleCustomInfo.sunburnColor);
            this.femaleCustomInfo.texTattoo_bId = savedata.texTattoo_bId;
            savedata.tattoo_bColor.CopyToHoneySelect(this.femaleCustomInfo.tattoo_bColor);
            this.femaleCustomInfo.matNipId = savedata.matNipId;
            savedata.nipColor.CopyToHoneySelect(this.femaleCustomInfo.nipColor);
            this.femaleCustomInfo.matUnderhairId = savedata.matUnderhairId;
            savedata.underhairColor.CopyToHoneySelect(this.femaleCustomInfo.underhairColor);
            savedata.nailColor.CopyToHoneySelect(this.femaleCustomInfo.nailColor);
            this.femaleCustomInfo.areolaSize = savedata.nipSize;
            this.femaleCustomInfo.texBodyDetailId = savedata.texBodyId + 1;
            this.femaleCustomInfo.bodyDetailWeight = savedata.bodyDetailWeight;
            this.femaleCustomInfo.bustSoftness = savedata.bustSoftness;
            this.femaleCustomInfo.bustWeight = savedata.bustWeight;
            CharFileInfoClothesFemale infoClothesFemale1 = new CharFileInfoClothesFemale();
            infoClothesFemale1.swimType = false;
            infoClothesFemale1.clothesId[0] = savedata.coord[0].clothesTopId;
            savedata.coord[0].clothesTopColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[0]);
            infoClothesFemale1.clothesId[1] = savedata.coord[0].clothesBotId;
            savedata.coord[0].clothesBotColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[1]);
            infoClothesFemale1.clothesId[2] = savedata.coord[0].braId;
            savedata.coord[0].braColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[2]);
            infoClothesFemale1.clothesId[3] = savedata.coord[0].shortsId;
            savedata.coord[0].shortsColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[3]);
            infoClothesFemale1.clothesId[7] = savedata.coord[0].glovesId;
            savedata.coord[0].glovesColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[7]);
            infoClothesFemale1.clothesId[8] = savedata.coord[0].panstId;
            savedata.coord[0].panstColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[8]);
            infoClothesFemale1.clothesId[9] = savedata.coord[0].socksId;
            savedata.coord[0].socksColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[9]);
            infoClothesFemale1.clothesId[10] = savedata.coord[0].shoesId;
            savedata.coord[0].shoesColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[10]);
            for (int index = 0; index < 5; ++index)
            {
                infoClothesFemale1.accessory[index].type = savedata.accessory[0, index].accessoryType;
                infoClothesFemale1.accessory[index].id = savedata.accessory[0, index].accessoryId;
                infoClothesFemale1.accessory[index].parentKey = savedata.accessory[0, index].parentKey;
                infoClothesFemale1.accessory[index].addPos = savedata.accessory[0, index].plusPos;
                infoClothesFemale1.accessory[index].addRot = savedata.accessory[0, index].plusRot;
                infoClothesFemale1.accessory[index].addScl = savedata.accessory[0, index].plusScl;
                savedata.accessoryColor[0, index].CopyToHoneySelect(infoClothesFemale1.accessory[index].color);
            }
            this.femaleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type00, infoClothesFemale1);
            CharFileInfoClothesFemale infoClothesFemale2 = new CharFileInfoClothesFemale();
            infoClothesFemale2.swimType = true;
            infoClothesFemale2.clothesId[4] = savedata.coord[1].swimsuitId;
            savedata.coord[1].swimsuitColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[4]);
            infoClothesFemale2.clothesId[5] = savedata.coord[1].swimTopId;
            savedata.coord[1].swimTopColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[5]);
            infoClothesFemale2.clothesId[6] = savedata.coord[1].swimBotId;
            savedata.coord[1].swimBotColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[6]);
            infoClothesFemale2.clothesId[7] = savedata.coord[1].glovesId;
            savedata.coord[1].glovesColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[7]);
            infoClothesFemale2.clothesId[8] = savedata.coord[1].panstId;
            savedata.coord[1].panstColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[8]);
            infoClothesFemale2.clothesId[9] = savedata.coord[1].socksId;
            savedata.coord[1].socksColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[9]);
            infoClothesFemale2.clothesId[10] = savedata.coord[1].shoesId;
            savedata.coord[1].shoesColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[10]);
            infoClothesFemale2.hideSwimOptTop = savedata.stateSwimOptTop > 0;
            infoClothesFemale2.hideSwimOptBot = savedata.stateSwimOptBot > 0;
            for (int index = 0; index < 5; ++index)
            {
                infoClothesFemale2.accessory[index].type = savedata.accessory[1, index].accessoryType;
                infoClothesFemale2.accessory[index].id = savedata.accessory[1, index].accessoryId;
                infoClothesFemale2.accessory[index].parentKey = savedata.accessory[1, index].parentKey;
                infoClothesFemale2.accessory[index].addPos = savedata.accessory[1, index].plusPos;
                infoClothesFemale2.accessory[index].addRot = savedata.accessory[1, index].plusRot;
                infoClothesFemale2.accessory[index].addScl = savedata.accessory[1, index].plusScl;
                savedata.accessoryColor[1, index].CopyToHoneySelect(infoClothesFemale2.accessory[index].color);
            }
            this.femaleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type02, infoClothesFemale2);
            this.ChangeCoordinateType(CharDefine.CoordinateType.type00);
            return true;
        }

        public override bool LoadFromSBPR(BinaryReader br)
        {
            CharSave_SexyBeachPR charSaveSexyBeachPr = new CharSave_SexyBeachPR(CharSave_SexyBeachPR.ConvertType.HoneySelect);
            if (!charSaveSexyBeachPr.LoadCharaFile(br))
                return false;
            CharSave_SexyBeachPR.SaveData savedata = charSaveSexyBeachPr.savedata;
            if (savedata.sex == 0)
            {
                Debug.LogWarning("キャラファイルの性別が違います");
                return false;
            }
            this.FromSBPR(savedata);
            return true;
        }

        private void FromSBPR(CharSave_SexyBeachPR.SaveData savedata)
        {
            this.charaFileName = savedata.fileName;
            this.charaFilePNG = savedata.pngData;
            this.femaleCustomInfo.personality = savedata.personality;
            this.femaleCustomInfo.name = savedata.name;
            this.femaleCustomInfo.headId = savedata.headId;
            for (int index = 0; index < savedata.shapeBody.Length; ++index)
                this.femaleCustomInfo.shapeValueBody[index] = Mathf.Clamp(savedata.shapeBody[index], 0.0f, 1f);
            for (int index = 0; index < savedata.shapeFace.Length; ++index)
                this.femaleCustomInfo.shapeValueFace[index] = Mathf.Clamp(savedata.shapeFace[index], 0.0f, 1f);
            for (int index = 0; index < this.femaleCustomInfo.hairId.Length; ++index)
            {
                this.femaleCustomInfo.hairId[index] = savedata.hairId[index];
                savedata.hairColor[index].CopyToHoneySelect(this.femaleCustomInfo.hairColor[index]);
                savedata.hairAcsColor[index].CopyToHoneySelect(this.femaleCustomInfo.hairAcsColor[index]);
            }
            this.femaleCustomInfo.hairType = savedata.hairType;
            this.femaleCustomInfo.texFaceId = savedata.texFaceId;
            savedata.skinColor.CopyToHoneySelect(this.femaleCustomInfo.skinColor);
            this.femaleCustomInfo.texEyeshadowId = savedata.texEyeshadowId;
            savedata.eyeshadowColor.CopyToHoneySelect(this.femaleCustomInfo.eyeshadowColor);
            this.femaleCustomInfo.texCheekId = savedata.texCheekId;
            savedata.cheekColor.CopyToHoneySelect(this.femaleCustomInfo.cheekColor);
            this.femaleCustomInfo.texLipId = savedata.texLipId;
            savedata.lipColor.CopyToHoneySelect(this.femaleCustomInfo.lipColor);
            this.femaleCustomInfo.texTattoo_fId = savedata.texTattoo_fId;
            savedata.tattoo_fColor.CopyToHoneySelect(this.femaleCustomInfo.tattoo_fColor);
            this.femaleCustomInfo.texMoleId = savedata.texMoleId;
            savedata.moleColor.CopyToHoneySelect(this.femaleCustomInfo.moleColor);
            this.femaleCustomInfo.matEyebrowId = savedata.matEyebrowId;
            savedata.eyebrowColor.CopyToHoneySelect(this.femaleCustomInfo.eyebrowColor);
            this.femaleCustomInfo.matEyelashesId = savedata.matEyelashesId;
            savedata.eyelashesColor.CopyToHoneySelect(this.femaleCustomInfo.eyelashesColor);
            this.femaleCustomInfo.matEyeLId = savedata.matEyeLId;
            savedata.eyeLColor.CopyToHoneySelect(this.femaleCustomInfo.eyeLColor);
            this.femaleCustomInfo.matEyeRId = savedata.matEyeRId;
            savedata.eyeRColor.CopyToHoneySelect(this.femaleCustomInfo.eyeRColor);
            this.femaleCustomInfo.matEyeHiId = savedata.matEyeHiId;
            savedata.eyeHiColor.CopyToHoneySelect(this.femaleCustomInfo.eyeHiColor);
            savedata.eyeWColor.CopyToHoneySelect(this.femaleCustomInfo.eyeWColor);
            this.femaleCustomInfo.texFaceDetailId = 0;
            this.femaleCustomInfo.faceDetailWeight = 0.5f;
            this.femaleCustomInfo.texBodyId = 0;
            this.femaleCustomInfo.texSunburnId = savedata.texSunburnId;
            savedata.sunburnColor.CopyToHoneySelect(this.femaleCustomInfo.sunburnColor);
            this.femaleCustomInfo.texTattoo_bId = savedata.texTattoo_bId;
            savedata.tattoo_bColor.CopyToHoneySelect(this.femaleCustomInfo.tattoo_bColor);
            this.femaleCustomInfo.matNipId = savedata.matNipId;
            savedata.nipColor.CopyToHoneySelect(this.femaleCustomInfo.nipColor);
            this.femaleCustomInfo.matUnderhairId = savedata.matUnderhairId;
            savedata.underhairColor.CopyToHoneySelect(this.femaleCustomInfo.underhairColor);
            savedata.nailColor.CopyToHoneySelect(this.femaleCustomInfo.nailColor);
            this.femaleCustomInfo.areolaSize = savedata.nipSize;
            this.femaleCustomInfo.texBodyDetailId = savedata.texBodyId + 1;
            this.femaleCustomInfo.bodyDetailWeight = savedata.bodyDetailWeight;
            this.femaleCustomInfo.bustSoftness = savedata.bustSoftness;
            this.femaleCustomInfo.bustWeight = savedata.bustWeight;
            CharFileInfoClothesFemale infoClothesFemale1 = new CharFileInfoClothesFemale();
            infoClothesFemale1.swimType = false;
            infoClothesFemale1.clothesId[0] = savedata.coord[0].clothesTopId;
            savedata.coord[0].clothesTopColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[0]);
            infoClothesFemale1.clothesId[1] = savedata.coord[0].clothesBotId;
            savedata.coord[0].clothesBotColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[1]);
            infoClothesFemale1.clothesId[2] = savedata.coord[0].braId;
            savedata.coord[0].braColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[2]);
            infoClothesFemale1.clothesId[3] = savedata.coord[0].shortsId;
            savedata.coord[0].shortsColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[3]);
            infoClothesFemale1.clothesId[7] = savedata.coord[0].glovesId;
            savedata.coord[0].glovesColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[7]);
            infoClothesFemale1.clothesId[8] = savedata.coord[0].panstId;
            savedata.coord[0].panstColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[8]);
            infoClothesFemale1.clothesId[9] = savedata.coord[0].socksId;
            savedata.coord[0].socksColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[9]);
            infoClothesFemale1.clothesId[10] = savedata.coord[0].shoesId;
            savedata.coord[0].shoesColor.CopyToHoneySelect(infoClothesFemale1.clothesColor[10]);
            for (int index = 0; index < 5; ++index)
            {
                infoClothesFemale1.accessory[index].type = savedata.accessory[0, index].accessoryType;
                infoClothesFemale1.accessory[index].id = savedata.accessory[0, index].accessoryId;
                infoClothesFemale1.accessory[index].parentKey = savedata.accessory[0, index].parentKey;
                infoClothesFemale1.accessory[index].addPos = savedata.accessory[0, index].plusPos;
                infoClothesFemale1.accessory[index].addRot = savedata.accessory[0, index].plusRot;
                infoClothesFemale1.accessory[index].addScl = savedata.accessory[0, index].plusScl;
                savedata.accessoryColor[0, index].CopyToHoneySelect(infoClothesFemale1.accessory[index].color);
            }
            this.femaleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type00, infoClothesFemale1);
            CharFileInfoClothesFemale infoClothesFemale2 = new CharFileInfoClothesFemale();
            infoClothesFemale2.swimType = true;
            infoClothesFemale2.clothesId[4] = savedata.coord[1].swimsuitId;
            savedata.coord[1].swimsuitColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[4]);
            infoClothesFemale2.clothesId[5] = savedata.coord[1].swimTopId;
            savedata.coord[1].swimTopColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[5]);
            infoClothesFemale2.clothesId[6] = savedata.coord[1].swimBotId;
            savedata.coord[1].swimBotColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[6]);
            infoClothesFemale2.clothesId[7] = savedata.coord[1].glovesId;
            savedata.coord[1].glovesColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[7]);
            infoClothesFemale2.clothesId[8] = savedata.coord[1].panstId;
            savedata.coord[1].panstColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[8]);
            infoClothesFemale2.clothesId[9] = savedata.coord[1].socksId;
            savedata.coord[1].socksColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[9]);
            infoClothesFemale2.clothesId[10] = savedata.coord[1].shoesId;
            savedata.coord[1].shoesColor.CopyToHoneySelect(infoClothesFemale2.clothesColor[10]);
            infoClothesFemale2.hideSwimOptTop = savedata.stateSwimOptTop != 0;
            infoClothesFemale2.hideSwimOptBot = savedata.stateSwimOptBot != 0;
            for (int index = 0; index < 5; ++index)
            {
                infoClothesFemale2.accessory[index].type = savedata.accessory[1, index].accessoryType;
                infoClothesFemale2.accessory[index].id = savedata.accessory[1, index].accessoryId;
                infoClothesFemale2.accessory[index].parentKey = savedata.accessory[1, index].parentKey;
                infoClothesFemale2.accessory[index].addPos = savedata.accessory[1, index].plusPos;
                infoClothesFemale2.accessory[index].addRot = savedata.accessory[1, index].plusRot;
                infoClothesFemale2.accessory[index].addScl = savedata.accessory[1, index].plusScl;
                savedata.accessoryColor[1, index].CopyToHoneySelect(infoClothesFemale2.accessory[index].color);
            }
            this.femaleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type02, infoClothesFemale2);
        }
    }
}

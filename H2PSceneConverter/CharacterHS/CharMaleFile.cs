// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharMaleFile
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System.IO;
using UnityEngine;

namespace CharacterHS
{
    public class CharMaleFile : CharFile
    {
        public CharMaleFile()
          : base("【HoneySelectCharaMale】", "chara/male/")
        {
            CharFileInfoCustomMale fileInfoCustomMale = new CharFileInfoCustomMale();
            this.maleCustomInfo = fileInfoCustomMale;
            this.customInfo = fileInfoCustomMale;
            CharFileInfoClothesMale fileInfoClothesMale = new CharFileInfoClothesMale();
            this.maleClothesInfo = fileInfoClothesMale;
            this.clothesInfo = fileInfoClothesMale;
            CharFileInfoCoordinateMale infoCoordinateMale = new CharFileInfoCoordinateMale();
            this.maleCoordinateInfo = infoCoordinateMale;
            this.coordinateInfo = infoCoordinateMale;
            CharFileInfoStatusMale fileInfoStatusMale = new CharFileInfoStatusMale();
            this.maleStatusInfo = fileInfoStatusMale;
            this.statusInfo = fileInfoStatusMale;
            CharFileInfoParameterMale infoParameterMale = new CharFileInfoParameterMale();
            this.maleParameterInfo = infoParameterMale;
            this.parameterInfo = infoParameterMale;
        }

        public CharFileInfoCustomMale maleCustomInfo { get; private set; }

        public CharFileInfoClothesMale maleClothesInfo { get; private set; }

        public CharFileInfoCoordinateMale maleCoordinateInfo { get; private set; }

        public CharFileInfoStatusMale maleStatusInfo { get; private set; }

        public CharFileInfoParameterMale maleParameterInfo { get; private set; }

        public override bool LoadFromSBPR(BinaryReader br)
        {
            CharSave_SexyBeachPR charSaveSexyBeachPr = new CharSave_SexyBeachPR(CharSave_SexyBeachPR.ConvertType.HoneySelect);
            if (!charSaveSexyBeachPr.LoadCharaFile(br))
                return false;
            CharSave_SexyBeachPR.SaveData savedata = charSaveSexyBeachPr.savedata;
            if (savedata.sex == 1)
            {
                Debug.LogWarning("キャラファイルの性別が違います");
                return false;
            }
            this.FromSBPR(savedata);
            return true;
        }

        public override bool LoadFromSBPR(string path)
        {
            CharSave_SexyBeachPR charSaveSexyBeachPr = new CharSave_SexyBeachPR(CharSave_SexyBeachPR.ConvertType.HoneySelect);
            if (!charSaveSexyBeachPr.LoadCharaFile(path))
                return false;
            CharSave_SexyBeachPR.SaveData savedata = charSaveSexyBeachPr.savedata;
            if (savedata.sex == 1)
                return false;
            this.charaFileName = savedata.fileName;
            this.charaFilePNG = savedata.pngData;
            this.maleCustomInfo.personality = savedata.personality;
            this.maleCustomInfo.name = savedata.name;
            this.maleCustomInfo.headId = savedata.headId;
            for (int index = 0; index < savedata.shapeBody.Length; ++index)
                this.maleCustomInfo.shapeValueBody[index] = CharFile.ClampEx(savedata.shapeBody[index], 0.0f, 1f);
            for (int index = 0; index < savedata.shapeFace.Length; ++index)
                this.maleCustomInfo.shapeValueFace[index] = CharFile.ClampEx(savedata.shapeFace[index], 0.0f, 1f);
            for (int index = 0; index < this.maleCustomInfo.hairId.Length; ++index)
            {
                this.maleCustomInfo.hairId[index] = savedata.hairId[index];
                savedata.hairColor[index].CopyToHoneySelect(this.maleCustomInfo.hairColor[index]);
                savedata.hairAcsColor[index].CopyToHoneySelect(this.maleCustomInfo.hairAcsColor[index]);
            }
            this.maleCustomInfo.texFaceId = savedata.texFaceId;
            this.maleCustomInfo.matBeardId = savedata.beardId;
            savedata.beardColor.CopyToHoneySelect(this.maleCustomInfo.beardColor);
            savedata.skinColor.CopyToHoneySelect(this.maleCustomInfo.skinColor);
            this.maleCustomInfo.texTattoo_fId = savedata.texTattoo_fId;
            savedata.tattoo_fColor.CopyToHoneySelect(this.maleCustomInfo.tattoo_fColor);
            this.maleCustomInfo.matEyebrowId = savedata.matEyebrowId;
            savedata.eyebrowColor.CopyToHoneySelect(this.maleCustomInfo.eyebrowColor);
            this.maleCustomInfo.matEyeLId = savedata.matEyeLId;
            savedata.eyeLColor.CopyToHoneySelect(this.maleCustomInfo.eyeLColor);
            this.maleCustomInfo.matEyeRId = savedata.matEyeRId;
            savedata.eyeRColor.CopyToHoneySelect(this.maleCustomInfo.eyeRColor);
            savedata.eyeWColor.CopyToHoneySelect(this.maleCustomInfo.eyeWColor);
            this.maleCustomInfo.texFaceDetailId = 1;
            this.maleCustomInfo.faceDetailWeight = savedata.faceDetailWeight;
            this.maleCustomInfo.texBodyId = savedata.texBodyId;
            this.maleCustomInfo.texTattoo_bId = savedata.texTattoo_bId;
            savedata.tattoo_bColor.CopyToHoneySelect(this.maleCustomInfo.tattoo_bColor);
            this.maleCustomInfo.texBodyDetailId = savedata.texBodyId + 1;
            this.maleCustomInfo.bodyDetailWeight = savedata.bodyDetailWeight;
            CharFileInfoClothesMale fileInfoClothesMale1 = new CharFileInfoClothesMale();
            fileInfoClothesMale1.clothesId[0] = savedata.coord[0].clothesTopId;
            savedata.coord[0].clothesTopColor.CopyToHoneySelect(fileInfoClothesMale1.clothesColor[0]);
            fileInfoClothesMale1.clothesId[1] = savedata.coord[0].shoesId;
            savedata.coord[0].shoesColor.CopyToHoneySelect(fileInfoClothesMale1.clothesColor[1]);
            for (int index = 0; index < 5; ++index)
            {
                fileInfoClothesMale1.accessory[index].type = savedata.accessory[0, index].accessoryType;
                fileInfoClothesMale1.accessory[index].id = savedata.accessory[0, index].accessoryId;
                fileInfoClothesMale1.accessory[index].parentKey = savedata.accessory[0, index].parentKey;
                fileInfoClothesMale1.accessory[index].addPos = savedata.accessory[0, index].plusPos;
                fileInfoClothesMale1.accessory[index].addRot = savedata.accessory[0, index].plusRot;
                fileInfoClothesMale1.accessory[index].addScl = savedata.accessory[0, index].plusScl;
                savedata.accessoryColor[0, index].CopyToHoneySelect(fileInfoClothesMale1.accessory[index].color);
            }
            this.maleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type00, fileInfoClothesMale1);
            CharFileInfoClothesMale fileInfoClothesMale2 = new CharFileInfoClothesMale();
            fileInfoClothesMale2.clothesId[0] = savedata.coord[1].clothesTopId;
            savedata.coord[1].clothesTopColor.CopyToHoneySelect(fileInfoClothesMale2.clothesColor[0]);
            fileInfoClothesMale2.clothesId[1] = savedata.coord[1].shoesId;
            savedata.coord[1].shoesColor.CopyToHoneySelect(fileInfoClothesMale2.clothesColor[1]);
            for (int index = 0; index < 5; ++index)
            {
                fileInfoClothesMale2.accessory[index].type = savedata.accessory[1, index].accessoryType;
                fileInfoClothesMale2.accessory[index].id = savedata.accessory[1, index].accessoryId;
                fileInfoClothesMale2.accessory[index].parentKey = savedata.accessory[1, index].parentKey;
                fileInfoClothesMale2.accessory[index].addPos = savedata.accessory[1, index].plusPos;
                fileInfoClothesMale2.accessory[index].addRot = savedata.accessory[1, index].plusRot;
                fileInfoClothesMale2.accessory[index].addScl = savedata.accessory[1, index].plusScl;
                savedata.accessoryColor[1, index].CopyToHoneySelect(fileInfoClothesMale2.accessory[index].color);
            }
            this.maleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type02, fileInfoClothesMale2);
            this.ChangeCoordinateType(CharDefine.CoordinateType.type00);
            return true;
        }

        private void FromSBPR(CharSave_SexyBeachPR.SaveData savedata)
        {
            this.charaFileName = savedata.fileName;
            this.charaFilePNG = savedata.pngData;
            this.maleCustomInfo.personality = savedata.personality;
            this.maleCustomInfo.name = savedata.name;
            this.maleCustomInfo.headId = savedata.headId;
            for (int index = 0; index < savedata.shapeBody.Length; ++index)
                this.maleCustomInfo.shapeValueBody[index] = Mathf.Clamp(savedata.shapeBody[index], 0.0f, 1f);
            for (int index = 0; index < savedata.shapeFace.Length; ++index)
                this.maleCustomInfo.shapeValueFace[index] = Mathf.Clamp(savedata.shapeFace[index], 0.0f, 1f);
            for (int index = 0; index < this.maleCustomInfo.hairId.Length; ++index)
            {
                this.maleCustomInfo.hairId[index] = savedata.hairId[index];
                savedata.hairColor[index].CopyToHoneySelect(this.maleCustomInfo.hairColor[index]);
                savedata.hairAcsColor[index].CopyToHoneySelect(this.maleCustomInfo.hairAcsColor[index]);
            }
            this.maleCustomInfo.texFaceId = savedata.texFaceId;
            this.maleCustomInfo.matBeardId = savedata.beardId;
            savedata.beardColor.CopyToHoneySelect(this.maleCustomInfo.beardColor);
            savedata.skinColor.CopyToHoneySelect(this.maleCustomInfo.skinColor);
            this.maleCustomInfo.texTattoo_fId = savedata.texTattoo_fId;
            savedata.tattoo_fColor.CopyToHoneySelect(this.maleCustomInfo.tattoo_fColor);
            this.maleCustomInfo.matEyebrowId = savedata.matEyebrowId;
            savedata.eyebrowColor.CopyToHoneySelect(this.maleCustomInfo.eyebrowColor);
            this.maleCustomInfo.matEyeLId = savedata.matEyeLId;
            savedata.eyeLColor.CopyToHoneySelect(this.maleCustomInfo.eyeLColor);
            this.maleCustomInfo.matEyeRId = savedata.matEyeRId;
            savedata.eyeRColor.CopyToHoneySelect(this.maleCustomInfo.eyeRColor);
            savedata.eyeWColor.CopyToHoneySelect(this.maleCustomInfo.eyeWColor);
            this.maleCustomInfo.texFaceDetailId = 1;
            this.maleCustomInfo.faceDetailWeight = savedata.faceDetailWeight;
            this.maleCustomInfo.texBodyId = savedata.texBodyId;
            this.maleCustomInfo.texTattoo_bId = savedata.texTattoo_bId;
            savedata.tattoo_bColor.CopyToHoneySelect(this.maleCustomInfo.tattoo_bColor);
            this.maleCustomInfo.texBodyDetailId = savedata.texBodyId + 1;
            this.maleCustomInfo.bodyDetailWeight = savedata.bodyDetailWeight;
            CharFileInfoClothesMale fileInfoClothesMale1 = new CharFileInfoClothesMale();
            fileInfoClothesMale1.clothesId[0] = savedata.coord[0].clothesTopId;
            savedata.coord[0].clothesTopColor.CopyToHoneySelect(fileInfoClothesMale1.clothesColor[0]);
            fileInfoClothesMale1.clothesId[1] = savedata.coord[0].shoesId;
            savedata.coord[0].shoesColor.CopyToHoneySelect(fileInfoClothesMale1.clothesColor[1]);
            for (int index = 0; index < 5; ++index)
            {
                fileInfoClothesMale1.accessory[index].type = savedata.accessory[0, index].accessoryType;
                fileInfoClothesMale1.accessory[index].id = savedata.accessory[0, index].accessoryId;
                fileInfoClothesMale1.accessory[index].parentKey = savedata.accessory[0, index].parentKey;
                fileInfoClothesMale1.accessory[index].addPos = savedata.accessory[0, index].plusPos;
                fileInfoClothesMale1.accessory[index].addRot = savedata.accessory[0, index].plusRot;
                fileInfoClothesMale1.accessory[index].addScl = savedata.accessory[0, index].plusScl;
                savedata.accessoryColor[0, index].CopyToHoneySelect(fileInfoClothesMale1.accessory[index].color);
            }
            this.maleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type00, fileInfoClothesMale1);
            CharFileInfoClothesMale fileInfoClothesMale2 = new CharFileInfoClothesMale();
            fileInfoClothesMale2.clothesId[0] = savedata.coord[1].clothesTopId;
            savedata.coord[1].clothesTopColor.CopyToHoneySelect(fileInfoClothesMale2.clothesColor[0]);
            fileInfoClothesMale2.clothesId[1] = savedata.coord[1].shoesId;
            savedata.coord[1].shoesColor.CopyToHoneySelect(fileInfoClothesMale2.clothesColor[1]);
            for (int index = 0; index < 5; ++index)
            {
                fileInfoClothesMale2.accessory[index].type = savedata.accessory[1, index].accessoryType;
                fileInfoClothesMale2.accessory[index].id = savedata.accessory[1, index].accessoryId;
                fileInfoClothesMale2.accessory[index].parentKey = savedata.accessory[1, index].parentKey;
                fileInfoClothesMale2.accessory[index].addPos = savedata.accessory[1, index].plusPos;
                fileInfoClothesMale2.accessory[index].addRot = savedata.accessory[1, index].plusRot;
                fileInfoClothesMale2.accessory[index].addScl = savedata.accessory[1, index].plusScl;
                savedata.accessoryColor[1, index].CopyToHoneySelect(fileInfoClothesMale2.accessory[index].color);
            }
            this.maleCoordinateInfo.SetInfo(CharDefine.CoordinateType.type02, fileInfoClothesMale2);
        }
    }
}

using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class CustomParameter : ParameterBase
    {
        public HairParameter hair;
        public HeadParameter head;
        public BodyParameter body;
        public WearParameter wear;
        public AccessoryParameter acce;

        public CustomParameter(SEX sex)
          : base(sex)
        {
            this.sex = sex;
            this.hair = new HairParameter(sex);
            this.head = new HeadParameter(sex);
            this.body = new BodyParameter(sex);
            this.wear = new WearParameter(sex);
            this.acce = new AccessoryParameter(sex);
        }

        public CustomParameter(CustomParameter copy)
          : base(copy.sex)
        {
            this.hair = new HairParameter(copy.hair);
            this.head = new HeadParameter(copy.head);
            this.body = new BodyParameter(copy.body);
            this.wear = new WearParameter(copy.wear);
            this.acce = new AccessoryParameter(copy.acce);
        }

        public void Copy(CustomParameter copy, int filter = -1)
        {
            if (copy == null)
                return;
            
            if (this.sex != copy.sex)
                Debug.LogWarning("違う性別のデータをコピーした");
            
            this.sex = copy.sex;
            
            if ((filter & 1) != 0)
                this.hair.Copy(copy.hair);
            
            if ((filter & 2) != 0)
                this.head.Copy(copy.head);
            
            if ((filter & 4) != 0)
                this.body.Copy(copy.body);
            
            if ((filter & 8) != 0)
            {
                this.wear.Copy(copy.wear);
            }
            
            if ((filter & 16) == 0)
                return;
            this.acce.Copy(copy.acce);
        }

        private void Init()
        {
            this.hair.Init();
            this.head.Init();
            this.body.Init();
            this.wear.Init();
            this.acce.Init();
        }

        private void InitCoordinate()
        {
            this.wear.Init();
            this.acce.Init();
        }

        public void FromSexyData(CharacterHS.CharFemaleFile sexy)
        {
            this.Init();
            this.FromSexyData_Common(sexy);
            this.head.eyeLashID = sexy.femaleCustomInfo.matEyelashesId;
            this.head.eyeLashColor.FromSexyData(sexy.femaleCustomInfo.eyelashesColor);
            this.head.eyeshadowTexID = sexy.femaleCustomInfo.texEyeshadowId;
            this.head.eyeshadowColor = sexy.femaleCustomInfo.eyeshadowColor.rgbaDiffuse;
            this.head.cheekTexID = sexy.femaleCustomInfo.texCheekId;
            this.head.cheekColor = sexy.femaleCustomInfo.cheekColor.rgbaDiffuse;
            this.head.lipTexID = sexy.femaleCustomInfo.texLipId;
            this.head.lipColor = sexy.femaleCustomInfo.lipColor.rgbaDiffuse;
            this.head.moleTexID = sexy.femaleCustomInfo.texMoleId;
            this.head.moleColor = sexy.femaleCustomInfo.moleColor.rgbaDiffuse;
            this.head.eyeHighlightTexID = sexy.femaleCustomInfo.matEyeHiId;
            this.head.eyeHighlightColor.FromSexyData(sexy.femaleCustomInfo.eyeHiColor);
            this.head.eyeHighlightColor.specColor1 = Color.white;
            this.body.sunburnID = sexy.femaleCustomInfo.texSunburnId;
            this.body.nipID = sexy.femaleCustomInfo.matNipId;
            this.body.nipColor.FromSexyData(sexy.femaleCustomInfo.nipColor);
            this.body.underhairID = sexy.femaleCustomInfo.matUnderhairId;
            this.body.underhairColor.mainColor = sexy.femaleCustomInfo.underhairColor.rgbaDiffuse;
            this.body.underhairColor.metallic = 0.0f;
            this.body.underhairColor.smooth = 0.5f;
            this.body.nailColor.Init(false, 1f);
            this.body.manicureColor.FromSexyData(sexy.femaleCustomInfo.nailColor);
            this.body.manicureColor.mainColor1.a = 0.0f;
            this.body.areolaSize = sexy.femaleCustomInfo.areolaSize;
            this.body.bustSoftness = sexy.femaleCustomInfo.bustSoftness;
            this.body.bustWeight = sexy.femaleCustomInfo.bustWeight;

            CharacterHS.CharFileInfoClothesFemale info = sexy.femaleCoordinateInfo.GetInfo(CharacterHS.CharDefine.CoordinateType.type00) as CharacterHS.CharFileInfoClothesFemale;
            for (int index = 0; index < 11; ++index)
            {
                this.wear.wears[index].id = info.clothesId[index];
                if (this.wear.wears[index].color == null)
                    this.wear.wears[index].color = new ColorParameter_PBR2();
                this.wear.wears[index].color.FromSexyData(info.clothesColor[index], info.clothesColor2[index]);
            }
            
            this.wear.isSwimwear = info.swimType;
            this.wear.swimOptTop = !info.hideSwimOptTop;
            this.wear.swimOptBtm = !info.hideSwimOptBot;
            this.body.shapeVals[this.body.shapeVals.Length - 1] = 0.0f;
        }

        public void FromSexyData(CharacterHS.CharMaleFile sexy)
        {
            this.Init();
            this.FromSexyData_Common(sexy);
            this.head.beardID = sexy.maleCustomInfo.matBeardId;
            this.head.beardColor = sexy.maleCustomInfo.beardColor.rgbaDiffuse;
            this.body.underhairID = 0;
            this.body.underhairColor = new ColorParameter_Alloy();
            this.body.underhairColor.mainColor = Color.black;
            this.body.underhairColor.metallic = 0.0f;
            this.body.underhairColor.smooth = 0.5f;
            
            CharacterHS.CharFileInfoClothes info = sexy.maleCoordinateInfo.GetInfo(CharacterHS.CharDefine.CoordinateType.type00);
            int index1 = 0;
            int index2 = 1;
            if (this.wear.wears[0].color == null)
                this.wear.wears[0].color = new ColorParameter_PBR2();
            
            if (this.wear.wears[10].color == null)
                this.wear.wears[10].color = new ColorParameter_PBR2();
            
            this.wear.wears[0].id = info.clothesId[index1];
            this.wear.wears[0].color.FromSexyData(info.clothesColor[index1], info.clothesColor2[index1]);
            this.wear.wears[10].id = info.clothesId[index2];
            this.wear.wears[10].color.FromSexyData(info.clothesColor[index2], info.clothesColor2[index2]);
        }

        private void FromSexyData_Common(CharacterHS.CharFile sexy)
        {
            this.body.bodyID = sexy.customInfo.texBodyDetailId;
            this.body.detailID = sexy.customInfo.texBodyDetailId;
            this.body.detailWeight = sexy.customInfo.bodyDetailWeight;
            this.body.skinColor.FromSexyData(sexy.customInfo.skinColor);
            this.body.tattooID = sexy.customInfo.texTattoo_bId;
            this.body.tattooColor = sexy.customInfo.tattoo_bColor.rgbaDiffuse;
            sexy.customInfo.shapeValueBody.CopyTo(body.shapeVals, 0);
            this.head.headID = sexy.customInfo.headId;
            this.head.faceTexID = sexy.customInfo.texFaceId;
            this.head.tattooID = sexy.customInfo.texTattoo_fId;
            this.head.tattooColor = sexy.customInfo.tattoo_fColor.rgbaDiffuse;
            this.head.eyeBrowID = sexy.customInfo.matEyebrowId;
            this.head.eyeBrowColor.FromSexyData(sexy.customInfo.eyebrowColor);
            this.head.eyeID_L = sexy.customInfo.matEyeLId;
            this.head.eyeIrisColorL = sexy.customInfo.eyeLColor.rgbDiffuse;
            this.head.eyeID_R = sexy.customInfo.matEyeRId;
            this.head.eyeIrisColorR = sexy.customInfo.eyeRColor.rgbDiffuse;
            this.head.eyeScleraColorL = sexy.customInfo.eyeWColor.rgbDiffuse;
            this.head.eyeScleraColorR = sexy.customInfo.eyeWColor.rgbDiffuse;
            this.head.detailID = sexy.customInfo.texFaceDetailId;
            this.head.detailWeight = sexy.customInfo.faceDetailWeight;
            this.head.eyePupilDilationL = 0.0f;
            this.head.eyePupilDilationR = 0.0f;
            this.head.eyeEmissiveL = 0.5f;
            this.head.eyeEmissiveR = 0.5f;
            this.head.shapeVals = new float[sexy.customInfo.shapeValueFace.Length];
            sexy.customInfo.shapeValueFace.CopyTo(head.shapeVals, 0);
            
            int num = this.sex != SEX.FEMALE ? 1 : 3;
            for (int index = 0; index < num; ++index)
            {
                this.hair.parts[index].ID = sexy.customInfo.hairId[index];
                if (this.hair.parts[index].hairColor == null)
                    this.hair.parts[index].hairColor = new ColorParameter_Hair();
                this.hair.parts[index].hairColor.FromSexyData(sexy.customInfo.hairColor[index]);
                this.hair.parts[index].acceColor = null;
            }

            CharacterHS.CharFileInfoClothes info = sexy.coordinateInfo.GetInfo(CharacterHS.CharDefine.CoordinateType.type00);
            for (int index = 0; index < 10; ++index)
            {
                CharacterHS.CharFileInfoClothes.Accessory accessory = info.accessory[index];
                this.acce.slot[index].Set((ACCESSORY_TYPE)accessory.type, accessory.id, accessory.parentKey);
                this.acce.slot[index].addPos = accessory.addPos;
                this.acce.slot[index].addRot = accessory.addRot;
                this.acce.slot[index].addScl = accessory.addScl;
                if (this.acce.slot[index].color == null)
                    this.acce.slot[index].color = new ColorParameter_PBR2();
                this.acce.slot[index].color.FromSexyData(accessory.color, accessory.color2);
            }
        }

        public void FromSexyCoordinateData(CharacterHS.CharFileInfoClothesFemale sexyCoord)
        {
            this.InitCoordinate();
            this.FromSexyCoordinateData_Common(sexyCoord);
            for (int index = 0; index < 11; ++index)
            {
                this.wear.wears[index].id = sexyCoord.clothesId[index];
                if (this.wear.wears[index].color == null)
                    this.wear.wears[index].color = new ColorParameter_PBR2();
                this.wear.wears[index].color.FromSexyData(sexyCoord.clothesColor[index], sexyCoord.clothesColor2[index]);
            }
            this.wear.isSwimwear = sexyCoord.swimType;
            this.wear.swimOptTop = !sexyCoord.hideSwimOptTop;
            this.wear.swimOptBtm = !sexyCoord.hideSwimOptBot;
        }

        public void FromSexyCoordinateData(CharacterHS.CharFileInfoClothesMale sexyCoord)
        {
            this.InitCoordinate();
            this.FromSexyCoordinateData_Common(sexyCoord);
            int index1 = 0;
            int index2 = 1;
            if (this.wear.wears[0].color == null)
                this.wear.wears[0].color = new ColorParameter_PBR2();
            if (this.wear.wears[10].color == null)
                this.wear.wears[10].color = new ColorParameter_PBR2();
            this.wear.wears[0].id = sexyCoord.clothesId[index1];
            this.wear.wears[0].color.FromSexyData(sexyCoord.clothesColor[index1], sexyCoord.clothesColor2[index1]);
            this.wear.wears[10].id = sexyCoord.clothesId[index2];
            this.wear.wears[10].color.FromSexyData(sexyCoord.clothesColor[index2], sexyCoord.clothesColor2[index2]);
        }

        private void FromSexyCoordinateData_Common(CharacterHS.CharFileInfoClothes sexyCoord)
        {
            for (int index = 0; index < 10; ++index)
            {
                CharacterHS.CharFileInfoClothes.Accessory accessory = sexyCoord.accessory[index];
                this.acce.slot[index].Set((ACCESSORY_TYPE)accessory.type, accessory.id, accessory.parentKey);
                this.acce.slot[index].addPos = accessory.addPos;
                this.acce.slot[index].addRot = accessory.addRot;
                this.acce.slot[index].addScl = accessory.addScl;
                if (this.acce.slot[index].color == null)
                    this.acce.slot[index].color = new ColorParameter_PBR2();
                this.acce.slot[index].color.FromSexyData(accessory.color, accessory.color2);
            }
        }

        public void Save(BinaryWriter writer)
        {
            this.Write(writer, CUSTOM_DATA_VERSION.DEBUG_10);
            this.Write(writer, this.sex);
            this.hair.Save(writer, this.sex);
            this.head.Save(writer, this.sex);
            this.body.Save(writer, this.sex);
            this.wear.Save(writer, this.sex);
            this.acce.Save(writer, this.sex);
        }

        public void Load(string file, bool female, bool male)
        {
            if (!File.Exists(file))
                return;
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    if (this.Load(reader, female, male))
                        return;
                    Debug.LogError("読み込みに失敗しました");
                }
            }
        }

        public bool Load(BinaryReader reader, bool female, bool male)
        {
            bool flag = true;
            try
            {
                long offset = PngAssist.CheckSize(reader);
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                string str = reader.ReadString();
                if (str == "【HoneySelectCharaFemale】")
                {
                    Debug.Log("ハニーセレクト：女");
                    if (female)
                    {
                        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                        CharacterHS.CharFemaleFile sexy = new CharacterHS.CharFemaleFile();
                        flag = sexy.Load(reader, true, true);
                        this.FromSexyData(sexy);
                    }
                    else
                        Debug.LogWarning("異性データ");
                }
                else if (str == "【PremiumResortCharaFemale】")
                {
                    Debug.Log("セクシービーチプレミアムリゾート：女");
                    if (female)
                    {
                        reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                        CharacterHS.CharFemaleFile sexy = new CharacterHS.CharFemaleFile();
                        flag = sexy.LoadFromSBPR(reader);
                        this.FromSexyData(sexy);
                    }
                    else
                        Debug.LogWarning("異性データ");
                }
                else if (str == "【PlayHome_Female】")
                {
                    if (female)
                        this.Load(reader);
                    else
                        Debug.LogWarning("異性データ");
                }
                else if (str == "【HoneySelectCharaMale】")
                {
                    Debug.Log("ハニーセレクト：男");
                    if (male)
                    {
                        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                        CharacterHS.CharMaleFile sexy = new CharacterHS.CharMaleFile();
                        flag = sexy.Load(reader, true, true);
                        this.FromSexyData(sexy);
                    }
                    else
                        Debug.LogWarning("異性データ");
                }
                else if (str == "【PremiumResortCharaMale】")
                {
                    Debug.Log("セクシービーチプレミアムリゾート：男");
                    if (male)
                    {
                        reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                        CharacterHS.CharMaleFile sexy = new CharacterHS.CharMaleFile();
                        flag = sexy.LoadFromSBPR(reader);
                        this.FromSexyData(sexy);
                    }
                    else
                        Debug.LogWarning("異性データ");
                }
                else if (str == "【PlayHome_Male】")
                {
                    if (male)
                        this.Load(reader);
                    else
                        Debug.LogWarning("異性データ");
                }
                else
                    Debug.LogWarning("読めないセーブデータ:" + str);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                flag = false;
            }
            return flag;
        }

        public void Load(BinaryReader reader)
        {
            this.Init();
            CUSTOM_DATA_VERSION version = CUSTOM_DATA_VERSION.UNKNOWN;
            this.Read(reader, ref version);
            this.Read(reader, ref this.sex);
            this.hair.Load(reader, this.sex, version);
            this.head.Load(reader, this.sex, version);
            this.body.Load(reader, this.sex, version);
            this.wear.Load(reader, this.sex, version);
            this.acce.Load(reader, this.sex, version);
        }

        public void SaveCoordinate(BinaryWriter writer)
        {
            this.Write(writer, CUSTOM_DATA_VERSION.DEBUG_10);
            this.Write(writer, this.sex);
            this.wear.Save(writer, this.sex);
            this.acce.Save(writer, this.sex);
        }

        public void LoadCoordinate(BinaryReader reader)
        {
            this.Init();
            CUSTOM_DATA_VERSION version = CUSTOM_DATA_VERSION.UNKNOWN;
            this.Read(reader, ref version);
            this.Read(reader, ref this.sex);
            this.wear.Load(reader, this.sex, version);
            this.acce.Load(reader, this.sex, version);
        }

        protected void Write(BinaryWriter writer, CUSTOM_DATA_VERSION version)
        {
            writer.Write((int)version);
        }

        protected void Read(BinaryReader reader, ref CUSTOM_DATA_VERSION version)
        {
            int num = reader.ReadInt32();
            if (num > 10)
                version = CUSTOM_DATA_VERSION.UNKNOWN;
            else
                version = (CUSTOM_DATA_VERSION)num;
        }

        protected void Write(BinaryWriter writer, SEX sex)
        {
            writer.Write((int)sex);
        }

        protected void Read(BinaryReader reader, ref SEX sex)
        {
            sex = (SEX)reader.ReadInt32();
        }

        public bool CheckWrongParam()
        {
            if (this.sex != SEX.FEMALE && this.sex != SEX.MALE || this.acce.slot.Length != 10)
                return true;
            for (int index = 0; index < this.head.shapeVals.Length; ++index)
            {
                if (this.head.shapeVals[index] < 0.0 || this.head.shapeVals[index] > 1.0)
                    return true;
            }
            for (int index = 0; index < this.body.shapeVals.Length; ++index)
            {
                if (this.body.shapeVals[index] < 0.0 || this.body.shapeVals[index] > 1.0)
                    return true;
            }
            return false;
        }
    }
}

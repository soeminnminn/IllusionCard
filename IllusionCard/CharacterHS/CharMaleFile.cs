// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharMaleFile
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;

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
    }
}

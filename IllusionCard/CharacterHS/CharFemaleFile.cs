// Decompiled with JetBrains decompiler
// Type: CharacterHS.CharFemaleFile
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;

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
    }
}

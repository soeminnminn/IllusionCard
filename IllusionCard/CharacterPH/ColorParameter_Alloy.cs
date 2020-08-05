using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class ColorParameter_Alloy : ColorParameter_Base
    {
        public Color mainColor = Color.white;
        public float metallic;
        public float smooth;

        public ColorParameter_Alloy()
        {
        }

        public ColorParameter_Alloy(ColorParameter_Alloy copy)
        {
            this.mainColor = copy.mainColor;
            this.metallic = copy.metallic;
            this.smooth = copy.smooth;
        }

        public override COLOR_TYPE GetColorType()
        {
            return COLOR_TYPE.ALLOY;
        }

        public override void Save(BinaryWriter writer)
        {
            this.Save_ColorType(writer);
            this.WriteColor(writer, this.mainColor);
            writer.Write(this.metallic);
            writer.Write(this.smooth);
        }

        public override bool Load(BinaryReader reader, CUSTOM_DATA_VERSION version)
        {
            if (version < CUSTOM_DATA_VERSION.DEBUG_04)
            {
                this.ReadColor(reader, ref this.mainColor);
                return true;
            }
            COLOR_TYPE colorType = Load_ColorType(reader, version);
            if (colorType == COLOR_TYPE.NONE)
                return false;
            if (colorType != this.GetColorType())
                Debug.LogError("色タイプが違う");
            this.ReadColor(reader, ref this.mainColor);
            this.metallic = reader.ReadSingle();
            this.smooth = reader.ReadSingle();
            return true;
        }

        public void FromSexyData(HSColorSet colorSet)
        {
            this.mainColor = colorSet.rgbaDiffuse;
            this.metallic = colorSet.specularIntensity;
            this.smooth = colorSet.specularSharpness;
        }
    }
}

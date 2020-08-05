using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class ColorParameter_EyeHighlight : ColorParameter_Base
    {
        public Color mainColor1 = Color.white;
        public Color specColor1 = Color.white;
        public float specular1 = 1f;
        public float smooth1 = 1f;

        public ColorParameter_EyeHighlight()
        {
        }

        public ColorParameter_EyeHighlight(ColorParameter_EyeHighlight copy)
        {
            this.mainColor1 = copy.mainColor1;
            this.specColor1 = copy.specColor1;
            this.specular1 = copy.specular1;
            this.smooth1 = copy.smooth1;
        }

        public override COLOR_TYPE GetColorType()
        {
            return COLOR_TYPE.EYEHIGHLIGHT;
        }

        public override void Save(BinaryWriter writer)
        {
            this.Save_ColorType(writer);
            this.WriteColor(writer, this.mainColor1);
            this.WriteColor(writer, this.specColor1);
            writer.Write(this.specular1);
            writer.Write(this.smooth1);
        }

        public override bool Load(BinaryReader reader, CUSTOM_DATA_VERSION version)
        {
            if (version < CUSTOM_DATA_VERSION.DEBUG_04)
            {
                this.ReadColor(reader, ref this.mainColor1);
                return true;
            }
            COLOR_TYPE colorType = Load_ColorType(reader, version);
            switch (colorType)
            {
                case COLOR_TYPE.NONE:
                    return false;
                case COLOR_TYPE.PBR1:
                    this.ReadColor(reader, ref this.mainColor1);
                    this.ReadColor(reader, ref this.specColor1);
                    this.specular1 = reader.ReadSingle();
                    this.smooth1 = reader.ReadSingle();
                    return true;
                default:
                    if (colorType != this.GetColorType())
                    {
                        Debug.LogError("色タイプが違う");
                        goto case COLOR_TYPE.PBR1;
                    }
                    else
                        goto case COLOR_TYPE.PBR1;
            }
        }

        public void FromSexyData(HSColorSet colorSet)
        {
            this.mainColor1 = colorSet.rgbaDiffuse;
            this.specColor1 = colorSet.rgbSpecular;
            this.specular1 = colorSet.specularIntensity;
            this.smooth1 = colorSet.specularSharpness;
        }
    }
}

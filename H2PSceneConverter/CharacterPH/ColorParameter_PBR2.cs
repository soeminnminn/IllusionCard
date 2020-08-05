using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class ColorParameter_PBR2 : ColorParameter_Base
    {
        public Color mainColor1 = Color.white;
        public Color specColor1 = Color.white;
        public Color mainColor2 = Color.white;
        public Color specColor2 = Color.white;
        public float specular1;
        public float smooth1;
        public float specular2;
        public float smooth2;

        public ColorParameter_PBR2()
        {
        }

        public ColorParameter_PBR2(ColorParameter_PBR2 src)
        {
            this.mainColor1 = src.mainColor1;
            this.specColor1 = src.specColor1;
            this.specular1 = src.specular1;
            this.smooth1 = src.smooth1;
            this.mainColor2 = src.mainColor2;
            this.specColor2 = src.specColor2;
            this.specular2 = src.specular2;
            this.smooth2 = src.smooth2;
        }

        public override COLOR_TYPE GetColorType()
        {
            return COLOR_TYPE.PBR2;
        }

        public override void Save(BinaryWriter writer)
        {
            this.Save_ColorType(writer);
            this.WriteColor(writer, this.mainColor1);
            this.WriteColor(writer, this.specColor1);
            writer.Write(this.specular1);
            writer.Write(this.smooth1);
            this.WriteColor(writer, this.mainColor2);
            this.WriteColor(writer, this.specColor2);
            writer.Write(this.specular2);
            writer.Write(this.smooth2);
        }

        public override bool Load(BinaryReader reader, CUSTOM_DATA_VERSION version)
        {
            if (version < CUSTOM_DATA_VERSION.DEBUG_04)
            {
                this.ReadColor(reader, ref this.mainColor1);
                return true;
            }
            COLOR_TYPE colorType = Load_ColorType(reader, version);
            if (colorType == COLOR_TYPE.NONE)
                return false;
            if (colorType != this.GetColorType())
                Debug.LogError("色タイプが違う");
            this.ReadColor(reader, ref this.mainColor1);
            this.ReadColor(reader, ref this.specColor1);
            this.specular1 = reader.ReadSingle();
            this.smooth1 = reader.ReadSingle();
            this.ReadColor(reader, ref this.mainColor2);
            this.ReadColor(reader, ref this.specColor2);
            this.specular2 = version >= CUSTOM_DATA_VERSION.DEBUG_05 ? reader.ReadSingle() : 0.0f;
            this.smooth2 = reader.ReadSingle();
            return true;
        }

        public void FromSexyData(HSColorSet colorSet1, HSColorSet colorSet2)
        {
            this.mainColor1 = colorSet1.rgbaDiffuse;
            this.specColor1 = colorSet1.rgbSpecular;
            this.specular1 = colorSet1.specularIntensity;
            this.smooth1 = colorSet1.specularSharpness;
            this.mainColor2 = colorSet2.rgbaDiffuse;
            this.specColor2 = colorSet2.rgbSpecular;
            this.specular2 = colorSet2.alpha;
            this.smooth2 = colorSet2.specularSharpness;
        }

        public void Copy(ColorParameter_PBR2 source)
        {
            this.mainColor1 = source.mainColor1;
            this.specColor1 = source.specColor1;
            this.specular1 = source.specular1;
            this.smooth1 = source.smooth1;
            this.mainColor2 = source.mainColor2;
            this.specColor2 = source.specColor2;
            this.specular2 = source.specular2;
            this.smooth2 = source.smooth2;
        }
    }
}

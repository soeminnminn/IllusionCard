using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class ColorParameter_Hair : ColorParameter_Base
    {
        public Color mainColor = Color.black;
        public Color cuticleColor = new Color(0.75f, 0.75f, 0.75f);
        public float cuticleExp = 6f;
        public Color fresnelColor = new Color(0.75f, 0.75f, 0.75f);
        public float fresnelExp = 0.3f;

        public ColorParameter_Hair()
        {
        }

        public ColorParameter_Hair(
          Color mainColor,
          Color cuticleColor,
          float cuticleExp,
          Color fresnelColor,
          float fresnelExp)
        {
            this.mainColor = mainColor;
            this.cuticleColor = cuticleColor;
            this.cuticleExp = cuticleExp;
            this.fresnelColor = fresnelColor;
            this.fresnelExp = fresnelExp;
        }

        public ColorParameter_Hair(ColorParameter_Hair copy)
        {
            this.mainColor = copy.mainColor;
            this.cuticleColor = copy.cuticleColor;
            this.cuticleExp = copy.cuticleExp;
            this.fresnelColor = copy.fresnelColor;
            this.fresnelExp = copy.fresnelExp;
        }

        public override COLOR_TYPE GetColorType()
        {
            return COLOR_TYPE.HAIR;
        }

        public override void Save(BinaryWriter writer)
        {
            this.Save_ColorType(writer);
            this.WriteColor(writer, this.mainColor);
            this.WriteColor(writer, this.cuticleColor);
            writer.Write(this.cuticleExp);
            this.WriteColor(writer, this.fresnelColor);
            writer.Write(this.fresnelExp);
        }

        public override bool Load(BinaryReader reader, CUSTOM_DATA_VERSION version)
        {
            if (version < CUSTOM_DATA_VERSION.DEBUG_04)
            {
                this.ReadColor(reader, ref this.mainColor);
                return true;
            }
            if (Load_ColorType(reader, version) != this.GetColorType())
                Debug.LogError("色タイプが違う");
            this.ReadColor(reader, ref this.mainColor);
            this.ReadColor(reader, ref this.cuticleColor);
            this.cuticleExp = reader.ReadSingle();
            this.ReadColor(reader, ref this.fresnelColor);
            this.fresnelExp = reader.ReadSingle();
            return true;
        }

        public void FromSexyData(HSColorSet colorSet)
        {
            this.mainColor = colorSet.rgbaDiffuse;
            this.cuticleColor = colorSet.rgbSpecular;
            this.fresnelColor = colorSet.rgbSpecular;
            this.cuticleExp = 6f;
            this.fresnelExp = 0.3f;
        }
    }
}

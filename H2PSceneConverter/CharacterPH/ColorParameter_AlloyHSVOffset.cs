using System;
using System.IO;
using UnityEngine;

namespace CharacterPH
{
    public class ColorParameter_AlloyHSVOffset : ColorParameter_Base
    {
        public float offset_s = 1f;
        public float offset_v = 1f;
        public float alpha = 1f;
        public float smooth = 0.562f;
        public float offset_h;
        public bool hasAlpha;
        public float metallic;

        public ColorParameter_AlloyHSVOffset()
        {
        }

        public ColorParameter_AlloyHSVOffset(bool hasAlpha, float alpha)
        {
            this.hasAlpha = hasAlpha;
            this.alpha = alpha;
        }

        public ColorParameter_AlloyHSVOffset(ColorParameter_AlloyHSVOffset copy)
        {
            this.offset_h = copy.offset_h;
            this.offset_s = copy.offset_s;
            this.offset_v = copy.offset_v;
            this.hasAlpha = copy.hasAlpha;
            this.alpha = copy.alpha;
            this.metallic = copy.metallic;
            this.smooth = copy.smooth;
        }

        public override COLOR_TYPE GetColorType()
        {
            return COLOR_TYPE.ALLOY_HSV;
        }

        public void Init(bool hasAlpha = false, float alpha = 1f)
        {
            this.offset_h = 0.0f;
            this.offset_s = 1f;
            this.offset_v = 1f;
            this.hasAlpha = hasAlpha;
            this.alpha = alpha;
            this.metallic = 0.0f;
            this.smooth = 0.562f;
        }

        public override void Save(BinaryWriter writer)
        {
            this.Save_ColorType(writer);
            writer.Write(this.offset_h);
            writer.Write(this.offset_s);
            writer.Write(this.offset_v);
            writer.Write(this.alpha);
            writer.Write(this.metallic);
            writer.Write(this.smooth);
        }

        public override bool Load(BinaryReader reader, CUSTOM_DATA_VERSION version)
        {
            Color white = Color.white;
            if (version < CUSTOM_DATA_VERSION.DEBUG_04)
            {
                this.ReadColor(reader, ref white);
                return true;
            }
            COLOR_TYPE colorType = Load_ColorType(reader, version);
            if (colorType == COLOR_TYPE.NONE)
                return false;
            if (colorType != this.GetColorType())
            {
                bool flag = false;
                if (version < CUSTOM_DATA_VERSION.DEBUG_06 && colorType == COLOR_TYPE.ALLOY)
                    flag = true;
                else if (colorType == COLOR_TYPE.ALLOY_HSV)
                    flag = true;
                if (!flag)
                    Debug.LogError("色タイプが違う");
            }
            if (version < CUSTOM_DATA_VERSION.DEBUG_06)
            {
                this.ReadColor(reader, ref white);
            }
            else
            {
                this.offset_h = reader.ReadSingle();
                this.offset_s = reader.ReadSingle();
                this.offset_v = reader.ReadSingle();
                if (version == CUSTOM_DATA_VERSION.DEBUG_07)
                {
                    reader.ReadBoolean();
                    this.alpha = reader.ReadSingle();
                }
                else if (version >= CUSTOM_DATA_VERSION.TRIAL)
                    this.alpha = reader.ReadSingle();
            }
            this.metallic = reader.ReadSingle();
            this.smooth = reader.ReadSingle();
            return true;
        }

        public void FromSexyData(HSColorSet colorSet)
        {
        }
    }
}

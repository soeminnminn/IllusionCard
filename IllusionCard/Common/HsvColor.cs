// Decompiled with JetBrains decompiler
// Type: HsvColor
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using UnityEngine;

[Serializable]
public class HsvColor
{
    private float _h;
    private float _s;
    private float _v;

    public HsvColor(float hue, float saturation, float brightness)
    {
        if (hue < 0.0 || 360.0 < hue)
            throw new ArgumentException("hueは0~360の値です。", nameof(hue));
        if (saturation < 0.0 || 1.0 < saturation)
            throw new ArgumentException("saturationは0以上1以下の値です。", nameof(saturation));
        if (brightness < 0.0 || 1.0 < brightness)
            throw new ArgumentException("brightnessは0以上1以下の値です。", nameof(brightness));
        this._h = hue;
        this._s = saturation;
        this._v = brightness;
    }

    public HsvColor(HsvColor src)
    {
        this._h = src._h;
        this._s = src._s;
        this._v = src._v;
    }

    public float H
    {
        get
        {
            return this._h;
        }
        set
        {
            this._h = value;
        }
    }

    public float S
    {
        get
        {
            return this._s;
        }
        set
        {
            this._s = value;
        }
    }

    public float V
    {
        get
        {
            return this._v;
        }
        set
        {
            this._v = value;
        }
    }

    public void Copy(HsvColor src)
    {
        this._h = src._h;
        this._s = src._s;
        this._v = src._v;
    }

    public static HsvColor FromRgb(Color rgb)
    {
        float r = rgb.r;
        float g = rgb.g;
        float b = rgb.b;
        float num1 = Math.Max(r, Math.Max(g, b));
        float num2 = Math.Min(r, Math.Min(g, b));
        float hue = 0.0f;
        if (num1 == (double)num2)
            hue = 0.0f;
        else if (num1 == (double)r)
            hue = (float)((60.0 * (g - (double)b) / (num1 - (double)num2) + 360.0) % 360.0);
        else if (num1 == (double)g)
            hue = (float)(60.0 * (b - (double)r) / (num1 - (double)num2) + 120.0);
        else if (num1 == (double)b)
            hue = (float)(60.0 * (r - (double)g) / (num1 - (double)num2) + 240.0);
        float saturation = num1 != 0.0 ? (num1 - num2) / num1 : 0.0f;
        float brightness = num1;
        return new HsvColor(hue, saturation, brightness);
    }

    public static Color ToRgb(float h, float s, float v)
    {
        return HsvColor.ToRgb(new HsvColor(h, s, v));
    }

    public static Color ToRgb(HsvColor hsv)
    {
        float v = hsv.V;
        float s = hsv.S;
        float r;
        float g;
        float b;
        if (s == 0.0)
        {
            r = v;
            g = v;
            b = v;
        }
        else
        {
            double d;
            int num1 = (int)Math.Floor(d = hsv.H / 60.0) % 6;
            float num2 = (float)d - (float)Math.Floor(d);
            float num3 = v * (1f - s);
            float num4 = v * (float)(1.0 - s * (double)num2);
            float num5 = v * (float)(1.0 - s * (1.0 - num2));
            switch (num1)
            {
                case 0:
                    r = v;
                    g = num5;
                    b = num3;
                    break;
                case 1:
                    r = num4;
                    g = v;
                    b = num3;
                    break;
                case 2:
                    r = num3;
                    g = v;
                    b = num5;
                    break;
                case 3:
                    r = num3;
                    g = num4;
                    b = v;
                    break;
                case 4:
                    r = num5;
                    g = num3;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = num3;
                    b = num4;
                    break;
                default:
                    throw new ArgumentException("色相の値が不正です。", nameof(hsv));
            }
        }
        return new Color(r, g, b);
    }

    public static Color ToRgba(HsvColor hsv, float alpha)
    {
        Color rgb = HsvColor.ToRgb(hsv);
        rgb.a = alpha;
        return rgb;
    }
}

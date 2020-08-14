// Decompiled with JetBrains decompiler
// Type: HSColorSet
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.IO;
using UnityEngine;

[Serializable]
public class HSColorSet
{
    public HsvColor hsvDiffuse = new HsvColor(19f, 0.07f, 0.63f);
    public float alpha = 1f;
    public HsvColor hsvSpecular = new HsvColor(0.0f, 0.0f, 0.8f);
    public float specularIntensity = 0.1f;
    public float specularSharpness = 0.33f;
    public const int SaveVersion = 1;

    public Color rgbaDiffuse
    {
        get
        {
            return HsvColor.ToRgba(this.hsvDiffuse, this.alpha);
        }
    }

    public Color rgbDiffuse
    {
        get
        {
            return HsvColor.ToRgb(this.hsvDiffuse);
        }
    }

    public Color rgbSpecular
    {
        get
        {
            return HsvColor.ToRgb(this.hsvSpecular);
        }
    }

    public static bool CheckSameColor(
      HSColorSet a,
      HSColorSet b,
      bool hsv,
      bool alpha,
      bool specular)
    {
        return (!alpha || (double)a.alpha == (double)b.alpha) && (!hsv || (double)a.hsvDiffuse.H == (double)b.hsvDiffuse.H && (double)a.hsvDiffuse.S == (double)b.hsvDiffuse.S && (double)a.hsvDiffuse.V == (double)b.hsvDiffuse.V) && (!specular || (double)a.specularIntensity == (double)b.specularIntensity && (double)a.specularSharpness == (double)b.specularSharpness && ((double)a.hsvSpecular.H == (double)b.hsvSpecular.H && (double)a.hsvSpecular.S == (double)b.hsvSpecular.S) && (double)a.hsvSpecular.V == (double)b.hsvSpecular.V);
    }

    public void SetDiffuseRGBA(Color rgba)
    {
        this.hsvDiffuse = HsvColor.FromRgb(rgba);
        this.alpha = rgba.a;
    }

    public void SetDiffuseRGB(Color rgb)
    {
        this.hsvDiffuse = HsvColor.FromRgb(rgb);
        this.alpha = 1f;
    }

    public void SetSpecularRGB(Color rgb)
    {
        this.hsvSpecular = HsvColor.FromRgb(rgb);
    }

    public void Copy(HSColorSet src)
    {
        this.hsvDiffuse = new HsvColor(src.hsvDiffuse.H, src.hsvDiffuse.S, src.hsvDiffuse.V);
        this.alpha = src.alpha;
        this.hsvSpecular = new HsvColor(src.hsvSpecular.H, src.hsvSpecular.S, src.hsvSpecular.V);
        this.specularIntensity = src.specularIntensity;
        this.specularSharpness = src.specularSharpness;
    }

    public void BlendHSV(HSColorSet c01, HSColorSet c02, float rate)
    {
        this.hsvDiffuse = new HsvColor(Mathf.Lerp(c01.hsvDiffuse.H, c02.hsvDiffuse.H, rate), Mathf.Lerp(c01.hsvDiffuse.S, c02.hsvDiffuse.S, rate), Mathf.Lerp(c01.hsvDiffuse.V, c02.hsvDiffuse.V, rate));
        this.alpha = Mathf.Lerp(c01.alpha, c02.alpha, rate);
        this.hsvSpecular = new HsvColor(Mathf.Lerp(c01.hsvSpecular.H, c02.hsvSpecular.H, rate), Mathf.Lerp(c01.hsvSpecular.S, c02.hsvSpecular.S, rate), Mathf.Lerp(c01.hsvSpecular.V, c02.hsvSpecular.V, rate));
        this.specularIntensity = Mathf.Lerp(c01.specularIntensity, c02.specularIntensity, rate);
        this.specularSharpness = Mathf.Lerp(c01.specularSharpness, c02.specularSharpness, rate);
    }

    public void BlendRGB(HSColorSet c01, HSColorSet c02, float rate)
    {
        Color rgb1 = HsvColor.ToRgb(c01.hsvDiffuse);
        Color rgb2 = HsvColor.ToRgb(c02.hsvDiffuse);
        Color rgb3 = new Color(Mathf.Lerp(rgb1.r, rgb2.r, rate), Mathf.Lerp(rgb1.g, rgb2.g, rate), Mathf.Lerp(rgb1.b, rgb2.b, rate));
        this.hsvDiffuse = HsvColor.FromRgb(rgb3);
        this.alpha = Mathf.Lerp(c01.alpha, c02.alpha, rate);
        Color rgb4 = HsvColor.ToRgb(c01.hsvSpecular);
        Color rgb5 = HsvColor.ToRgb(c02.hsvSpecular);
        rgb3 = new Color(Mathf.Lerp(rgb4.r, rgb5.r, rate), Mathf.Lerp(rgb4.g, rgb5.g, rate), Mathf.Lerp(rgb4.b, rgb5.b, rate));
        this.hsvSpecular = HsvColor.FromRgb(rgb3);
        this.specularIntensity = Mathf.Lerp(c01.specularIntensity, c02.specularIntensity, rate);
        this.specularSharpness = Mathf.Lerp(c01.specularSharpness, c02.specularSharpness, rate);
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write((double)this.hsvDiffuse.H);
        writer.Write((double)this.hsvDiffuse.S);
        writer.Write((double)this.hsvDiffuse.V);
        writer.Write((double)this.alpha);
        writer.Write((double)this.hsvSpecular.H);
        writer.Write((double)this.hsvSpecular.S);
        writer.Write((double)this.hsvSpecular.V);
        writer.Write((double)this.specularIntensity);
        writer.Write((double)this.specularSharpness);
    }

    public void Load(BinaryReader reader, int version)
    {
        // 72
        this.hsvDiffuse.H = (float)reader.ReadDouble();
        this.hsvDiffuse.S = (float)reader.ReadDouble();
        this.hsvDiffuse.V = (float)reader.ReadDouble();
        this.alpha = (float)reader.ReadDouble();
        this.hsvSpecular.H = (float)reader.ReadDouble();
        this.hsvSpecular.S = (float)reader.ReadDouble();
        this.hsvSpecular.V = (float)reader.ReadDouble();
        this.specularIntensity = (float)reader.ReadDouble();
        this.specularSharpness = (float)reader.ReadDouble();
    }
}

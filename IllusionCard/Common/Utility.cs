using System;
using System.IO;
using UnityEngine;

internal static class Utility
{
    public static void SaveColor(BinaryWriter _writer, Color _color)
    {
        _writer.Write(_color.r);
        _writer.Write(_color.g);
        _writer.Write(_color.b);
        _writer.Write(_color.a);
    }

    public static Color LoadColor(BinaryReader _reader)
    {
        // 16
        Color color;
        color.r = _reader.ReadSingle();
        color.g = _reader.ReadSingle();
        color.b = _reader.ReadSingle();
        color.a = _reader.ReadSingle();
        return color;
    }

    public static Color ConvertColor(int _r, int _g, int _b)
    {
        var sysColor = System.Drawing.Color.FromArgb(_r, _g, _b);
        return new Color(sysColor.R / (float)byte.MaxValue, sysColor.G / (float)byte.MaxValue, sysColor.B / (float)byte.MaxValue, sysColor.A / (float)byte.MaxValue);
    }
}

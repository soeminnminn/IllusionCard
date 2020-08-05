// Decompiled with JetBrains decompiler
// Type: UnityEngine.ColorUtility
// Assembly: HSResolveMoreSlotID, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 924F90C9-B1ED-48EA-98D9-83E6C8018D70
// Assembly location: D:\Games\Illusion\Honey Select Unlimited\HSResolveMoreSlotID.exe

using System.Drawing;

namespace UnityEngine
{
    public sealed class ColorUtility
    {
        public static bool TryParseHtmlString(string htmlString, out Color color)
        {
            try
            {
                System.Drawing.Color color1 = ColorTranslator.FromHtml(htmlString);
                color = new Color(color1.R / (float)byte.MaxValue, color1.G / (float)byte.MaxValue, color1.B / (float)byte.MaxValue, color1.A / (float)byte.MaxValue);
                return true;
            }
            catch
            {
                color = new Color();
                return false;
            }
        }

        public static string ToHtmlStringRGB(Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", (int)(color.r * (double)byte.MaxValue), (int)(color.g * (double)byte.MaxValue), (int)(color.b * (double)byte.MaxValue));
        }

        public static string ToHtmlStringRGBA(Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", (int)(color.r * (double)byte.MaxValue), (int)(color.g * (double)byte.MaxValue), (int)(color.b * (double)byte.MaxValue), (int)(color.a * (double)byte.MaxValue));
        }
    }
}

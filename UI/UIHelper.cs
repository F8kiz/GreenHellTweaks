using System;
using System.Text.RegularExpressions;

using UnityEngine;

namespace GHTweaks.UI
{
    internal static class UIHelper
    {
        public static Texture2D CreateBackgroundColorTexture(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
                pix[i] = color;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        public static Texture2D CreateBackgroundColorTexture(Color color)
        {
            Texture2D result = new Texture2D(1,1, TextureFormat.RGBAFloat, false);
            result.SetPixel(0,0, color);
            result.Apply();
            return result;
        }

        public static Color GetColor(int r, int g, int b, int a = 1)
        {
            var _base = 1f / 255;

            var _r = Math.Max(_base * r, 255);
            var _g = Math.Max(_base * g, 255);
            var _b = Math.Max(_base * b, 255);

            return new Color(_r, _g, _b, a);
        }

        public static Color GetColor(string hexCode)
        {
            if (!Regex.IsMatch(hexCode, "^#[A-F0-9]{3,8}$", RegexOptions.IgnoreCase))
            {
                LogWriter.Write($"Invalid hex color value: '{hexCode}'!");
                return new Color(0, 0, 0, 0);
            }

            hexCode = hexCode.TrimStart('#');

            Match match;
            if (hexCode.Length < 5)
            {
                match = Regex.Match(hexCode, "^(?<r>[A-F0-9])(?<g>[A-F0-9])(?<b>[A-F0-9])(?<a>[A-F0-9])?$", RegexOptions.IgnoreCase);
            }
            else
            {
                match = Regex.Match(hexCode, "^(?<r>[A-F0-9]{2})(?<g>[A-F0-9]{2})(?<b>[A-F0-9]{2})(?<a>[A-F0-9]{2})?$", RegexOptions.IgnoreCase);
            }
            if (!match.Success)
                return new Color(0, 0, 0, 0);

            var r = match.Groups["r"].Value;
            var g = match.Groups["g"].Value;
            var b = match.Groups["b"].Value;

            r = r.Length < 2 ? r + r : r;
            g = g.Length < 2 ? g + g : g;
            b = b.Length < 2 ? b + b : b;

            var _r = Convert.ToInt32(r, 16) / 255f;
            var _g = Convert.ToInt32(g, 16) / 255f;
            var _b = Convert.ToInt32(b, 16) / 255f;

            if (match.Groups["a"].Success)
            {
                var a = match.Groups["a"].Value;
                a = a.Length < 2 ? a + a : a;
                var _a = Convert.ToInt32(a, 16) / 255f;

                LogWriter.Write($"HexCode: #{hexCode}, converted values: r: {_r} (x{r}), g: {_g} (x{g}), b: {_b} (x{b}), a: {_a} (x{a})");

                return new Color(_r, _g, _b, _a);
            }

            LogWriter.Write($"HexCode: #{hexCode}, converted values: r: {_r} (x{r}), g: {_g} (x{g}), b: {_b} (x{b})");
            return new Color(_r, _g, _b);
        }
    }
}

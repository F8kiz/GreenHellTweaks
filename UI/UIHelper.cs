using GHTweaks.UI.Console;
using GHTweaks.UI.Console.Command.Core;

using System;
using System.Collections;
using System.Reflection;
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

        public static string GetMemberInfoString(MemberInfo mi, object instance)
        {
            static string getArrayLengthString(int length)
            {
                if (length < 1)
                    return "(0 Items)";

                if (length == 1)
                    return "(1 item, [0])";

                return $"({length} items, [0 - {length - 1}])";
            }

            static string convertValueToString(object value, bool isReadOnly, string modifier, string memberName)
            {
                var readOnly = isReadOnly ? $" {"readonly".ToColoredRTF(Style.TextColor.AccessModifier)}" : "";
                if (value == null)
                    return $"{Style.LineIndent}{modifier}{readOnly} {memberName} = " + "null".ToColoredRTF(Style.TextColor.NullValue);

                if (value is IList lst)
                {
                    var typeString = $"{value.GetType().Name}".ToColoredRTF(Style.GetTypeColor(value.GetType()));
                    var range = getArrayLengthString(lst.Count);
                    return $"{Style.LineIndent}{modifier}{readOnly} {memberName} : {typeString} {range}";
                }
                
                if (value is Array array)
                {
                    var typeString = $"{value.GetType().Name}[]".ToColoredRTF(Style.GetTypeColor(value.GetType()));
                    var range = getArrayLengthString(array.Length);
                    return $"{Style.LineIndent}{modifier}{readOnly} {memberName} : {typeString} {range}";
                }

                return $"{Style.LineIndent}{modifier}{readOnly} {memberName} : {value.GetType().Name.ToColoredRTF(Style.GetTypeColor(value.GetType()))} = {value.ToCodeRTF()}";
            }


            if (mi is PropertyInfo pi)
            {
                var modifier = (pi.GetSetMethod()?.IsStatic ?? false) ? "public static" : "public";
                modifier = modifier.ToColoredRTF(Style.TextColor.AccessModifier);
                object value = instance != null ? pi.GetValue(instance, null) : null;

                //
                // pi.CanWrite seems to be buggy, maybe it's better to use a PropertyDescriptor?
                //
                return convertValueToString(value, !(pi.CanWrite || pi.SetMethod != null), modifier, pi.Name);
            }

            if (mi is FieldInfo fi)
            {
                var modifier = fi.IsStatic ? "private static" : "private";
                modifier = modifier.ToColoredRTF(Style.TextColor.AccessModifier);
                object value = instance != null ? fi.GetValue(instance) : null;

                return convertValueToString(value, fi.IsInitOnly, modifier, fi.Name);
            }

            return mi.ToString();
        }

        public static string GetMemberInfoString(MemberInfo mi)
        {
            if (mi is PropertyInfo pi)
            {
                var readOnly = pi.CanRead ? "" : " readonly".ToColoredRTF(Style.TextColor.Error);
                var modifier = (pi.GetSetMethod()?.IsStatic ?? false) ? "public static" : "public";
                modifier = modifier.ToColoredRTF(Style.TextColor.AccessModifier);
                var acceptedTypeInfo = AssemblyHelper.GetFriendlyTypeInformation(pi.PropertyType).ToColoredRTF(Style.GetTypeColor(pi.PropertyType));

                return $"{modifier}{readOnly} {pi.Name.ToShortTypeName()} :: {acceptedTypeInfo}";
            }

            if (mi is FieldInfo fi)
            {
                var readOnly = fi.IsInitOnly ? " readonly".ToColoredRTF(Style.TextColor.Error) : "";
                var modifier = fi.IsStatic ? "private static" : "private";
                modifier = modifier.ToColoredRTF(Style.TextColor.AccessModifier);
                var acceptedTypeInfo = AssemblyHelper.GetFriendlyTypeInformation(fi.FieldType).ToColoredRTF(Style.GetTypeColor(fi.FieldType));

                return $"{modifier}{readOnly} {fi.Name} :: {acceptedTypeInfo}";
            }

            return mi.ToString();
        }
    }
}

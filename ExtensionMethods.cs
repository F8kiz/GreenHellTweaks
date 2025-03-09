using GHTweaks.UI.Console;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using UnityEngine;

namespace GHTweaks
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts the given boolean into an known string.
        /// </summary>
        /// <param name="b">The boolean that should be converted.</param>
        /// <returns>Enabled if the bool has the value TRUE, otherwise disabled.</returns>
        public static string ToKnownState(this bool b) => b ? "enabled" : "disabled";

        public static bool InRange(this float value, float min, float max) => value >= min && value <= max;

        public static bool IsLowerOrGreaterThan(this float value, float range) => value <= range || value >= range;

        public static bool IsNewLine(this string str) => str == "\n" || str == Environment.NewLine;

        public static float ToScrollPositionY(this float contentHeight)
        {
            // 3.963302752293578 is the content scale 
            return Math.Max(0, contentHeight / 3.963302752293578f);
        }

        public static string ToColoredRTF(this string str, string color, bool deepSearch = false)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (deepSearch)
                str = Regex.Replace(str, "<color=[^>]*>|</color>", "", RegexOptions.Singleline);

            return $"<color={color}>{str}</color>";
        }

        public static string ToColoredRTF(this object obj, string color) => obj?.ToString().ToColoredRTF(color);

        public static string ToCodeRTF(this object obj)
        {
            var type = obj.GetType();

            string color = Style.GetTypeColor(type);
            if (type == typeof(string))
                color = Style.TextColor.StringType;
            else if (type.IsClass)
                color = Style.TextColor.ClassType;
            else if (type.IsPrimitive)
                color = Style.TextColor.PrimitiveType;
            else
                color = Style.TextColor.Code;

            return $"<color={color}>{obj}</color>";
        }

        public static string ToBoldCodeRTF(this string str)
            => string.IsNullOrEmpty(str) ? str : $"<color={Style.TextColor.Code}><b>{str}</b></color>";

        public static string ToSyntaxHighlightedRTF(this string str, bool lastTypeIsClass = false) => ToSyntaxHighlightedRTF(str, null, lastTypeIsClass, ":", false);

        public static string ToSyntaxHighlightedRTF(this string str, object value, bool lastTypeIsClass = false, string assignChar = ":", bool printNullValues = true)
        {
            var p1 = str.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            var types = p1[0].Split('.');
            var buffer = new List<string>();

            if (lastTypeIsClass)
            {
                for (int i = 0; i < types.Length; ++i)
                    buffer.Add($"<color={Style.TextColor.ClassType}>{types[i]}</color>");
            }
            else
            {
                for (int i = 0; i < types.Length - 1; ++i)
                    buffer.Add($"<color={Style.TextColor.ClassType}>{types[i]}</color>");

                buffer.Add($"<color={Style.TextColor.PropertyMember}>{types.Last()}</color>");
            }
            str = string.Join(".", buffer);

            if (p1.Length > 1)
                str += $" :: {p1[1]}";

            if (value == null)
            {
                if (printNullValues)
                    str += $" {assignChar} {"null".ToColoredRTF(Style.TextColor.NullValue)}";
            }
            else
            {
                str += $" {assignChar} {value.ToCodeRTF()}";
            }
            return str;
        }

        public static string ToShortTypeName(this string str) => Regex.Replace(str, "^.*?\\.([^.]+)$", "$1");
        



        public static bool IsLowerThan(this Vector2 _this, Vector2 other)
            => other.x > _this.x || other.y > _this.y;

        public static bool IsSameCommandMap(this Dictionary<string, string[]> _this, Dictionary<string, string[]> other)
        {
            if (_this.Count != other.Count)
                return false;

            foreach (var kvp in _this)
            {
                if (!other.TryGetValue(kvp.Key, out string[] otherAcceptedArgs))
                    return false;

                if (kvp.Value == null && otherAcceptedArgs == null)
                    continue;

                if ((kvp.Value == null && otherAcceptedArgs != null) || (kvp.Value != null) && otherAcceptedArgs == null)
                    return false;

                var missingArgs = kvp.Value.FirstOrDefault(x => !otherAcceptedArgs.Contains(x));
                if (!string.IsNullOrEmpty(missingArgs))
                    return false;
            }
            return true;
        }

        public static string TrimStart(this string str, string trim)
        {
            trim = trim.Replace(".", "\\.");
            return Regex.Replace(str, $"^{trim}", "", RegexOptions.IgnoreCase);
        }

        public static string TrimEnd(this string str, string trim)
        {
            trim = trim.Replace(".", "\\.");
            return Regex.Replace(str, $"{trim}$", "", RegexOptions.IgnoreCase);
        }

        /// <returns>
        /// Returns True if the current string contains an array index or a dictionary key, otherwise false.<br/>
        /// E.g. Returns true if the string contains:
        /// <code>
        /// Foo.Bar[1]       // Index
        /// Foo.Bar[someKey] // Key
        /// </code>
        /// </returns>
        public static bool HasSomeKindOfIndex(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            foreach (var item in str.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Regex.IsMatch(item, @"[\w._]+\[\w+](\.[\w._]+)?$"))
                    return true;
            }
            return false;
        }

        public static string RemoveAllKindsOfIndex(this string str) => Regex.Replace(str, @"\[\w+]", "");

        public static int[] GetIndexer(this string str)
            => Regex.Matches(str, @"\[(\w+)]").Cast<Match>().Select(m => int.TryParse(m.Groups[1].Value, out int i) ? i : -1).Where(i => i > -1).ToArray();

        public static object GetValue(this MemberInfo mi, object parentInstance)
        {
            if (parentInstance == null)
                return null;

            if (mi.MemberType == MemberTypes.Property)
                return ((PropertyInfo)mi).GetValue(parentInstance);
            
            if (mi.MemberType == MemberTypes.Field)
                return ((FieldInfo)mi).GetValue(parentInstance);

            return null;
        }
    }
}

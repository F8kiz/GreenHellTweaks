using GHTweaks.UI.Console;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;

namespace GHTweaks
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts the given boolean into an known string.
        /// </summary>
        /// <param name="b">The boolean which should be converted.</param>
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

        public static string ToColoredRTF(this string str, string color)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var match = Regex.Match(str, "^<color=(?<color>[^>]*)>");
            if (match.Success)
            {
                var nStr = str.Replace(match.Groups[1].Value, color);

                LogWriter.Write($"Update RTF color. OldValue: {str}; NewValue: {nStr}");
                return nStr;
            }

            return string.IsNullOrEmpty(str) || string.IsNullOrEmpty(color) ? str : $"<color={color}>{str}</color>";
        }

        public static string ToCodeRTF(this string str)
            => string.IsNullOrEmpty(str) ? str : $"<color={Style.TextColor.Code}><b>{str}</b></color>";

        public static string ToBoldRTF(this string str)
             => string.IsNullOrEmpty(str) ? str : $"<b>{str}</b>";

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
    }
}

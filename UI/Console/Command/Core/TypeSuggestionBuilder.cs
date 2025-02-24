using GHTweaks.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GHTweaks.UI.Console.Command.Core
{
    internal static class TypeSuggestionBuilder
    {
        internal enum MemberType
        {
            Field = 0,
            Property = 16
        }

        internal struct TypeSuggestion
        {
            public string MemberName;
            public string MemberPath;
            public string MemberTypeName;
            public string AcceptedArgumentType;
            public bool IsClass;
            public bool IsPublic;
            public bool IsStatic;
            public BindingFlags? BindingFlags;
            public MemberType MemberType;


            public readonly string GetAccessModifier() => (IsPublic ? "public" : "private") + (IsStatic ? " static" : "");

            public readonly string GetFullName() => $"{MemberPath}.{MemberName}";

            public override readonly string ToString() => $"{MemberPath}.{MemberName} :: {GetAccessModifier()} {MemberTypeName}";          
        }


        public static List<TypeSuggestion> GetTypeSuggestionsForPath(string path, string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                if (path.StartsWith("config", StringComparison.OrdinalIgnoreCase))
                    alias = "Config";
                else if (path.StartsWith("game", StringComparison.OrdinalIgnoreCase))
                    alias = "Game";
            }

            if (path.StartsWith(alias, StringComparison.OrdinalIgnoreCase))
                path = Regex.Replace(path, $"^{alias}\\.?", "", RegexOptions.IgnoreCase);

            const StringComparison strComparsion = StringComparison.OrdinalIgnoreCase;
            var buffer = new List<TypeSuggestion>();
            if (string.IsNullOrEmpty(path))
            {
                if (alias.ToLower() == "config")
                    buffer.AddRange(typeof(Config).GetProperties(AssemblyHelper.BINDING_FLAGS_CONFIG).Select(x => ConvertToTypeSuggestion(x, alias)));
                else
                    buffer.AddRange(AssemblyHelper.GetSingleTonGameTypes().Select(x => ConvertToTypeSuggestion(x, alias)));
                return buffer;
            }

            var memberNames = path.Split('.');
            var matchingTypes = alias.ToLower() == "game"
                ? AssemblyHelper.GetMatchingSingleTonGameTypes(path.EndsWith(".") ? $"^{memberNames[0]}$" : $"^{memberNames[0]}").ToArray()
                : typeof(Config).GetProperties(AssemblyHelper.BINDING_FLAGS_CONFIG)
                    .Where(x => x.Name.StartsWith(memberNames[0], strComparsion))
                    .Select(a => a.PropertyType)
                    .ToArray();

            if (matchingTypes == null)
                return new List<TypeSuggestion>();

            if (memberNames.Length == 1)
                return matchingTypes.Select(x => ConvertToTypeSuggestion(x, alias)).ToList();

            foreach(var item in matchingTypes)
            {
                // Find parent member type
                var parentType = item;
                var cPath = $"{alias}.{parentType.Name}";
                for (var i = 1; i < memberNames.Length - 1; ++i)
                {
                    var property = parentType.GetProperty(memberNames[i], AssemblyHelper.BINDING_FLAGS_GAME);
                    if (property == null)
                        break;

                    parentType = property.PropertyType;
                    cPath = $"{cPath}.{property.Name}".Trim('.');
                }

                if (path.EndsWith("."))
                {
                    // If the path ends with a dot we need to print all the members of the parent type.
                    buffer.AddRange(parentType.GetProperties(AssemblyHelper.BINDING_FLAGS_GAME).Select(x => ConvertToTypeSuggestion(x, cPath)));
                }
                else
                {
                    // Collect all member that starts with typeNames.Last();
                    var search = memberNames.Last();
                    buffer.AddRange(parentType.GetProperties(AssemblyHelper.BINDING_FLAGS_GAME)
                        .Where(prop => prop.Name.StartsWith(search, strComparsion))
                        .Select(x => ConvertToTypeSuggestion(x, cPath)));
                }
            }
            return buffer;
        }


        private static List<TypeSuggestion> GetPropertySuggestions(Type sourceType, string startPath, int maxDepth = 10)
        {
            var buffer = new List<TypeSuggestion>();
            var properties = sourceType.GetProperties(AssemblyHelper.BINDING_FLAGS_GAME);
            foreach (var property in properties)
            {
                if (property.PropertyType == sourceType || !property.MemberType.HasFlag(MemberTypes.Property))
                    continue;

                buffer.Add(new TypeSuggestion()
                {
                    MemberName = property.Name,
                    MemberPath = startPath,
                    MemberTypeName = property.PropertyType.Name,
                    AcceptedArgumentType = AssemblyHelper.GetFriendlyTypeInformation(property.PropertyType),
                    IsClass = property.PropertyType.IsClass,
                    IsPublic = property.GetMethod?.IsPublic ?? property.PropertyType.IsPublic,
                    IsStatic = property.GetMethod?.IsStatic ?? false,
                    BindingFlags = (BindingFlags)property.GetType().GetProperty("BindingFlags", AssemblyHelper.BINDING_FLAGS_GAME)?.GetValue(property),
                    MemberType = MemberType.Property,
                });

                try
                {
                    if (maxDepth > 0 && ShouldGenerateSuggestionForType(property.PropertyType))
                        buffer.AddRange(GetPropertySuggestions(property.PropertyType, $"{startPath}.{property.Name}", --maxDepth));
                }
                catch (Exception ex)
                {
                    LogWriter.Write($"Exception in {nameof(GetPropertySuggestions)}");
                    LogWriter.Write(ex);
                }
            }

            return buffer;
        }

        private static List<TypeSuggestion> GetFieldSuggestions(Type sourceType, string startPath, int maxDepth = 10)
        {
            var buffer = new List<TypeSuggestion>();
            if (!sourceType.MemberType.HasFlag(MemberTypes.Field))
                return buffer;

            foreach (var field in sourceType.GetFields(AssemblyHelper.BINDING_FLAGS_GAME))
            {
                if (!field.MemberType.HasFlag(MemberTypes.Field))
                    continue;

                var path = $"{startPath}.{field.Name}";
                buffer.Add(ConvertToTypeSuggestion(field, sourceType, path));

                try
                {
                    if (maxDepth > 0 && ShouldGenerateSuggestionForType(field.FieldType))
                        buffer.AddRange(GetFieldSuggestions(field.FieldType, path, --maxDepth));
                }
                catch (Exception ex)
                {
                    LogWriter.Write($"Exception in {nameof(GetFieldSuggestions)}");
                    LogWriter.Write(ex);
                }
            }
            return buffer;
        }

        private static Type GetProperty(Type sourceType, string memberPath)
        {
            var memberNames = memberPath.Split('.');
            var targetName = memberNames.Last();
            var path = "";

            foreach(var member in memberNames)
            {
                var type = sourceType.GetProperty(member, AssemblyHelper.BINDING_FLAGS_GAME);
                if (type == null)
                    return null;

                if (type.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase))
                    return type.PropertyType;

                path = string.IsNullOrEmpty(path) ? member : $"{path}.{member}";
                return GetProperty(type.PropertyType, path);
            }

            return null;
        }

        private static TypeSuggestion ConvertToTypeSuggestion(Type type, string memberPath)
        {
            return new TypeSuggestion()
            {
                MemberName = type.Name,
                MemberPath = memberPath,
                MemberTypeName = type.Name,
                IsClass = type.IsClass
            };
        }

        private static TypeSuggestion ConvertToTypeSuggestion(PropertyInfo pi, string memberPath)
        {
            return new TypeSuggestion()
            {
                MemberName = pi.Name,
                MemberPath = memberPath,
                MemberTypeName = pi.PropertyType.Name,
                AcceptedArgumentType = AssemblyHelper.GetFriendlyTypeInformation(pi.PropertyType),
                IsClass = pi.PropertyType.IsClass,
                IsPublic = pi.GetMethod?.IsPublic ?? pi.PropertyType.IsPublic,
                IsStatic = pi.GetMethod?.IsStatic ?? false,
                BindingFlags = (BindingFlags?)pi.GetType().GetProperty("BindingFlags", AssemblyHelper.BINDING_FLAGS_GAME)?.GetValue(pi) ?? null,
                MemberType = MemberType.Property,
            };
        }

        private static TypeSuggestion ConvertToTypeSuggestion(FieldInfo fi, Type parentType, string memberPath)
        {
            return new TypeSuggestion()
            {
                MemberName = fi.Name,
                MemberPath = memberPath,
                MemberTypeName = parentType.Name,
                AcceptedArgumentType = AssemblyHelper.GetFriendlyTypeInformation(fi.ReflectedType),
                IsClass = fi.ReflectedType.IsClass,
                IsPublic = fi.IsPublic,
                IsStatic = fi.IsStatic,
                BindingFlags = (BindingFlags)fi.GetType().GetProperty("BindingFlags", AssemblyHelper.BINDING_FLAGS_GAME)?.GetValue(fi),
                MemberType = MemberType.Field,
            };
        }

        private static bool ShouldGenerateSuggestionForType(Type propertyType)
        {
            if (propertyType.IsEnum)
                return true;

            if (propertyType.IsPrimitive)
                return false;

            if (propertyType == typeof(string))
                return false;

            return true;
        }
    }
}

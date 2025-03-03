using GHTweaks.Configuration;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GHTweaks.UI.Console.Command.Core
{
    public class AssemblyHelper
    {
        public const BindingFlags BINDING_FLAGS_CONFIG = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

        public const BindingFlags BINDING_FLAGS_GAME = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Default | BindingFlags.IgnoreCase;

        public const BindingFlags BINDING_FLAGS_PS = BindingFlags.Public | BindingFlags.Static;

        private static Type[] singleTonGameTypes;



        public static Assembly GetCSharpAssembly() 
            => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp");

        /// <summary>
        /// Returns a list with all Assembly-CSharp Types that owns a public static property: Instance or a public static method: Get.
        /// </summary>
        public static Type[] GetSingleTonGameTypes()
        {
            try
            {
                if (singleTonGameTypes == null)
                {
                    singleTonGameTypes = GetCSharpAssembly().GetTypes()
                        .Where(t => t.GetProperty("Instance", BINDING_FLAGS_PS) != null || t.GetMethod("Get", BINDING_FLAGS_PS) != null)
                        .ToArray();
                }
                return singleTonGameTypes;
            }
            catch(Exception ex)
            {
                LogWriter.Write(ex);
            }
            return new Type[0];
        }


        public static IEnumerable<Type> GetMatchingSingleTonGameTypes(string pattern, RegexOptions regexOptions=RegexOptions.IgnoreCase)
        {
            var result = new List<Type>();
            foreach (var type in GetSingleTonGameTypes())
            {
                if (Regex.IsMatch(type.Name, pattern, regexOptions))
                    result.Add(type);
            }
            return result;
        }


        public static bool TryGetSingleTonInstance(Type type, out object instance)
        {
            instance = null;
            if (type == null)
                return false;

            try
            {
                var pi = type.GetProperty("Instance", BINDING_FLAGS_PS);
                if (pi != null)
                {
                    instance = pi.GetValue(null);
                    if (instance.GetType() == type)
                        return true;
                }

                var mi = type.GetMethod("Get", BINDING_FLAGS_PS);
                if (mi == null || mi.ReturnType != type)
                    return false;

                instance = mi.Invoke(null, null);
                LogWriter.Write($"Invoked {type.Name}.Get() method, returned: {instance?.GetType().Name ?? "null"}.");
                return instance != null && instance.GetType() == type;
            }
            catch(Exception ex)
            {
                LogWriter.Write(ex);
            }
            return false;
        }


        public static bool TryGetSingleTonGameType(string name, out Type type)
        {
            type = GetSingleTonGameTypes().FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return type != null;
        }


        public static bool TryGetMemberInfoFromPath(string path, out object parentInstance, out MemberInfo memberInfo)
        {
            memberInfo = null;
            parentInstance = null;
            if (string.IsNullOrEmpty(path))
                return false;

            // Get parent type
            var bindingFlags = BINDING_FLAGS_GAME;
            var memberNames = path.Split('.');
            if (memberNames[0].Equals("config", StringComparison.OrdinalIgnoreCase))
            {
                parentInstance = Mod.Instance.Config;
                bindingFlags = BINDING_FLAGS_CONFIG;
            }
            else if (memberNames[0].Equals("game", StringComparison.OrdinalIgnoreCase))
            {
                if (memberNames.Length < 2)
                    return false;

                if (!TryGetSingleTonInstance(GetSingleTonGameTypes().FirstOrDefault(x => x.Name.Equals(memberNames[1], StringComparison.OrdinalIgnoreCase)), out parentInstance))
                    return false;
            }

            Type type;
            for (int i = 1; i < memberNames.Length -1; ++i)
            {
                var member = memberNames[i];
                type = parentInstance.GetType();

                memberInfo = type.GetProperty(member, bindingFlags) as MemberInfo ?? type.GetField(member, bindingFlags);
                if (memberInfo == null)
                    return false;

                //
                // TODO: Make type check
                //

                if (memberInfo.MemberType == MemberTypes.Property) 
                {
                    var pi = (PropertyInfo)memberInfo;
                    parentInstance = pi.GetValue(parentInstance);
                    memberInfo = pi.PropertyType;
                }
                else 
                {
                    var fi = (FieldInfo)memberInfo;
                    parentInstance = fi.GetValue(parentInstance);
                    memberInfo = fi.FieldType;
                }
            }

            var search = memberNames.Last();
            type = parentInstance.GetType();
            memberInfo = type.GetProperty(search, bindingFlags) as MemberInfo ?? type.GetField(search, bindingFlags);

            return true;
        }


        public static bool TryGetMemberTypeFromPath(string path, out Type type)
        {
            type = null;
            if (string.IsNullOrEmpty(path))
                return false;

            // Get parent type
            var memberNames = path.Split('.');
            if (memberNames.Length < 2)
                return false;

            var bindingFlags = BINDING_FLAGS_GAME;
            if (memberNames[0].Equals("config", StringComparison.OrdinalIgnoreCase))
            {
                type = typeof(Config);
                bindingFlags = BINDING_FLAGS_CONFIG;
            }
            else if (memberNames[0].Equals("game", StringComparison.OrdinalIgnoreCase))
            {
                if (!TryGetSingleTonGameType(memberNames[1], out type))
                    return false;

                memberNames = memberNames.Skip(1).ToArray();
            }

            for (int i = 1; i < memberNames.Length; ++i)
            {
                var member = memberNames[i];
                var memberInfo = type.GetProperty(member, bindingFlags) as MemberInfo ?? type.GetField(member, bindingFlags);
                if (memberInfo == null)
                    return false;

                if (memberInfo.MemberType == MemberTypes.Property)
                    type = ((PropertyInfo)memberInfo).PropertyType;
                else
                    type = ((FieldInfo)memberInfo).FieldType;
            }

            return true;
        }


        public static string[] DumpMemberTypes(object instance)
        {
            if (instance == null || !(instance.GetType().GetProperties(BINDING_FLAGS_GAME).Any() && instance.GetType().GetFields(BINDING_FLAGS_GAME).Any()))
                return new string[0];

            var buffer = new List<string>()
            {
                $"<b>{instance.GetType().Name.ToSyntaxHighlightedRTF(true)}</b> : {instance.GetType().FullName}"
            };

            foreach(var type in instance.GetType().GetProperties(BINDING_FLAGS_GAME))
            {
                var str = UIHelper.GetMemberInfoString(type, instance);
                buffer.Add($"{Style.LineIndent}{str}");
            }

            foreach (var type in instance.GetType().GetFields(BINDING_FLAGS_GAME))
            {
                var str = UIHelper.GetMemberInfoString(type, instance);
                buffer.Add($"{Style.LineIndent}{str}");
            }

            return buffer.ToArray();
        }


        public static string[] DumpMemberTypes(Type type)
        {
            if (type == null || !(type.GetProperties(BINDING_FLAGS_GAME).Any() && type.GetFields(BINDING_FLAGS_GAME).Any()))
                return new string[0];

            var buffer = new List<string>()
            {
                $"<b>{type.Name.ToSyntaxHighlightedRTF(true)}</b> : {type.FullName}"
            };

            foreach (var _type in type.GetProperties(BINDING_FLAGS_GAME))
            {
                var str = UIHelper.GetMemberInfoString(_type);
                buffer.Add($"{Style.LineIndent}{str}");
            }

            foreach (var _type in type.GetFields(BINDING_FLAGS_GAME))
            {
                var str = UIHelper.GetMemberInfoString(_type);
                buffer.Add($"{Style.LineIndent}{str}");
            }

            return buffer.ToArray();
        }


        public static string GetFriendlyTypeInformation(Type type)
        {
            string suffix;
            if (type == typeof(bool))
                suffix = $"{type.Name} (True or False)";
            else if (Regex.IsMatch(type.Name, "^Single|Double|Decimal$"))
                suffix = $"{type.Name} (0.0 - 9.9)";
            else if (Regex.IsMatch(type.Name, "^U?Int\\d*$"))
                suffix = $"{type.Name} (0 - 9)";
            else if (type.IsEnum)
                suffix = $"Enum ({type.Name})";
            else if (type == typeof(string))
                suffix = $"{type.Name} (Abc)";
            else
                suffix = type.Name;

            return suffix;
        }


        public static bool TryGetCollectionItem(string propertyPath, out object instance, out MemberInfo propertyInfo)
        {
            instance = null;
            propertyInfo = null;

            if (string.IsNullOrEmpty(propertyPath) || !propertyPath.HasSomeKindOfIndex())
                return false;

            var indexer = propertyPath.GetIndexer();
            if (indexer.Length > 1)
                return false;

            var match = Regex.Match(propertyPath, @"^(?<list>\w+\[\w+])(?<path>\.\w+[\w.]+)?$");
            if (!match.Success)
                return false;

            var parentType = match.Groups["list"].Value;
            var stripedParentType = parentType.RemoveAllKindsOfIndex();
            BindingFlags bindingFlags = BINDING_FLAGS_GAME;

            if (!TryGetSingleTonGameType(stripedParentType, out Type type) || !TryGetSingleTonInstance(type, out instance))
            {
                bindingFlags = BINDING_FLAGS_CONFIG;
                var pi = typeof(Config).GetProperty(stripedParentType, bindingFlags);
                if (pi == null)
                    return false;

                instance = Mod.Instance.Config;
            }

            if (instance == null)
                return false;

            var typeNames = stripedParentType.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var member in typeNames)
            {
                propertyInfo = (MemberInfo)instance.GetType().GetProperty(member, bindingFlags) ?? instance.GetType().GetField(member, bindingFlags);
                if (propertyInfo == null)
                    return false;

                instance = propertyInfo.GetValue(instance);
            }

            var index = indexer[0];
            if (instance is Array array)
            {
                instance = array.GetValue(index);
            }
            else if (instance is IList lst)
            {
                instance = lst[index];
            }
            else
            {
                return false;
            }

            typeNames = match.Groups["path"].Value.Trim('.').Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < typeNames.Length - 1; ++i)
            {
                var member = typeNames[i];
                propertyInfo = (MemberInfo)instance.GetType().GetProperty(member, bindingFlags) ?? instance.GetType().GetField(member, bindingFlags);
                if (propertyInfo == null)
                    return false;

                if ((instance = propertyInfo.GetValue(instance)) == null)
                    return false;
            }

            propertyInfo = (MemberInfo)instance.GetType().GetProperty(typeNames.Last(), bindingFlags) ?? instance.GetType().GetField(typeNames.Last(), bindingFlags);
            if (propertyInfo == null)
                return false;

            return instance != null;
        }
    }
}

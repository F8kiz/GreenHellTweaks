using GHTweaks.Configuration.Core;
using GHTweaks.UI.Console.Command.Core;

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using static P2PStats.ReplicationStat;

namespace GHTweaks.UI.Console.Command
{
    [Command("get|set|save|reload|print=Config*", "")]
    public class ConfigCommandHandler : ICommand
    {
        public string[] UsageExamples { get; private set; } = new string[] {
                $"Type in {"get Config.DebugModeEnabled".ToBoldCodeRTF()} and the console prints the current value of <b>Config.DebugModeEnabled</b>.",
                $"Type in {"set Config.DebugModeEnabled True".ToBoldCodeRTF()} to enable debug mode.",
                $"Type in {"get Config.LiquidContainerConfig.BidonCapacity".ToBoldCodeRTF()} and the console prints the current value of <b>Config.LiquidContainerConfig.BidonCapacity</b>.",
                $"Type in {"set Config.LiquidContainerConfig.BidonCapacity 100".ToBoldCodeRTF()} to set the capacity for bidons to 100.",
                $"Type in {"save Config".ToBoldCodeRTF()} to save the current runtime config.",
                $"Type in {"print Config".ToBoldCodeRTF()} to print all GHTweaks Config properties without AIParams.",
                $"Type in {"print Config ex _".ToBoldCodeRTF()} to print all GHTweaks Config properties.",
                $"Type in {"print Config full_names _".ToBoldCodeRTF()} to print all GHTweaks Config properties with their full names.",
                $"Type in {"print Config full_names ex".ToBoldCodeRTF()} to combine the two previous options."
            };


        public CommandResult Execute(CommandInfo cmd)
        {
            var result = new CommandResult(cmd);
            if (!cmd.FirstArgumentNameStartsWith("Config"))
                return result;
            
            if (cmd.CommandEqualsAny("get", "set"))
                return GetOrSetConfigPropertyCommand.Execute(cmd);

            if (cmd.CommandEquals("Save", "config"))
            {
                if (Mod.Instance.TrySaveConfig())
                {
                    result.OutputAdd("Config was saved.");
                    result.CmdExecResult = CmdExecResult.Executed;
                }
            }

            if (cmd.CommandEquals("reload", "config"))
            {
                Mod.Instance.ReloadConfig();
                result.OutputAdd("Config reloaded.");
                result.CmdExecResult = CmdExecResult.Executed;
            }

            if (cmd.CommandEquals("print", "config"))
            {
                PrintConfigCommand.Execute(cmd.HasArgument("ex"), cmd.HasArgument("full_names"), ref result);
                result.CmdExecResult = CmdExecResult.Executed;
            }

            return result;
        }



        internal static class ConfigHelper
        {
            public static void PrintPatchConfig(string propertyName, string propertyPath, IPatchConfig instance, bool debugModeEnabled, ref CommandResult result)
            {
                result.OutputAdd($"<b>{propertyName}:</b>");
                foreach (var pi0 in instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    try
                    {
                        if (pi0.Name == nameof(IPatchConfig.HasAtLeastOneEnabledPatch) && !debugModeEnabled)
                            continue;

                        propertyPath = !string.IsNullOrWhiteSpace(propertyPath) ? $"{propertyPath}." : "";
                        result.OutputAdd($"{Style.LineIndent}{propertyPath}{pi0.Name}: {pi0.GetValue(instance).ToCodeRTF()}");
                    }
                    catch (Exception ex)
                    {
                        result.OutputAdd($"{Style.LineIndent}{propertyName}.{pi0.Name}.GetValue: {ex.Message}", Style.TextColor.Error);
                        LogWriter.Write(ex);
                    }
                }
            }
        }


        internal static class GetOrSetConfigPropertyCommand
        {
            public static CommandResult Execute(CommandInfo cmd)
            {
                var result = new CommandResult(cmd, CmdExecResult.Executed);
                if (!TryGetConfigValue(cmd, ref result, out object instance, out PropertyInfo property))
                    return result;

                if (instance == null)
                {
                    result.OutputAdd($"Got no object instance to get value of property: {property?.Name}", Style.TextColor.Warning);
                    result.CmdExecResult |= CmdExecResult.Error;
                    return result;
                }

                if (property == null)
                {
                    result.OutputAdd("Got no PropertyInfo instance.", Style.TextColor.Warning);
                    result.CmdExecResult |= CmdExecResult.Error;
                    return result;
                }

                try
                {
                    var propName = property.Name;
                    var typeName = cmd.ArgumentMap.First().Key;

                    if (cmd.CommandEquals("get"))
                        ExecuteGetCommand(typeName, propName, instance, property, ref result);
                    else
                        ExecuteSetCommand(cmd, typeName, propName, instance, property, ref result);
                }
                catch (Exception ex)
                {
                    result.OutputAdd(ex.Message, Style.TextColor.Error);
                    result.CmdExecResult |= CmdExecResult.Exception;
                    LogWriter.Write(ex);
                }

                return result;
            }


            private static void ExecuteGetCommand(string typeName, string propName, object instance, PropertyInfo property, ref CommandResult result)
            {
                LogWriter.Write($"Try get value from: {instance.GetType().Name}.{propName}");

                object value = null;
                if (typeName.HasSomeKindOfIndex())
                {
                    instance = property.GetValue(instance, null);
                    var match = Regex.Match(typeName, @"\[(?<index>\w+)]$");
                    if (int.TryParse(match.Groups["index"].Value, out int index))
                    {
                        if (instance is Array array)
                            value = array.GetValue(index);
                        else if (instance is IList lst)
                            value = lst[index];
                    }
                }
                else
                {
                    value = property.GetValue(instance);
                }
                result.OutputAdd($"{typeName}: {value.ToCodeRTF()}");

                var type = value.GetType();
                if (type.GetInterface(nameof(IPatchConfig)) != null)
                    ConfigHelper.PrintPatchConfig(propName, "", (IPatchConfig)value, false, ref result);
                else if (type.IsClass)
                    result.OutputAddRange(AssemblyHelper.DumpMemberTypes(value));
            }

            private static void ExecuteSetCommand(CommandInfo cmd, string typeName, string propName, object instance, PropertyInfo property, ref CommandResult result)
            {
                var kvp = cmd.ArgumentMap.First();
                propName = kvp.Key.Split('.').Last();
                if (!string.Equals(propName, property.Name, StringComparison.OrdinalIgnoreCase))
                {
                    result.OutputAdd($"Unable to set property, expected Property: {propName}, got Property: {property.Name}");
                    result.CmdExecResult |= CmdExecResult.Error;
                }

                try
                {
                    object value = kvp.Value;
                    if (value.GetType() != property.PropertyType)
                    {
                        value = Convert.ChangeType(value, property.PropertyType);
                        if (value == null || value.GetType() != property.PropertyType)
                            return;
                    }
                    property.SetValue(instance, value);
                    result.OutputAdd($"{typeName}: {property.GetValue(instance)}");
                }
                catch (Exception ex)
                {
                    LogWriter.Write(ex);
                    result.OutputAdd(ex.Message);
                }
            }

            private static bool TryGetConfigValue(CommandInfo cmd, ref CommandResult result, out object instance, out PropertyInfo value)
            {
                instance = null;
                value = null;

                var firstArgs = cmd.GetFirstArgumentName();
                if (string.IsNullOrEmpty(firstArgs))
                {
                    result.OutputAdd($"A command parameter is expected but not provided. The right syntax is: {"get/set Config.(SomeProperty)".ToBoldCodeRTF()}.");
                    return false;
                }

                var classChain = Regex.Replace(firstArgs, @"^Config\.", "", RegexOptions.IgnoreCase);
                var objects = classChain.Split('.');
                if (objects.Length < 1)
                {
                    result.OutputAdd($"At least one property name is expected. The right syntax is: {"get/set Config.(SomeProperty)".ToBoldCodeRTF()}.");
                    return false;
                }

                if (classChain.HasSomeKindOfIndex())
                {
                    var indexer = classChain.GetIndexer();
                    if (indexer.Length > 1)
                    {
                        result.OutputAdd($"Sorry buddy but nested List or Array is not supported.");
                        result.OutputAdd($" [ {"!".ToBoldCodeRTF().ToColoredRTF(Style.TextColor.Warning)} ] Please avoid more than one indexer like: Foo[0].Bar[1]");
                        result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Warning;
                        return false;
                    }
                    if (AssemblyHelper.TryGetCollectionItem(classChain, out instance, out MemberInfo mi))
                    {
                        value = mi as PropertyInfo;
                        return true;
                    }
                }

                try
                {
                    var search = objects.Last();
                    var config = Mod.Instance.Config;
                    instance = Mod.Instance.Config;
                    object lastInstance = null;
                    var properties = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    PropertyInfo property = null;

                    for (int i = 0; i < objects.Length; ++i)
                    {
                        var typeName = objects[i];
                        if ((property = properties.FirstOrDefault(x => x.Name == typeName && x.MemberType == MemberTypes.Property)) == null)
                        {
                            result.OutputAdd($"Found no such property '{lastInstance?.GetType().Name ?? "Config"}.{typeName}'...");
                            var suggestions = properties.Where(x =>
                                Regex.IsMatch(x.Name, $"^({typeName}.*|.*{typeName}|.*{typeName}.*)$", RegexOptions.IgnoreCase)
                            ).Select(p => new CommandInfo($"{cmd.Command} {property?.Name ?? "Config"}.{p.Name}")).ToList();

                            if (suggestions.Count > 0)
                            {
                                result.OutputAdd($"Do you mean one them:\n{string.Join("\n", suggestions)}");
                                result.Value = suggestions;
                                result.ConsoleAction = ConsoleAction.SetCommandSuggestion;
                            }
                            result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Error;
                            break;
                        }

                        lastInstance = instance;
                        instance = property.GetValue(instance, null);
                        if (property.PropertyType is IPatchConfig cfg)
                        {
                            properties = cfg.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            continue;
                        }

                        if (property.Name == search)
                        {
                            value = property;
                            instance = lastInstance;
                            return true;
                        }
                        properties = property.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                catch (Exception ex)
                {
                    result.OutputAdd(ex.Message);
                    result.CmdExecResult |= CmdExecResult.Exception;
                    LogWriter.Write(ex);
                }
                return false;
            }
        }


        internal static class PrintConfigCommand
        {
            public static void Execute(bool printListItems, bool printFullNames, ref CommandResult result)
            {
                var config = Mod.Instance.Config;
                var configs = config.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var debugModeEnabled = Mod.Instance.Config.DebugModeEnabled;
                var path = "";

                foreach (var pi in configs)
                {
                    if (pi.Name == nameof(config.PlayerLastPosition))
                        continue;

                    object instance = pi.GetValue(config);
                    if (instance == null)
                        continue;

                    if (printFullNames)
                        path = $"Config.{pi.Name}.";

                    if (instance is IList list)
                    {
                        if (printListItems)
                        {
                            PrintList(pi.Name, list, ref result);
                            result.OutputAdd($"");
                        }
                    }
                    else if (pi.PropertyType.GetInterface(nameof(IPatchConfig)) != null)
                    {
                        ConfigHelper.PrintPatchConfig(pi.Name, path, (IPatchConfig)instance, debugModeEnabled, ref result);
                        result.OutputAdd($"");
                    }
                    else
                    {
                        if (printFullNames)
                            path = "Config.";

                        result.OutputAdd($"{path}{pi.Name}: {pi.GetValue(config).ToCodeRTF()}");
                    }
                }
            }


            private static void PrintList(string propertyName, IList list, ref CommandResult result)
            {
                if (list == null)
                {
                    result.OutputAdd($"<b>{propertyName}:</b> null");
                    return;
                }

                if (list.Count < 1)
                {
                    result.OutputAdd($"<b>{propertyName}:</b> No items");
                    return;
                }

                var firstType = list[0].GetType();
                var sLineIndent = Style.LineIndent;
                var dLineIndent = sLineIndent + sLineIndent;

                result.OutputAdd($"<b>{propertyName}:</b> {list.Count} : {firstType.Name}");
                for(int i = 0; i < list.Count; ++i)
                {
                    result.OutputAdd($"{sLineIndent}<b>Item: {i}</b>");
                    foreach(var prop in firstType.GetProperties())
                    {
                        try
                        {
                            result.OutputAdd($"{dLineIndent}{prop.Name}: {prop.GetValue(list[i])}");
                        }
                        catch (Exception ex) 
                        {
                            result.OutputAdd($"{dLineIndent}{propertyName}.{prop.Name}.GetValue: {ex.Message}", Style.TextColor.Error);
                            LogWriter.Write(ex);
                        }
                    }
                }
            }
        }
    }
}
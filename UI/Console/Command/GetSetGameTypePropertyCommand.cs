using GHTweaks.UI.Console.Command.Core;

using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GHTweaks.UI.Console.Command
{
    [Command("get|set=Game*", "")]
    public class GetSetGameTypePropertyCommand : ICommand
    {
        public string[] UsageExamples => null;

        public CommandResult Execute(CommandInfo cmd)
        {
            if (!cmd.CommandEqualsAny("get", "set") || !cmd.FirstArgumentNameStartsWith("Game"))
                return new CommandResult(cmd);

            CommandResult result;
            if (cmd.CommandEquals("get"))
                result = HandleGetCommand(cmd);
            else 
                result = HandleSetCommand(cmd);

            return result;
        }


        private CommandResult HandleGetCommand(CommandInfo cmd)
        {
            var result = new CommandResult(cmd, CmdExecResult.Executed);
            var clsChain = cmd.GetFirstArgumentName().TrimStart("Game.");

            if (clsChain.HasSomeKindOfIndex())
            {
                var indexer = clsChain.GetIndexer();
                if (indexer.Length > 1)
                {
                    result.OutputAdd($"Sorry buddy but nested List or Array is not supported.");
                    result.OutputAdd($" [ {"!".ToBoldCodeRTF().ToColoredRTF(Style.TextColor.Warning)} ] Please avoid more than one indexer like: Foo[0].Bar[1]");
                    result.CmdExecResult |= CmdExecResult.Warning;
                    return result;
                }

                if (AssemblyHelper.TryGetCollectionItem(clsChain, out object instance, out MemberInfo mi))
                {
                    var str = UIHelper.GetMemberInfoString(mi, instance);
                    result.OutputAdd(str);
                    return result;
                }
            }

            // Try to get an instance member.
            if (AssemblyHelper.TryGetMemberInfoFromPath(cmd.GetFirstArgumentName(), out object ownerInstance, out MemberInfo memberInfo))
            {
                try
                {
                    if (memberInfo != null)
                    {
                        var value = memberInfo.GetValue(ownerInstance);
                        if (value == null)
                        {
                            result.OutputAdd($"Found no proper value for: {ownerInstance.GetType()}, MemberInfo: {memberInfo.Name}");
                            result.CmdExecResult |= CmdExecResult.Error;
                            return result;
                        }

                        if (!value.GetType().IsClass)
                        {
                            result.OutputAdd(clsChain.ToSyntaxHighlightedRTF(value, false, "="));
                        }
                        else
                        {
                            var types = AssemblyHelper.DumpMemberTypes(value);
                            if (types.Length > 0)
                                result.OutputAddRange(types);
                            else
                                result.OutputAdd($"Found no class member in Type: {value.GetType()}");
                        }

                    }
                    else if (ownerInstance.GetType().IsClass)
                    {
                        var types = AssemblyHelper.DumpMemberTypes(ownerInstance);
                        if (types.Length > 0)
                            result.OutputAddRange(types);
                        else
                            result.OutputAdd($"Found no class member in Type: {ownerInstance.GetType()}");
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    LogWriter.Write(ex);
                    result.OutputAdd(ex.Message, Style.TextColor.Error);
                    result.CmdExecResult |= CmdExecResult.Error; 
                    return result;
                }
            }

            if (AssemblyHelper.TryGetMemberTypeFromPath(cmd.GetFirstArgumentName(), out Type type))
            {
                try
                {
                    var types = AssemblyHelper.DumpMemberTypes(type);
                    if (types.Length > 0)
                        result.OutputAddRange(types);
                    else
                        result.OutputAdd($"Found no class member in Type: {type.GetType()}");

                    return result;
                }
                catch (Exception ex)
                {
                    LogWriter.Write(ex);
                    result.OutputAdd(ex.Message, Style.TextColor.Error);
                    result.CmdExecResult |= CmdExecResult.Error;
                    return result;
                }
            }

            result.CmdExecResult = CmdExecResult.NotExecuted;
            result.OutputAdd($"Found no instance: {clsChain}. <b>Keep in mind, that some instances requires a running game session.</b>");
            return result;
        }

        private CommandResult HandleSetCommand(CommandInfo cmd)
        {
            var result = new CommandResult(cmd);
            if (string.IsNullOrEmpty(cmd.GetFirstArgumentValue()))
            {
                result.OutputAdd($"A value to assign to property is required but not provided.");
                return result;
            }

            var typeName = cmd.GetFirstArgumentName().TrimStart("Game.");
            if (!AssemblyHelper.TryGetMemberInfoFromPath(cmd.GetFirstArgumentName(), out object ownerInstance, out MemberInfo memberInfo))
            {
                result.OutputAdd($"Found no instance: {typeName}. <b>Keep in mind, that some instances requires a running game session.</b>");
                result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Error;
                return result;
            }

            if (memberInfo == null || (memberInfo.MemberType != MemberTypes.Property && memberInfo.MemberType != MemberTypes.Field))
            {
                result.OutputAdd($"The required property was not found.");
                result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Error;
                return result;
            }

            var type = memberInfo.MemberType == MemberTypes.Property ? ((PropertyInfo)memberInfo).PropertyType : ((FieldInfo)memberInfo).FieldType;
            if (!TryConvertStringValueToRequiredType(cmd.GetFirstArgumentValue(), type, out object propertyValue))
            {
                result.OutputAdd($"The value you try to assign has an invalid type. You can assign: {AssemblyHelper.GetFriendlyTypeInformation(type)}");
                result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Error;
                return result;
            }

            // 
            // TODO: Add support for collections
            //

            result.CmdExecResult = CmdExecResult.Executed;

            if (memberInfo.MemberType == MemberTypes.Property)
                ((PropertyInfo)memberInfo).SetValue(ownerInstance, propertyValue);
            else
                ((FieldInfo)memberInfo).SetValue(ownerInstance, propertyValue);

            var newValue = memberInfo.GetValue(ownerInstance);
            result.OutputAdd($"New value: {newValue.ToCodeRTF()}");
            
            if (newValue != propertyValue)
               result.CmdExecResult |= CmdExecResult.Error;

            return result;
        }

        private bool TryConvertStringValueToRequiredType(string value, Type requiredType, out object convertedValue)
        {
            convertedValue = null;
            if (requiredType == typeof(string))
            {
                if (value.ToLower() == "null")
                    value = null;

                convertedValue = value;
                return true;
            }

            if (requiredType == typeof(bool))
            {
                if (!Regex.IsMatch(value, "^[10]|true|false|on|off$", RegexOptions.IgnoreCase))
                    return false;

                convertedValue = Regex.IsMatch(value, "^1|true|on$");
                return true;
            }

            convertedValue = Convert.ChangeType(value, requiredType);
            if (convertedValue == null || convertedValue.GetType() != requiredType)
                return false;

            return true;
        }
    }
}

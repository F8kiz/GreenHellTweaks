using System;
using System.Collections.Generic;
using System.Linq;

namespace GHTweaks.UI.Console.Command.Core
{
    internal class CommandDescription
    {
        public string Command {  get; set; }

        public Dictionary<string, string> AcceptedParameters { get; set; }


        public CommandDescription() { }

        public CommandDescription(string command) => Command = command;

        /// <param name="acceptedParameters">
        /// <para>You can pass the accepted parameters as a comma separated key value pair list.</para>
        /// An trailing "=" is optional if the parameter does not accept a value.
        /// But it's good practice to use it to make clear that the parameter does not accept an value.
        /// 
        /// <code>
        /// acceptedParameters = "param=value, paramWithoutValue="
        /// </code>
        /// 
        /// The line above would result in an <see cref="AcceptedParameters"/> dictionary like this:
        /// 
        /// <code>
        /// new Dictionary() {
        ///     { param, value },
        ///     { paramWithoutValue, null }
        /// }
        /// </code>
        /// </param>
        public CommandDescription(string command, string acceptedParameters)
        {
            Command = command;
            if (!string.IsNullOrEmpty(acceptedParameters))
            {
                AcceptedParameters = acceptedParameters.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim())
                    .Select(a => a.Split('='))
                    .Select(c =>
                    {
                        var name = c[0].Trim();
                        var value = c[1]?.Trim() ?? null;
                        return new Tuple<string, string>(name, value);
                    }).ToDictionary(key => key.Item1, value => value.Item2);
            }
        }

        public CommandDescription(string command, Dictionary<string, string> acceptedParameters) 
        { 
            Command = command;
            AcceptedParameters = acceptedParameters;
        }

    }
}

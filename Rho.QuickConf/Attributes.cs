using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ConfigurationFileAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ConfigurationFieldAttribute : Attribute
    {
        public string Group { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Marks a field as a configuration parameter
        /// </summary>
        /// <param name="group">What section/group it belongs to in the file (none if empty)</param>
        /// <param name="name">If empty it's assumed to be the same as the field name</param>
        public ConfigurationFieldAttribute(string group = "", string name = "")
        {
            Group = group;
            Name = name;
        }
    }
}

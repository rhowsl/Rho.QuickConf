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

        public ConfigurationFieldAttribute(string group = "", string name = "")
        {
            Group = group;
            Name = name;
        }
    }
}

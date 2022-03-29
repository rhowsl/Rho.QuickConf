using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf
{
    /// <summary>
    /// Marks a class as a configuration file. Every field is a string, basically.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ConfigurationFileAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
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

            if (Name == string.Empty)
                throw new ArgumentNullException("Name cannot be empty.");
        }
    }
}

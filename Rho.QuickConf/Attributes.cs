using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationFile : Attribute
    {
        public readonly string SetFileName;

        public ConfigurationFile(string setFileName)
        {
            SetFileName = setFileName;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class Field : Attribute
    {
        public string Group { get; set; }
        public string Name { get; set; }

        public Field(string group = "", string name = "")
        {
            Group = group;
            Name = name;
        }
    }
}

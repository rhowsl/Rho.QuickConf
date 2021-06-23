using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rho.QuickConf
{
    public static class ConfigurationReader
    {
        private static string GetFileNameFromConfigurationObject(object configurationObject)
        {
            var attribute = configurationObject.GetType().GetCustomAttribute(typeof(ConfigurationFile));
            if (!(attribute is null))
            {
                var cfgAttribute = attribute as ConfigurationFile;
                return cfgAttribute.SetFileName;
            }
            return null;
        }

        private static ConfigurationField ExtractFieldAttribute(FieldInfo fieldMember)
        {
            var attribute = fieldMember.GetCustomAttribute(typeof(ConfigurationField));
            if (!(attribute is null))
            {
                var fieldAttribute = attribute as ConfigurationField;
                return new ConfigurationField()
                {
                    Group = fieldAttribute.Group,
                    Name = (fieldAttribute.Name != string.Empty) ? fieldAttribute.Name : fieldMember.GetType().Name
                };
            }
            return null;
        }

        public static void ParseInto(object configurationObject, string fileName = "")
        {
            // Check for attribute presence
            string setFileName = GetFileNameFromConfigurationObject(configurationObject);
            fileName = setFileName != string.Empty ? setFileName : fileName;

            // Parse it first
            var config = ConfigParser.ReadRawConfigFile(fileName);

            // List out fields that need to be checked and shit
            List<ConfigurationField> fields = new List<ConfigurationField>();
            foreach (var field in configurationObject.GetType().GetFields())
            {
                var fieldAttribute = ExtractFieldAttribute(field);

                if (field.IsInitOnly)
                    throw new MemberAccessException($"Field {fieldAttribute.Name} is read-only.");
                
                if (!(fieldAttribute is null))
                {
                    object value = config[fieldAttribute.Group][fieldAttribute.Name];
                    field.SetValue(configurationObject, value);
                }
            }
        }
    }
}

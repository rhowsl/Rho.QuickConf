using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rho.QuickConf
{
    public static class ConfigurationSerializer
    {
        private static bool IsSerialiable(object obj) 
            => !(obj.GetType().GetCustomAttribute<SerializableAttribute>() is null);

        private static FieldInfo[] GetAllConfigurationFields(object obj) =>
            obj.GetType().GetFields().Where(f => !(f.GetCustomAttribute<ConfigurationFieldAttribute>() is null)).ToArray();

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

        private static ConfigurationFieldAttribute ExtractFieldAttribute(FieldInfo fieldMember)
        {
            var attribute = fieldMember.GetCustomAttribute(typeof(ConfigurationFieldAttribute));
            if (!(attribute is null))
            {
                var fieldAttribute = attribute as ConfigurationFieldAttribute;
                return new ConfigurationFieldAttribute()
                {
                    Group = fieldAttribute.Group,
                    Name = (fieldAttribute.Name != string.Empty) ? fieldAttribute.Name : fieldMember.GetType().Name
                };
            }
            return null;
        }

        public static void DeserializeInto(object configurationObject, string[] data)
        {
            // Parse
            var config = ConfigParser.ReadRawConfigData(data);

            // List out fields that need to be checked and shit
            List<ConfigurationFieldAttribute> fields = new List<ConfigurationFieldAttribute>();
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

        public static string[] SerializeFrom(object configurationObject)
        {            
            if (!IsSerialiable(configurationObject))
                throw new InvalidOperationException("The given class is not serializable!");

            Dictionary<string, Dictionary<string, object>> groupsValuesPairs = new Dictionary<string, Dictionary<string, object>>();

            foreach (var field in GetAllConfigurationFields(configurationObject))
            {
                var attribute = ExtractFieldAttribute(field);

                if (!groupsValuesPairs.ContainsKey(attribute.Group))
                {
                    groupsValuesPairs.Add(attribute.Group, new Dictionary<string, object>());
                }
                else
                {
                    groupsValuesPairs[attribute.Group].Add(attribute.Name, field.GetValue(configurationObject));
                }
            }

            string[] data = ConfigParser.WriteRawConfigData(groupsValuesPairs);

            return data;
        }
    }
}

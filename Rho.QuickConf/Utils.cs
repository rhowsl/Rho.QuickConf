using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rho.QuickConf
{
    static class Utils
    {
        public static bool IsSerialiable(object obj) 
            => !(obj.GetType().GetCustomAttribute<SerializableAttribute>() is null);

        public static FieldInfo[] GetAllConfigurationFields(object obj) =>
            obj.GetType().GetFields().Where(f => !(f.GetCustomAttribute<ConfigurationFieldAttribute>() is null)).ToArray();

        public static bool IsConfigurationClass(object configurationObject)
            => !(configurationObject.GetType().GetCustomAttribute(typeof(ConfigurationFileAttribute)) is null);

        public static ConfigurationFieldAttribute ExtractFieldAttribute(FieldInfo fieldMember)
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
    }
}

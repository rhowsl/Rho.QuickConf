using System;
using System.Linq;
using System.Reflection;

namespace Rho.QuickConf
{
    static class Utils
    {
        public static bool IsSerialiable(object obj) 
            => !(obj.GetType().GetCustomAttribute<SerializableAttribute>() is null);

        public static MemberInfo[] GetAllConfigurationMembers(object obj) =>
            obj.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(f => !(f.GetCustomAttribute<ConfigurationFieldAttribute>() is null) && (f.MemberType == MemberTypes.Property || f.MemberType == MemberTypes.Field)).ToArray();

        public static bool IsConfigurationClass(object configurationObject)
            => !(configurationObject.GetType().GetCustomAttribute(typeof(ConfigurationFileAttribute)) is null);

        public static ConfigurationFieldAttribute ExtractMemberAttribute(MemberInfo fieldMember)
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

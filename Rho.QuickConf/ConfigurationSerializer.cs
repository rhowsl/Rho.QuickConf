using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rho.QuickConf
{
    // TODO: Make Serialize return an object.
    public class ConfigurationSerializer<T>
    {
        /// <summary>
        /// Deserializes configuration data into an object with marked fields.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="data">ini data (multi-line string)</param>
        public T Deserialize(string[] data)
        {
            T obj = Activator.CreateInstance<T>();

            if (!Utils.IsConfigurationClass(obj))
                throw new InvalidOperationException("The given class is not a configuration object!");

            // parse the file
            var config = Parser.ReadRawConfigData(data);

            var members = obj.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttribute(typeof(ConfigurationFieldAttribute)) != null);

            foreach (var member in members)
            {
                var memberCfgAttribute = Utils.ExtractMemberAttribute(member);
                object value = null;

                if (memberCfgAttribute != null)
                {
                    switch (member.MemberType)
                    {
                        case MemberTypes.Field:
                            value = config[memberCfgAttribute.Group][memberCfgAttribute.Name];
                            (member as FieldInfo).SetValue(obj, value);

                            break;
                        case MemberTypes.Property:
                            value = config[memberCfgAttribute.Group][memberCfgAttribute.Name];
                            (member as PropertyInfo).SetValue(obj, value);

                            break;
                        default:
                            value = null;

                            break;
                    }
                }
            }

            return obj;
        }

        public string[] Serialize(T obj)
        {
            if (!Utils.IsSerialiable(obj))
                throw new InvalidOperationException("The given class is not serializable!");

            if (!Utils.IsConfigurationClass(obj))
                throw new InvalidOperationException("The given class is not a configuration object!");

            Dictionary<string, Dictionary<string, object>> groupsValuesPairs = new Dictionary<string, Dictionary<string, object>>();
            Action<MemberInfo, ConfigurationFieldAttribute> AddMember = (MemberInfo m, ConfigurationFieldAttribute a) =>
            {
                switch (m.MemberType)
                {
                    case MemberTypes.Field:
                        groupsValuesPairs[a.Group].Add(a.Name, (m as FieldInfo).GetValue(obj));
                        break;
                    case MemberTypes.Property:
                        groupsValuesPairs[a.Group].Add(a.Name, (m as PropertyInfo).GetValue(obj));
                        break;
                }
            };

            foreach (var member in Utils.GetAllConfigurationMembers(obj))
            {
                var attribute = Utils.ExtractMemberAttribute(member);

                if (!groupsValuesPairs.ContainsKey(attribute.Group))
                {                    
                    groupsValuesPairs.Add(attribute.Group, new Dictionary<string, object>());
                    AddMember(member, attribute);
                }
                else
                {
                    AddMember(member, attribute);
                }
            }

            string[] data = Parser.WriteRawConfigData(groupsValuesPairs);

            return data;
        }
    }
}

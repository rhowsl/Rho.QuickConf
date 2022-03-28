using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rho.QuickConf
{
    public static class ConfigurationReader
    {
        /// <summary>
        /// Deserializes configuration data into an object with marked fields.
        /// </summary>
        /// <param name="configurationObject"></param>
        /// <param name="data">ini data (multi-line string)</param>
        public static void DeserializeInto(object configurationObject, string[] data)
        {
            if (!Utils.IsConfigurationClass(configurationObject))
                throw new InvalidOperationException("The given class is not a configuration object!");

            // parse the file
            var config = Parser.ReadRawConfigData(data);

            var members = configurationObject.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
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
                            (member as FieldInfo).SetValue(configurationObject, value);

                            break;
                        case MemberTypes.Property:
                            value = config[memberCfgAttribute.Group][memberCfgAttribute.Name];
                            (member as PropertyInfo).SetValue(configurationObject, value);

                            break;
                        default:
                            value = null;

                            break;
                    }
                }
            }
        }
    }
}

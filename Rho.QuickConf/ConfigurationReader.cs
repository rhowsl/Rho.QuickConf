using System;
using System.Collections.Generic;
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

            List<ConfigurationFieldAttribute> fields = new List<ConfigurationFieldAttribute>();
            foreach (var field in configurationObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var fieldAttribute = Utils.ExtractFieldAttribute(field);

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

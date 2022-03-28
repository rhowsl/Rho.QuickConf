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

            var fields = configurationObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var properties = configurationObject.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var fieldAttribute = Utils.ExtractMemberAttribute(field);

                if (field.IsInitOnly)
                    throw new MemberAccessException($"Field {field.Name} is read-only.");

                if (!(fieldAttribute is null))
                {
                    object value = config[fieldAttribute.Group][fieldAttribute.Name];
                    field.SetValue(configurationObject, value);
                }
            }

            foreach (var property in properties)
            {
                var propertyAttribute = Utils.ExtractMemberAttribute(property);

                // HACK: this is being done without accessibility checks

                if (!(propertyAttribute is null))
                {
                    object value = config[propertyAttribute.Group][propertyAttribute.Name];
                    property.SetValue(configurationObject, value);
                }
            }
        }
    }
}

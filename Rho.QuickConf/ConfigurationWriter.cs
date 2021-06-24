﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf
{
    public static class ConfigurationWriter
    {
        public static string[] SerializeFrom(object configurationObject)
        {
            if (!Utils.IsSerialiable(configurationObject))
                throw new InvalidOperationException("The given class is not serializable!");

            if (!Utils.IsConfigurationClass(configurationObject))
                throw new InvalidOperationException("The given class is not a configuration object!");

            Dictionary<string, Dictionary<string, object>> groupsValuesPairs = new Dictionary<string, Dictionary<string, object>>();

            foreach (var field in Utils.GetAllConfigurationFields(configurationObject))
            {
                var attribute = Utils.ExtractFieldAttribute(field);

                if (!groupsValuesPairs.ContainsKey(attribute.Group))
                {
                    groupsValuesPairs.Add(attribute.Group, new Dictionary<string, object>());
                }
                else
                {
                    groupsValuesPairs[attribute.Group].Add(attribute.Name, field.GetValue(configurationObject));
                }
            }

            string[] data = Parser.WriteRawConfigData(groupsValuesPairs);

            return data;
        }
    }
}
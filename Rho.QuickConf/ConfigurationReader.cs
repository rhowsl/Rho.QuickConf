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

        private static Field ExtractFieldAttribute(FieldInfo fieldMember)
        {
            var attribute = fieldMember.GetCustomAttribute(typeof(Field));
            if (!(attribute is null))
            {
                var fieldAttribute = attribute as Field;
                return new Field()
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

            // Parser prep
            int cursor = 0;
            string[] data = File.ReadAllLines(fileName);

            // List out fields that need to be checked and shit
            List<Field> fields = new List<Field>();
            foreach (var field in configurationObject.GetType().GetFields())
            {
                var fieldAttribute = ExtractFieldAttribute(field);

                if (!(field is null))
                    if (!(fieldAttribute is null))
                    {
                        // Error out if field is read only
                        if (field.IsInitOnly || field.IsLiteral)
                            throw new AccessViolationException($"{field.Name} is a read-only field!");

                        if (fieldAttribute.Group == string.Empty)
                        {
                            IEnumerable<string> value = from line in data where line.StartsWith(fieldAttribute.Name) select line;
                            // Error out if there is more than one value
                            if (value.Count() > 1)
                                throw new MemberAccessException($"More than one value with the name {fieldAttribute.Name} outside of any group");

                            field.SetValue(configurationObject, value.FirstOrDefault());
                        }
                        else
                        {
                            IEnumerable<string> group = from line in data where line == $"[{fieldAttribute.Group}]" select line;

                            if (group.Count() > 1)
                                throw new MemberAccessException($"There are more than one groups with {fieldAttribute.Group} on the file {fileName}");

                            cursor = Array.IndexOf(data, group.FirstOrDefault());

                            while (true)
                            {
                                cursor++;

                                if (Parser.GetValueName(data[cursor]) == fieldAttribute.Name)
                                {
                                    field.SetValue(configurationObject, group.FirstOrDefault());
                                    field.SetValue(configurationObject, Parser.GetValue(data[cursor]));
                                    break;
                                }

                                if (cursor > data.Length)
                                    throw new Exception($"There is no value named {fieldAttribute.Name} in group {fieldAttribute.Group}");
                            }
                        }
                    }
            }
        }
    }
}

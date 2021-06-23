using System.Collections.Generic;
using System.IO;

namespace Rho.QuickConf
{
    static class ConfigParser
    {
        private static string RemoveTrailing(this string str) =>
            str.TrimStart().TrimEnd();
        public static bool IsComment(string line) =>
            line.StartsWith("#");
        public static bool IsGroup(string line) =>
            line.StartsWith("[") && line.EndsWith("]");
        public static bool IsValue(string line) =>
            line.Split('=', 2).Length > 1;

        public static string ReadValue(string line) =>
            line.Split('=', 2)[1];
        public static string ReadValueName(string line) =>
            line.Split('=', 2)[0];

        public static Dictionary<string, Dictionary<string, object>> ReadRawConfigFile(string fileName)
        {
            Dictionary<string, Dictionary<string, object>> config =
                new Dictionary<string, Dictionary<string, object>>()
                {
                    {  string.Empty, new Dictionary<string, object>() }
                };

            int cursor;
            string currentGroup = string.Empty;
            string[] data = File.ReadAllLines(fileName);

            for (cursor = 0; cursor < data.Length; cursor++)
            {
                string line = data[cursor];

                if (IsComment(line))
                    continue;
                else if (IsGroup(line))
                {
                    // this is stupid and unfunny
                    currentGroup = $"{line.Replace("[", "").Replace("]", "").RemoveTrailing()}";

                    config.Add(currentGroup, new Dictionary<string, object>());
                }
                else if (IsValue(line))
                {
                    if (!config.ContainsKey(currentGroup))
                        config.Add(currentGroup, new Dictionary<string, object>());

                    config[currentGroup].Add(ReadValueName(line), ReadValue(line));
                }                
            }

            return config;
        }
    }
}

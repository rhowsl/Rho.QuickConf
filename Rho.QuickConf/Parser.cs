namespace Rho.QuickConf
{
    static class Parser
    {
        public static bool IsComment(string line) =>
            line.StartsWith("#");
        public static bool IsGroup(string line) =>
            line.StartsWith("[") && line.EndsWith("]");
        public static string GetValue(string line) =>
            line.Split('=', 2)[1];
        public static string GetValueName(string line) =>
            line.Split('=', 2)[0];
    }
}

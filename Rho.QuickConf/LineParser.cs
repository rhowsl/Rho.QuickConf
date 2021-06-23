namespace Rho.QuickConf
{
    static class LineParser
    {
        public static bool IsComment(string line) =>
            line.StartsWith("#");
        public static bool IsGroup(string line) =>
            line.StartsWith("[") && line.EndsWith("]");
        public static string ReadValue(string line) =>
            line.Split('=', 2)[1];
        public static string ReadValueName(string line) =>
            line.Split('=', 2)[0];
    }
}

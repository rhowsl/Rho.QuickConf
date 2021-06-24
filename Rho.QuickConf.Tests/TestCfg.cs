using System;

namespace Rho.QuickConf.Tests
{
    [ConfigurationFile]
    [Serializable]
    public class TestCfg
    {
        [ConfigurationField("", "Value1")]
        private string vn_1;

        [ConfigurationField("Group A", "Value2")]
        private string va_1;

        public string Value1N => vn_1;
        public string Value1A => va_1;
    }
}
using System;

namespace Rho.QuickConf.Tests
{
    [ConfigurationFile]
    [Serializable]
    public class TestCfg
    {
        [ConfigurationField("", "Value1")]
        public string vn_1;

        [ConfigurationField("Group A", "Value2")]
        public string va_1;
    }
}
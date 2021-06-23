using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf.Tests
{
    [ConfigurationFile("Test.conf")]
    public class TestCfg
    {
        [ConfigurationField(name: "Value1")]
        public string FirstValue;

        [ConfigurationField("Group A", "Value1")]
        public string value1;

        public string Value1 => value1;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf.Tests
{
    [ConfigurationFile("Test.conf")]
    public class TestCfg
    {
        [ConfigurationField("Group A", "Value1")]
        public readonly string Fuck;
    }
}

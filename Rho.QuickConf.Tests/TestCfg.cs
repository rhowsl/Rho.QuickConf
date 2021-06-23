using System;
using System.Collections.Generic;
using System.Text;

namespace Rho.QuickConf.Tests
{
    [ConfigurationFile("Test.conf")]
    public class TestCfg
    {
        [Field("Group A", "Value1")]
        public readonly string Fuck;
    }
}

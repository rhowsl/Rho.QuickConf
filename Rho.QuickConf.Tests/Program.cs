using System;

namespace Rho.QuickConf.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCfg t = new TestCfg();
            Configuration.ParseInto(t);

            Console.WriteLine($"value of fuck is {t.Fuck}");
        }
    }
}

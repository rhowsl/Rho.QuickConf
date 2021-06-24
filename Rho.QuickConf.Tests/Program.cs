using System;
using System.IO;

namespace Rho.QuickConf.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCfg t = new TestCfg();
            ConfigurationSerializer.DeserializeInto(t, File.ReadAllLines("Test.conf"));

            Console.WriteLine($"{t.va_1}");
            Console.WriteLine($"{t.vn_1}");
        }
    }
}

using System;
using System.IO;

namespace Rho.QuickConf.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCfg t = new TestCfg();
            ConfigurationReader.DeserializeInto(t, File.ReadAllLines("Test.conf"));

            Console.WriteLine($"{t.Value1A}");
            Console.WriteLine($"{t.Value1N}");
            Console.WriteLine();

            var w = ConfigurationWriter.SerializeFrom(t);
            foreach (var line in w) Console.WriteLine(line);
        }
    }
}

using System;
using System.IO;

namespace Rho.QuickConf.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration t = new Configuration();
            ConfigurationReader.DeserializeInto(t, File.ReadAllLines("Test.conf"));

            var w = ConfigurationWriter.SerializeFrom(t);
            foreach (var line in w) Console.WriteLine(line);

            Configuration2 t2 = new Configuration2();
            ConfigurationReader.DeserializeInto(t2, File.ReadAllLines("Test.conf"));

            Console.WriteLine();
        }
    }
}

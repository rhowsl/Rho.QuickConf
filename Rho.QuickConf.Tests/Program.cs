using System;
using System.IO;

namespace Rho.QuickConf.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationSerializer<Configuration> ser = new ConfigurationSerializer<Configuration>();
            ConfigurationSerializer<Configuration2> ser2 = new ConfigurationSerializer<Configuration2>();

            Configuration t = ser.Deserialize(File.ReadAllLines("Test.conf"));

            // var w = ser.Serialize(t);
            // foreach (var line in w) Console.WriteLine(line);

            Configuration2 t2 = ser2.Deserialize(File.ReadAllLines("Test.conf"));

            var w2 = ser2.Serialize(t2);
            foreach (var line in w2) Console.WriteLine(line);

            Console.WriteLine();
        }
    }
}

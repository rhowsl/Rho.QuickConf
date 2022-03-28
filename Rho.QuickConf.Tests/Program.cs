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

            Configuration t = new Configuration();
            ser.Deserialize(t, File.ReadAllLines("Test.conf"));

            var w = ser.Serialize(t);
            foreach (var line in w) Console.WriteLine(line);

            Configuration2 t2 = new Configuration2();
            ser2.Deserialize(t2, File.ReadAllLines("Test.conf"));

            Console.WriteLine();
        }
    }
}

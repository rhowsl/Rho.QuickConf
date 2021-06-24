using System;

namespace Rho.QuickConf.Tests
{
    [ConfigurationFile]
    [Serializable]
    public class Configuration
    {
        [ConfigurationField("", "Value1")]
        string v1;
        [ConfigurationField("", "Value2")]
        string v2;
        [ConfigurationField("", "Value3")]
        string v3;

        [ConfigurationField("Controls", "ButtonA")]
        string bt_a;
        [ConfigurationField("Controls", "ButtonB")]
        string bt_b;
        [ConfigurationField("Controls", "ButtonC")]
        string bt_c;
        [ConfigurationField("Controls", "ButtonD")]
        string bt_d;

        [ConfigurationField("Options", "LimitFramerate")]
        string limit_fps;
        [ConfigurationField("Options", "MaxFramerate")]
        string max_fps;

        public string Value1 => v1;
        public string Value2 => v2;
        public string Value3 => v3;

        public string ButtonA => bt_a;
        public string ButtonB => bt_b;
        public string ButtonC => bt_c;
        public string ButtonD => bt_d;

        public string LimitFramerate => limit_fps;
        public string MaxFramerate => max_fps;
    }
}
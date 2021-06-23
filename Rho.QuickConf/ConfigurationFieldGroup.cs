using System.Collections.Generic;

namespace Rho.QuickConf
{
    public abstract class ConfigurationFieldGroup
    {
        public string GroupName { get; }
        public Dictionary<string, object> Fields { get; }
    }
}

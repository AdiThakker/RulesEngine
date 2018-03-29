using System.Configuration;

namespace DynamicRulesEngine.Configuration
{
    public class RulesEngineSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public RuleElementCollection Rules => (RuleElementCollection)base[""];
    }
}

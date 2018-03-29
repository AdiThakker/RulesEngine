using System.Configuration;

namespace DynamicRulesEngine.Configuration
{
    public class RuleDecisionsElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new RuleDecisionElement();

        protected override object GetElementKey(ConfigurationElement element) => ((RuleDecisionElement)element);

        protected override string ElementName => "RuleDecision";

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;
    }
    public class RuleDecisionElement : ConfigurationElement
    {
        [ConfigurationProperty("propertyName", IsRequired = true)]
        public string PropertyName
        {
            get { return (string)this["propertyName"]; }
            set { this["propertyName"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}

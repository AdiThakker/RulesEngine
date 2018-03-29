using System.Configuration;

namespace DynamicRulesEngine.Configuration
{
    public class RuleEvaluationsElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new RuleEvaluationElement();

        protected override object GetElementKey(ConfigurationElement element) => ((RuleEvaluationElement)element);

        protected override string ElementName => "RuleEvaluation";

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;
    }

    public class RuleEvaluationElement : ConfigurationElement
    {
        [ConfigurationProperty("propertyName", IsRequired = true)]
        public string PropertyName
        {
            get { return (string)this["propertyName"]; }
            set { this["propertyName"] = value; }
        }

        [ConfigurationProperty("criteria", IsRequired = true)]
        public string Criteria
        {
            get { return (string)this["criteria"]; }
            set { this["criteria"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }

}

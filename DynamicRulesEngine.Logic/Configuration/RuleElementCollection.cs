using System.Configuration;

namespace DynamicRulesEngine.Configuration
{
    public class RuleElementCollection : ConfigurationElementCollection
    {
        protected override string ElementName => "Rule";

        protected override ConfigurationElement CreateNewElement() => new RuleElement();

        protected override object GetElementKey(ConfigurationElement element) => ((RuleElement)element).Priority;

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;
    }

    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("priority", IsRequired = true)]
        public string Priority
        {
            get { return (string)this["priority"]; }
            set { this["priority"] = value; }
        }

        [ConfigurationProperty("RuleEvaluations", IsDefaultCollection = true)]
        public RuleEvaluationsElementCollection RuleEvaluations { get { return (RuleEvaluationsElementCollection)base["RuleEvaluations"]; } }

        [ConfigurationProperty("RuleDecisions", IsDefaultCollection = true)]
        public RuleDecisionsElementCollection RuleDecisions { get { return (RuleDecisionsElementCollection)base["RuleDecisions"]; } }
    }
}


# RulesEngine

A dynamic rules engine which can be used to execute simple logic / decisions via configuration using Expression Trees.

e.g. of a Rule Configuration
```
<RulesEngineSection>
	<Rule group = "" priority = "">
		<RuleEvaluations>
	       		<RuleEvaluation propertyName ="Timestamp" criteria ="equal" value="NOREAD" />
	    	</RuleEvaluations>
	    	<RuleDecisions>
	    		<RuleDecision propertyName ="Status" value ="Failure" />
	    	</RuleDecisions>
	</Rule>	
	<Rule group = "" priority = "">
	    	<RuleEvaluations>
	       		<RuleEvaluation propertyName ="Timestamp" criteria ="notEqual" value="NOREAD" />
	       		<RuleEvaluation propertyName ="Authorized" criteria ="equal" value="" />		   
	    	</RuleEvaluations>
	    	<RuleDecisions>
	    		<RuleDecision propertyName ="Status" value ="Pass" />
	        	<RuleDecision propertyName ="Processed" value ="False" />
	    	</RuleDecisions>
	</Rule>	
</RulesEngineSection>
```



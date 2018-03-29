using DynamicRulesEngine.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicRulesEngine
{
    public class Engine<TContext>
    {
        private static Dictionary<string, Func<TContext, bool>> _rules = new Dictionary<string, Func<TContext, bool>>();

        public static List<Func<TContext, bool>> Initialize(RulesEngineSection configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            configuration
                .Rules
                .Cast<RuleElement>()
                .ToList()
                .ForEach(rule => _rules.Add(rule.Priority, GenerateRuleExpression(rule).Compile() as Func<TContext, bool>));

            return _rules.Values.ToList();
        }

        private static Expression<Func<TContext, bool>> GenerateRuleExpression(RuleElement rule)
        {
            try
            {
                var parameterExpression = Expression.Parameter(typeof(TContext), "obj");

                List<Expression> blockExpressions = new List<Expression>();
                
                // Build Evaluation rules
                Expression finalBinaryExpression = Expression.Constant(true);
                var evaluations = rule.RuleEvaluations.Cast<RuleEvaluationElement>().ToList();
                var decisions = rule.RuleDecisions.Cast<RuleDecisionElement>().ToList();

                evaluations
                    .ForEach(ruleEvaluation =>
                    {
                        var memberExpression = MemberExpression.Property(parameterExpression, ruleEvaluation.PropertyName);
                        var constantExpression = MemberExpression.Constant(ruleEvaluation.Value, typeof(string));
                        ExpressionType binaryExpressionType;
                        if (ExpressionType.TryParse(ruleEvaluation.Criteria, true, out binaryExpressionType))
                        {
                            var binaryExpression = Expression.MakeBinary(binaryExpressionType, memberExpression, constantExpression);
                            finalBinaryExpression = Expression.And(finalBinaryExpression, binaryExpression);
                        }
                    });

                var conditionalExpression = Expression.IfThen(finalBinaryExpression, GenerateRuleDecisionExpression(decisions, ref parameterExpression));
                blockExpressions.Add(conditionalExpression);

                LabelTarget returnTarget = Expression.Label(typeof(bool));
                blockExpressions.Add(Expression.Return(returnTarget, Expression.Constant(true), typeof(bool)));
                blockExpressions.Add(Expression.Label(returnTarget, Expression.Constant(true)));
                BlockExpression lambdaBodyExpression = Expression.Block(blockExpressions);

                // Compile and return
                var lambdaExpression = Expression.Lambda<Func<TContext, bool>>(lambdaBodyExpression, parameterExpression);
                return lambdaExpression; 

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Expression GenerateRuleDecisionExpression(List<RuleDecisionElement> decisions, ref ParameterExpression parameterExpression)
        {
            List<Expression> blockExpressions = new List<Expression>();

            foreach (var decision in decisions)
            {
                var memberExpression = MemberExpression.Property(parameterExpression, decision.PropertyName);
                var constantExpression = MemberExpression.Constant(decision.Value, typeof(string));
                blockExpressions.Add(Expression.Assign(memberExpression, constantExpression));
            }

            return Expression.Block(blockExpressions);
        }
    }
}

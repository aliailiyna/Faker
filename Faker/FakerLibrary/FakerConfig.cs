using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public class FakerConfig : IFakerConfig
    {
        private List<(Type, string, Type, IGenerator)> configGeneratorsList = 
            new List<(Type, string, Type, IGenerator)>();

        public void Add<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> fieldGetExpr, IGenerator generator)
        {
            MemberInfo classField = ParseFieldGetExpression(fieldGetExpr);
            if (classField != null && generator.GetObjectType().Equals(typeof(ResultType)))
            {
                Console.WriteLine("success");
                if (!configGeneratorsList.Contains((typeof(ParameterType), classField.Name, typeof(ResultType), generator)))
                {
                    configGeneratorsList.Add((typeof(ParameterType), classField.Name, typeof(ResultType), generator));
                }
            }
        }

        private MemberInfo ParseFieldGetExpression<ClassType, ResultType>(Expression<Func<ClassType, ResultType>> fieldGetExpr)
        {
            if ((fieldGetExpr.Body.NodeType == ExpressionType.MemberAccess))
            {
                MemberExpression memberExpression = fieldGetExpr.Body as MemberExpression;
                return memberExpression.Member;
            }
            else
            {
                return null;
            }
        }

        public List<(Type, string, Type, IGenerator)> GetConfigGenerators()
        {
            return configGeneratorsList;
        }
    }
}

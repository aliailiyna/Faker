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

        private List<(Type, string, Type, ICollectionGenerator)> configCollectionGeneratorsList =
            new List<(Type, string, Type, ICollectionGenerator)>();
        public void AddGenerator<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> fieldGetExpr, IGenerator generator)
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

        public void AddCollectionGenerator<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> fieldGetExpr, ICollectionGenerator generator)
        {
            MemberInfo classField = ParseFieldGetExpression(fieldGetExpr);
            if (classField != null && generator.GetCollectionType().Equals(typeof(ResultType)))
            {
                Console.WriteLine("success");
                Console.WriteLine(typeof(ParameterType).ToString());
                Console.WriteLine(classField.Name);
                Console.WriteLine(typeof(ResultType).ToString());
                if (!configCollectionGeneratorsList.Contains((typeof(ParameterType), classField.Name, typeof(ResultType), generator)))
                {
                    configCollectionGeneratorsList.Add((typeof(ParameterType), classField.Name, typeof(ResultType), generator));
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

        public List<(Type, string, Type, ICollectionGenerator)> GetConfigCollectionGenerators()
        {
            return configCollectionGeneratorsList;
        }
    }
}

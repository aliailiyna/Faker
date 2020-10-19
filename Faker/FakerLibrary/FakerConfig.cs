using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public class FakerConfig : IFakerConfig
    {
        // структура для работы с IGenerator
        private List<(Type, string, Type, IGenerator)> configGeneratorsList = 
            new List<(Type, string, Type, IGenerator)>();

        // структура для работы с ICollectionGenerator
        private List<(Type, string, Type, ICollectionGenerator)> configCollectionGeneratorsList =
            new List<(Type, string, Type, ICollectionGenerator)>();


        // добавление IGenerator
        public void AddGenerator<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> fieldGetExpr, IGenerator generator)
        {
            MemberInfo classField = ParseFieldGetExpression(fieldGetExpr);
            if (classField != null && generator.GetObjectType().Equals(typeof(ResultType)))
            {
                if (!configGeneratorsList.Contains((typeof(ParameterType), classField.Name, typeof(ResultType), generator)))
                {
                    configGeneratorsList.Add((typeof(ParameterType), classField.Name, typeof(ResultType), generator));
                }
            }
        }

        // добавление ICollectionGenerator
        public void AddCollectionGenerator<ParameterType, ResultType>(Expression<Func<ParameterType, ResultType>> fieldGetExpr, ICollectionGenerator generator)
        {
            MemberInfo classField = ParseFieldGetExpression(fieldGetExpr);
            if (classField != null && generator.GetCollectionType().Equals(typeof(ResultType)))
            {
                if (!configCollectionGeneratorsList.Contains((typeof(ParameterType), classField.Name, typeof(ResultType), generator)))
                {
                    configCollectionGeneratorsList.Add((typeof(ParameterType), classField.Name, typeof(ResultType), generator));
                }
            }
        }

        // обработка дерева выражений
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

        // возвращение словаря настраиваемых генераторов IGenerator
        public Dictionary<Type, Dictionary<(string, Type), IGenerator>> GetConfigGenerators()
        {
            Dictionary<Type, Dictionary<(string, Type), IGenerator>> configGeneratorsDictionary = new Dictionary<Type, Dictionary<(string, Type), IGenerator>>();
            foreach (var generatorInfo in configGeneratorsList)
            {
                if (!configGeneratorsDictionary.ContainsKey(generatorInfo.Item1))
                {
                    configGeneratorsDictionary.Add(generatorInfo.Item1, new Dictionary<(string, Type), IGenerator>());
                }
                configGeneratorsDictionary[generatorInfo.Item1].Add((generatorInfo.Item2, generatorInfo.Item3), generatorInfo.Item4);
                Console.WriteLine(generatorInfo.Item4.GetType().Name);
            }
            return configGeneratorsDictionary;
        }

        // возвращение словаря настраиваемых генераторов ICollectionGenerator
        public Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>> GetConfigCollectionGenerators()
        {
            Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>> configGeneratorsCollectionDictionary = new Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>>();
            foreach (var generatorInfo in configCollectionGeneratorsList)
            {
                if (!configGeneratorsCollectionDictionary.ContainsKey(generatorInfo.Item1))
                {
                    configGeneratorsCollectionDictionary.Add(generatorInfo.Item1, new Dictionary<(string, Type), ICollectionGenerator>());
                }
                configGeneratorsCollectionDictionary[generatorInfo.Item1].Add((generatorInfo.Item2, generatorInfo.Item3), generatorInfo.Item4);
                Console.WriteLine(generatorInfo.Item4.GetType().Name);
            }
            return configGeneratorsCollectionDictionary;
        }
    }
}

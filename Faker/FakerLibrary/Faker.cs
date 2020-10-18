using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DTOAttributeLibrary;
using FakerInterfaceLibrary;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public class Faker : IFaker
    {
        private readonly Dictionary<Type, IGenerator> standardGeneratorsDictionary;
        private readonly Dictionary<Type, ICollectionGenerator> standardCollectionGeneratorsDictionary;
        private readonly Dictionary<Type, IGenerator> pluginGeneratorsDictionary;
        private readonly Dictionary<Type, ICollectionGenerator> pluginCollectionGeneratorsDictionary;
        private readonly Dictionary<ConfigKey, IGenerator> configGeneratorsDictionary;
        private readonly Dictionary<ConfigKey, ICollectionGenerator> configCollectionGeneratorsDictionary;

        private IFakerLoader fakerLoader = new FakerLoader();
        public Faker()
        {
            standardGeneratorsDictionary = fakerLoader.getStandardGenerators();
            standardCollectionGeneratorsDictionary = fakerLoader.getStandardCollectionGenerators();
            pluginGeneratorsDictionary = fakerLoader.getPluginGenerators();
            pluginCollectionGeneratorsDictionary = fakerLoader.getPluginCollectionGenerators();
        }

        //public Faker(FakerConfig fakerConfig)
        //{
        //}

        public type Create<type>()
        {
            return (type)Create(typeof(type), null, null);
        }

        private object Create(Type objectType, string name, Type propertyOrFieldType)
        {
            object createdObject;
            if (IsStandardGenerator(objectType))
                createdObject = GenerateStandard(objectType);
            else if (IsStandardCollectionGenerator(objectType))
                createdObject = GenerateCollectionStandard(objectType);
            else if (IsPluginGenerator(objectType))
                createdObject = GeneratePlugin(objectType);
            else if (IsPluginCollectionGenerator(objectType))
                createdObject = GenerateCollectionPlugin(objectType);
            else if (IsDTO(objectType))
                createdObject = GenerateDTO(objectType);
            else
                createdObject = GenerateDefaultValue(objectType);

            return createdObject;
        }

        private bool IsStandardGenerator(Type objectType)
        {
            return standardGeneratorsDictionary.ContainsKey(objectType);
        }

        private object GenerateStandard(Type objectType)
        {
            return standardGeneratorsDictionary[objectType].GenerateObject();
        }

        private bool IsStandardCollectionGenerator(Type objectType)
        {
            return standardCollectionGeneratorsDictionary.ContainsKey(objectType);
        }

        private object GenerateCollectionStandard(Type objectType)
        {
            Type elementType = objectType.GetGenericArguments()[0];
            return standardCollectionGeneratorsDictionary[objectType].GenerateCollection(elementType, this);
        }
        private bool IsPluginGenerator(Type objectType)
        {
            return pluginGeneratorsDictionary.ContainsKey(objectType);
        }

        private object GeneratePlugin(Type objectType)
        {
            return pluginGeneratorsDictionary[objectType].GenerateObject();
        }

        private bool IsPluginCollectionGenerator(Type objectType)
        {
            return pluginCollectionGeneratorsDictionary.ContainsKey(objectType);
        }

        private object GenerateCollectionPlugin(Type objectType)
        {
            Type elementType = objectType.GetGenericArguments()[0];
            return pluginCollectionGeneratorsDictionary[objectType].GenerateCollection(elementType, this);
        }

        private bool IsDTO(Type objectType)
        {
            return objectType.GetCustomAttribute(typeof(DTOAttribute)) != null;
        }

        private object GenerateDTO(Type objectType)
        {
            object DTOObject = CreateObject(objectType);

            PropertyInfo[] properties = objectType.GetProperties().Where(property => property.GetSetMethod() != null).ToArray();
            SetOjectPoperties(DTOObject, properties);

            FieldInfo[] fields = objectType.GetFields();
            SetObjectFields(DTOObject, fields);

            return DTOObject;
        }

        private object CreateObject(Type objectType)
        {
            ConstructorInfo[] constructorInfos = GetObjectConstructors(objectType);
            ConstructorInfo bestConstructor = ChooseBestConstructor(constructorInfos);
            return InvokeConstructor(bestConstructor);
        }

        private ConstructorInfo[] GetObjectConstructors(Type objectType)
        {
            ConstructorInfo[] constructors = objectType.GetConstructors();
            if (constructors.Length == 0)
                constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            return constructors;
        }

        private ConstructorInfo ChooseBestConstructor(ConstructorInfo[] constructorInfos)
        {
            ConstructorInfo[] suitableConstructors = constructorInfos;
            return suitableConstructors.Aggregate((firstCI, secondCI) =>
                firstCI.GetParameters().Length > secondCI.GetParameters().Length ? firstCI : secondCI);
        }

        private ConstructorInfo[] SelectConstructorsByFields(ConstructorInfo[] constructorInfos,
                                Dictionary<(string, Type), IGenerator> generatorsForClass)
        {
            return constructorInfos.Where(constructorInfo =>
            {
                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
                    if (generatorsForClass.ContainsKey((parameterInfo.Name, parameterInfo.ParameterType)))
                        return true;
                return false;
            }).ToArray();
        }

        private object InvokeConstructor(ConstructorInfo constructor)
        {
            List<object> parameters = new List<object>();
            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
                parameters.Add(Create(parameterInfo.ParameterType, parameterInfo.Name, constructor.DeclaringType));
            }
            return constructor.Invoke(parameters.ToArray());
        }

        private void SetOjectPoperties(object obj, PropertyInfo[] properties)
        {
            foreach (PropertyInfo property in properties)
                property.SetValue(obj, Create(property.PropertyType, property.Name, obj.GetType()));
        }

        private void SetObjectFields(object obj, FieldInfo[] fields)
        {
            foreach (FieldInfo field in fields)
                field.SetValue(obj, Create(field.FieldType, field.Name, obj.GetType()));
        }

        private object GenerateDefaultValue(Type objectType)
        {
            return default;
        }
    }
}

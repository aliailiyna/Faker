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
        private readonly Dictionary<Type, Dictionary<(string, Type), IGenerator>> configGeneratorsDictionary =
            new Dictionary<Type, Dictionary<(string, Type), IGenerator>>();
        private readonly Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>> configCollectionGeneratorsDictionary =
            new Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>>();
        // структура обрабатываемых типов, обеспечивающая разрешение 
        // циклических зависимостей на любом уровне вложенности
        private Queue<Type> processingDTOObjects = new Queue<Type>();

        private IFakerLoader fakerLoader = new FakerLoader();
        public Faker()
        {
            standardGeneratorsDictionary = fakerLoader.getStandardGenerators();
            standardCollectionGeneratorsDictionary = fakerLoader.getStandardCollectionGenerators();
            pluginGeneratorsDictionary = fakerLoader.getPluginGenerators();
            pluginCollectionGeneratorsDictionary = fakerLoader.getPluginCollectionGenerators();
        }

        public Faker(IFakerConfig fakerConfig) : this()
        {
            List<(Type, string, Type, IGenerator)> configGeneratorsList = fakerConfig.GetConfigGenerators();
            foreach (var generatorInfo in configGeneratorsList)
            {
                if (!configGeneratorsDictionary.ContainsKey(generatorInfo.Item1))
                {
                    configGeneratorsDictionary.Add(generatorInfo.Item1, new Dictionary<(string, Type), IGenerator>());
                }
                configGeneratorsDictionary[generatorInfo.Item1].Add((generatorInfo.Item2, generatorInfo.Item3), generatorInfo.Item4);
            }
        }

        public type Create<type>()
        {
            return (type)Create(typeof(type));
        }

        public object Create(Type objectType)
        {
            return Create(objectType, null, null);
        }

        private object Create(Type objectType, string name, Type classType)
        {
            object createdObject;
            if (IsConfigGenerator(classType, name, objectType))
                createdObject = GenerateUsingUserGenerator(classType, name, objectType);
            else if (IsStandardGenerator(objectType))
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

        private bool IsConfigGenerator(Type classType, string name, Type returnType)
        {
            if (classType == typeof(byte) && name == "ConfigNumber")
            {
                Console.WriteLine("is");
                if (configGeneratorsDictionary.ContainsKey(classType))
                {
                    if (configGeneratorsDictionary[classType].ContainsKey((name, returnType)))
                    {

                    }
                }
            }
            else
            {
                if (classType != null)
                Console.WriteLine("now " + classType.FullName.ToString() + " " + name);
            }
            return classType != null && name != null &&
                configGeneratorsDictionary.ContainsKey(classType) && configGeneratorsDictionary[classType].ContainsKey((name, returnType));
        }

        private object GenerateUsingUserGenerator(Type classType, string memberName, Type returnType)
        {
            return configGeneratorsDictionary[classType][(memberName, returnType)].GenerateObject();
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
            if (objectType.GetInterface(typeof(IList<>).Name) != null)
            {
                Type collectionType = objectType.GetGenericTypeDefinition();
                return standardCollectionGeneratorsDictionary.ContainsKey(collectionType);
            }
            return false;
        }

        private object GenerateCollectionStandard(Type objectType)
        {
            Type collectionType = objectType.GetGenericTypeDefinition();
            Type elementType = objectType.GetGenericArguments()[0];
            return standardCollectionGeneratorsDictionary[collectionType].GenerateCollection(elementType, this);
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
            if (objectType.GetInterface(typeof(IList<>).Name) != null)
            {
                Type collectionType = objectType.GetGenericTypeDefinition();
                return pluginCollectionGeneratorsDictionary.ContainsKey(collectionType);
            }
            return false;
        }

        private object GenerateCollectionPlugin(Type objectType)
        {
            Type collectionType = objectType.GetGenericTypeDefinition();
            Type elementType = objectType.GetGenericArguments()[0];
            return pluginCollectionGeneratorsDictionary[collectionType].GenerateCollection(elementType, this);
        }

        private bool IsDTO(Type objectType)
        {
            return objectType.GetCustomAttribute(typeof(DTOAttribute)) != null;
        }

        private object GenerateDTO(Type objectType)
        {
            // возвращение значения по умолчанию в случае если данный тип обрабатывается
            if (processingDTOObjects.Contains(objectType))
            {
                return GenerateDefaultValue(objectType);
            }

            // добавление типа в очередь обрабатываемых типов
            processingDTOObjects.Enqueue(objectType);

            object DTOObject = CreateObject(objectType);

            PropertyInfo[] properties = objectType.GetProperties().Where(property => property.GetSetMethod() != null).ToArray();
            SetOjectPoperties(DTOObject, properties);

            FieldInfo[] fields = objectType.GetFields();
            SetObjectFields(DTOObject, fields);

            // извлечение типа из очереди обрабатываемых типов
            processingDTOObjects.Dequeue();

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
            {
                constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            }
            return constructors;
        }

        private ConstructorInfo ChooseBestConstructor(ConstructorInfo[] constructorInfos)
        {
            ConstructorInfo[] suitableConstructors = constructorInfos;
            return suitableConstructors.Aggregate((firstCI, secondCI) =>
                firstCI.GetParameters().Length > secondCI.GetParameters().Length ? firstCI : secondCI);
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

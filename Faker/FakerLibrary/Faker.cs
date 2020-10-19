using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DTOAttributeLibrary;
using FakerInterfaceLibrary;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public class Faker : IFaker
    {
        // структуры для хранения стандартных генераторов
        private readonly Dictionary<Type, IGenerator> standardGeneratorsDictionary;
        private readonly Dictionary<Type, ICollectionGenerator> standardCollectionGeneratorsDictionary;
        // структуры для хранения генераторов, загружаемых из плагинов
        private readonly Dictionary<Type, IGenerator> pluginGeneratorsDictionary;
        private readonly Dictionary<Type, ICollectionGenerator> pluginCollectionGeneratorsDictionary;

        // структуры для хранения настраеваемых генераторов
        private readonly Dictionary<Type, Dictionary<(string, Type), IGenerator>> configGeneratorsDictionary;
        private readonly Dictionary<Type, Dictionary<(string, Type), ICollectionGenerator>> configCollectionGeneratorsDictionary;

        // структура обрабатываемых типов, обеспечивающая разрешение 
        // циклических зависимостей на любом уровне вложенности
        private Queue<Type> processingDTOObjects = new Queue<Type>();

        // создание загрузчика стандартных генераторов и генераторов из плагинов
        private IFakerLoader fakerLoader = new FakerLoader();

        // загрузка стандартных генераторов и генераторов из плагинов
        public Faker()
        {
            // загрузка стандартных генераторов
            standardGeneratorsDictionary = fakerLoader.getStandardGenerators();
            standardCollectionGeneratorsDictionary = fakerLoader.getStandardCollectionGenerators();

            // загрузка генераторов из 
            pluginGeneratorsDictionary = fakerLoader.getPluginGenerators();
            pluginCollectionGeneratorsDictionary = fakerLoader.getPluginCollectionGenerators();
        }

        // загрузка настраиваемых генераторов
        public Faker(IFakerConfig fakerConfig) : this()
        {
            Console.WriteLine("Настраиваемые генераторы");

            // IGenerator
            configGeneratorsDictionary = fakerConfig.GetConfigGenerators();

            // IСollectionGenerator
            configCollectionGeneratorsDictionary = fakerConfig.GetConfigCollectionGenerators();
        }

        // создание объекта по его типу
        public type Create<type>()
        {
            return (type)Create(typeof(type));
        }

        // создание объекта по его типу
        public object Create(Type objectType)
        {
            return Create(objectType, null, null);
        }

        // создание объекта; 
        // параметры: 
        // Type objectType - тип создаваемого объекта;
        // string name - название свойства/поля (если объект является свойством или полем; в противном случае null);
        // Type classType - тип класса, в который входит свойство/поли (если объект является свойством или полем; в противном случае null).
        private object Create(Type objectType, string name, Type classType)
        {
            object createdObject;

            // настраиваемые генераторы
            if (IsConfigGenerator(classType, name, objectType))
                createdObject = GenerateConfigGenerator(classType, name, objectType);
            else if (IsConfigCollectionGenerator(classType, name, objectType))
                createdObject = GenerateConfigCollectionGenerator(classType, name, objectType);
            // стандартные генераторы
            else if (IsStandardGenerator(objectType))
                createdObject = GenerateStandard(objectType);
            else if (IsStandardCollectionGenerator(objectType))
                createdObject = GenerateCollectionStandard(objectType);
            // генераторы, загружаемые из плагинов
            else if (IsPluginGenerator(objectType))
                createdObject = GeneratePlugin(objectType);
            else if (IsPluginCollectionGenerator(objectType))
                createdObject = GenerateCollectionPlugin(objectType);
            // рекурсивная генерация объектов DTO
            else if (IsDTO(objectType))
                createdObject = GenerateDTO(objectType);
            // возвращение значения по умолчанию
            else
                createdObject = GenerateDefaultValue(objectType);

            return createdObject;
        }

        // существует ли настраеваемый генератор IGenerator
        private bool IsConfigGenerator(Type classType, string name, Type returnType)
        {
            return classType != null && name != null &&
                configGeneratorsDictionary.ContainsKey(classType) && configGeneratorsDictionary[classType].ContainsKey((name, returnType));
        }

        // создание объекта с помощью настраеваемого генератора IGenerator
        private object GenerateConfigGenerator(Type classType, string name, Type returnType)
        {
            return configGeneratorsDictionary[classType][(name, returnType)].GenerateObject();
        }

        // существует ли настраеваемый генератор ICollectionGenerator
        private bool IsConfigCollectionGenerator(Type classType, string name, Type returnType)
        {
            if (classType != null && name != null)
            {
                return configCollectionGeneratorsDictionary.ContainsKey(classType) && configCollectionGeneratorsDictionary[classType].ContainsKey((name, returnType));
            }
            return false;
        }

        // создание объекта с помощью настраеваемого генератора ICollectionGenerator
        private object GenerateConfigCollectionGenerator(Type classType, string name, Type returnType)
        {
            if (returnType.GetInterface(typeof(IList<>).Name) != null)
            {
                Type collectionType = returnType.GetGenericTypeDefinition();
                Type[] collectionArgs = returnType.GetGenericArguments();
                Type fullCollectionType = collectionType.MakeGenericType(collectionArgs);
                return configCollectionGeneratorsDictionary[classType][(name, fullCollectionType)].GenerateCollection(null, this);
            }
            return false;
        }

        // существует ли стандартный генератор IGenerator
        private bool IsStandardGenerator(Type objectType)
        {
            return standardGeneratorsDictionary.ContainsKey(objectType);
        }

        // создание объекта с помощью стандартного генератора IGenerator
        private object GenerateStandard(Type objectType)
        {
            return standardGeneratorsDictionary[objectType].GenerateObject();
        }

        // существует ли стандартный генератор ICollectionGenerator
        private bool IsStandardCollectionGenerator(Type objectType)
        {
            if (objectType.GetInterface(typeof(IList<>).Name) != null)
            {
                Type collectionType = objectType.GetGenericTypeDefinition();
                return standardCollectionGeneratorsDictionary.ContainsKey(collectionType);
            }
            return false;
        }

        // создание объекта с помощью стандартного генератора ICollectionGenerator
        private object GenerateCollectionStandard(Type objectType)
        {
            Type collectionType = objectType.GetGenericTypeDefinition();
            Type elementType = objectType.GetGenericArguments()[0];
            return standardCollectionGeneratorsDictionary[collectionType].GenerateCollection(elementType, this);
        }

        // существует ли загружаемый из плагина генератор IGenerator
        private bool IsPluginGenerator(Type objectType)
        {
            return pluginGeneratorsDictionary.ContainsKey(objectType);
        }

        // создание объекта с помощью загружаемого из плагина генератора IGenerator
        private object GeneratePlugin(Type objectType)
        {
            return pluginGeneratorsDictionary[objectType].GenerateObject();
        }

        // существует ли загружаемый из плагина генератор ICollectionGenerator
        private bool IsPluginCollectionGenerator(Type objectType)
        {
            if (objectType.GetInterface(typeof(IList<>).Name) != null)
            {
                Type collectionType = objectType.GetGenericTypeDefinition();
                return pluginCollectionGeneratorsDictionary.ContainsKey(collectionType);
            }
            return false;
        }

        // создание объекта с помощью загружаемого из плагина генератора ICollectionGenerator
        private object GenerateCollectionPlugin(Type objectType)
        {
            Type collectionType = objectType.GetGenericTypeDefinition();
            Type elementType = objectType.GetGenericArguments()[0];
            return pluginCollectionGeneratorsDictionary[collectionType].GenerateCollection(elementType, this);
        }

        // является ли объект DTO
        private bool IsDTO(Type objectType)
        {
            return objectType.GetCustomAttribute(typeof(DTOAttribute)) != null;
        }

        // рекурсивная генерация DTO
        private object GenerateDTO(Type objectType)
        {
            // возвращение значения по умолчанию в случае если данный тип обрабатывается
            if (processingDTOObjects.Contains(objectType))
            {
                return GenerateDefaultValue(objectType);
            }

            // добавление типа в очередь обрабатываемых типов
            processingDTOObjects.Enqueue(objectType);

            // создание DTO
            object DTOObject = CreateObject(objectType);

            // заполнение свойств DTO
            PropertyInfo[] properties = objectType.GetProperties().Where(property => property.GetSetMethod() != null).ToArray();
            SetOjectPoperties(DTOObject, properties);

            // заполнение полей DTO
            FieldInfo[] fields = objectType.GetFields();
            SetObjectFields(DTOObject, fields);

            // извлечение типа из очереди обрабатываемых типов
            processingDTOObjects.Dequeue();

            return DTOObject;
        }

        // создание DTO
        private object CreateObject(Type objectType)
        {
            ConstructorInfo[] constructorInfos = GetObjectConstructors(objectType);
            ConstructorInfo bestConstructor = ChooseBestConstructor(constructorInfos);
            return InvokeConstructor(bestConstructor);
        }

        // получение конструкторов объекта
        private ConstructorInfo[] GetObjectConstructors(Type objectType)
        {
            ConstructorInfo[] constructors = objectType.GetConstructors();
            if (constructors.Length == 0)
            {
                constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            }
            return constructors;
        }

        // выбор конструктора с наибольшим числом параметров
        private ConstructorInfo ChooseBestConstructor(ConstructorInfo[] constructorInfos)
        {
            ConstructorInfo[] suitableConstructors = constructorInfos;
            return suitableConstructors.Aggregate((firstCI, secondCI) =>
                firstCI.GetParameters().Length > secondCI.GetParameters().Length ? firstCI : secondCI);
        }

        // работа конструктора
        private object InvokeConstructor(ConstructorInfo constructor)
        {
            List<object> parameters = new List<object>();
            foreach (ParameterInfo parameterInfo in constructor.GetParameters())
            {
                parameters.Add(Create(parameterInfo.ParameterType, parameterInfo.Name, constructor.DeclaringType));
            }
            return constructor.Invoke(parameters.ToArray());
        }

        // заполнение свойств DTO
        private void SetOjectPoperties(object obj, PropertyInfo[] properties)
        {
            foreach (PropertyInfo property in properties)
                property.SetValue(obj, Create(property.PropertyType, property.Name, obj.GetType()));
        }

        // заполнение полей DTO
        private void SetObjectFields(object obj, FieldInfo[] fields)
        {
            foreach (FieldInfo field in fields)
                field.SetValue(obj, Create(field.FieldType, field.Name, obj.GetType()));
        }

        // генерация значения по умолчанию
        // применяется для объектов, не являющихся DTO,
        // а также объектов DTO, которые уже генерируются
        // (разрешение циклических зависимостей)
        private object GenerateDefaultValue(Type objectType)
        {
            return default;
        }
    }
}

using System;
using FakerInterfaceLibrary;
using FakerLibrary;
using DTOObjectsLibrary;
using ConfigGeneratorsLibrary;
using System.Collections.Generic;
using WritingLibrary;
using SerializationLibrary;
using PathNavigatorLibrary;
using System.Reflection;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        // список созданных объектов
        private static List<object> objectList = new List<object>();

        // FakerConfig
        private static IFakerConfig fakerConfig;

        // Faker
        private static IFaker faker;

        // Настройка генерируемых случайных значений для конкретного поля 
        // путем передачи собственного генератора для конкретного поля/свойства
        private static void ConfigFaker()
        {
            fakerConfig.AddGenerator<ClassA, byte>(a => a.Number, new NumberGenerator());
            fakerConfig.AddGenerator<ClassA, DateTime>(a => a.DateTime2020, new DateTime2020Generator());
            fakerConfig.AddCollectionGenerator<ClassA, List<string>>(a => a.catStringList, new CatStringListGenerator());
            fakerConfig.AddCollectionGenerator<ClassA, List<string>>(a => a.DogStringList, new DogStringListGenerator());
            fakerConfig.AddGenerator<ClassD, string>(d => d.russianString, new RussianStringGenerator());
            fakerConfig.AddGenerator<ClassD, string>(d => d.numberString, new NumberStringGenerator());
        }

        // создание объектов
        private static void CreateObjects(IFaker faker)
        {
            ClassA a = faker.Create<ClassA>();
            objectList.Add(a);
            ClassB b = faker.Create<ClassB>();
            objectList.Add(b);
            ClassC c = faker.Create<ClassC>();
            objectList.Add(c);
            ClassD d = faker.Create<ClassD>();
            objectList.Add(d);
            ClassE e = faker.Create<ClassE>();
            objectList.Add(e);
            ClassF f = faker.Create<ClassF>();
            objectList.Add(f);
            ClassG g = faker.Create<ClassG>();
            objectList.Add(g);
            ClassH h = faker.Create<ClassH>();
            objectList.Add(h);
            ClassI i = faker.Create<ClassI>();
            objectList.Add(i);
        }

        private static void OutputObjects()
        {
            // создание писателей
            IWriter consoleWriter = new ConsoleWriter();
            IWriter fileWriter = new FileWriter();

            // создание сериализатора
            ISerializer jsonSerializer = new JsonSerializer();
            // получение имени сериализатора
            string strNameJson = jsonSerializer.GetName();

            foreach (object obj in objectList)
            {
                if (obj != null)
                {
                    // получение сериализованных объектов
                    string strJson = jsonSerializer.Serialize(obj);

                    // вывод в файлы
                    fileWriter.Write(strJson, obj.GetType().Name + "." + strNameJson);

                    // вывод в консоль
                    Console.WriteLine();
                    consoleWriter.Write(strJson, obj.GetType().Name + "." + strNameJson);
                }
            }
        }

        public static void Main(string[] args)
        {
            // FakerConfig
            fakerConfig = new FakerConfig();

            // Настройка генерируемых случайных значений для конкретного поля 
            // путем передачи собственного генератора для конкретного поля/свойства
            ConfigFaker();

            // Faker
            faker = new Faker(fakerConfig);

            // создание объектов
            CreateObjects(faker);

            // вывод объектов
            OutputObjects();

            // ожидание ввода
            Console.ReadLine();
        }
    }
}

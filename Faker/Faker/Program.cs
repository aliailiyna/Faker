using System;
using FakerInterfaceLibrary;
using FakerLibrary;
using DTOObjectsLibrary;
using ConfigGeneratorsLibrary;
using System.Collections.Generic;
using WritingLibrary;
using SerializationLibrary;
using PathNavigatorLibrary;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IFakerConfig fakerConfig = new FakerConfig();
            //fakerConfig.AddGenerator<ClassA, byte>(a => a.ConfigNumber, new NumberGenerator());
            fakerConfig.AddCollectionGenerator<ClassA, List<string>>(a => a.configList, new StringListGenerator());
            IFaker faker = new Faker(fakerConfig);
            //IFaker faker = new Faker();
            ClassA a = faker.Create<ClassA>();

            IWriter consoleWriter = new ConsoleWriter();
            IWriter fileWriter = new FileWriter();

            ISerializer jsonSerializer = new JsonSerializer();


            string strJson = jsonSerializer.Serialize(a);
            string strNameJson = jsonSerializer.GetName();

            fileWriter.Write(strJson, a.GetType().Name + "." + strNameJson);
            consoleWriter.Write(strJson, a.GetType().Name + "." + strNameJson);

            Console.WriteLine();
            foreach (var el in a.configList)
            Console.WriteLine(el.ToString());
            Console.WriteLine("Hello World!");
            Console.WriteLine((new StringListGenerator()).GetCollectionType().FullName);

            PathNavigator pathNavigator = new PathNavigator();
            string path = pathNavigator.GetWriteResultDirectory();
            Console.WriteLine(path + @"\" + a.GetType().Name + "." + strNameJson);
        }
    }
}

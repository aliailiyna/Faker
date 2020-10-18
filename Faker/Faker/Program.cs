using System;
using FakerInterfaceLibrary;
using FakerLibrary;
using DTOObjectsLibrary;
using ConfigGeneratorsLibrary;
using System.Collections.Generic;

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
            if (a != null && a.configList != null)
            foreach (var el in a.configList)
            Console.WriteLine(el.ToString());
            Console.WriteLine("Hello World!");
            Console.WriteLine((new StringListGenerator()).GetCollectionType().FullName);
        }
    }
}

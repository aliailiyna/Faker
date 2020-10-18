using System;
using FakerInterfaceLibrary;
using FakerLibrary;
using DTOObjectsLibrary;
using ConfigGeneratorsLibrary;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IFakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<ClassA, byte>(a => a.ConfigNumber, new NumberGenerator());
            IFaker faker = new Faker(fakerConfig);
            ClassA a = faker.Create<ClassA>();
            Console.WriteLine(a.ConfigNumber.ToString());
            Console.WriteLine("Hello World!");
        }
    }
}

using System;
using FakerInterfaceLibrary;
using FakerLibrary;
using DTOObjectsLibrary;
using DTOAttributeLibrary;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IFaker faker = new Faker();
            ClassA a = faker.Create<ClassA>();
            if (a == null)
            {
                Console.WriteLine("null");
            }
            else
            {
                Console.WriteLine(a.number.ToString());
            }
            Console.WriteLine("Hello World!");
        }
    }
}

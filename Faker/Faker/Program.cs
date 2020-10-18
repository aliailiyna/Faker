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
            //ClassA a = faker.Create<ClassA>();
            //Console.WriteLine(a.number.ToString());
            ClassF f = faker.Create<ClassF>();
            if (f == null)
            {
                Console.WriteLine("error");
            }
            else
            {
                Console.WriteLine(f.classA.number.ToString());
            }
            Console.WriteLine("Hello World!");
        }
    }
}

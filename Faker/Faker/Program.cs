using System;
using FakerInterfaceLibrary;
using FakerLibrary;
using DTOObjectsLibrary;
using DTOAttributeLibrary;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IFaker faker = new Faker();
            //ClassA a = faker.Create<ClassA>();
            ClassC c = faker.Create<ClassC>();
            Console.WriteLine(c.list.GetType().ToString());
            Console.WriteLine(c.list.Count.ToString());
            foreach (var el in c.list)
            {
                Console.WriteLine(el.ToString());
            }
            Console.WriteLine("Hello World!");
        }
    }
}

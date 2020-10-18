using System;
using FakerLibrary;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            Expression<Func<int, bool>> lambda = num => num < 5;
            Console.WriteLine("Hello World!");
        }
    }
}

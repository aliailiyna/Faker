using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PathNavigatorLibrary;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public class Faker : IFaker
    {
        private IPathNavigator pathNavigator;
        private Assembly standardGeneratorsAssembly;
        private List<Type> standardGeneratorsTypesList = new List<Type>();
        public Faker()
        {
            pathNavigator = new PathNavigator();
            Console.WriteLine(pathNavigator.GetRootDirectory());
            Console.WriteLine(pathNavigator.GetStandardDirectory());
            Console.WriteLine(pathNavigator.GetWriteResultDirectory());
            Console.WriteLine(pathNavigator.GetPluginsDirectory());
            this.LoadStandardGenerators();
        }

        private void LoadStandardGenerators()
        {
            standardGeneratorsAssembly = Assembly.LoadFile(pathNavigator.GetStandardDirectory());
            standardGeneratorsTypesList = standardGeneratorsAssembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IGenerator)) && type.IsClass && !type.IsAbstract).ToList();
            foreach (Type currentType in standardGeneratorsTypesList)
            {
                if (!currentType.IsAbstract)
                {
                    Console.WriteLine(currentType.Name);
                }
            }
        }
    }
}

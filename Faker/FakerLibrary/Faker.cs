using System;
using System.Collections.Generic;
using System.Text;
using FakerInterfaceLibrary;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    public class Faker : IFaker
    {
        private Dictionary<Type, IGenerator> standardGeneratorsDictionary;
        private Dictionary<Type, ICollectionGenerator> standardCollectionGeneratorsDictionary;
        private Dictionary<Type, IGenerator> pluginGeneratorsDictionary;
        private Dictionary<Type, ICollectionGenerator> pluginCollectionGeneratorsDictionary;
        private Dictionary<ConfigKey, IGenerator> configGeneratorsDictionary;
        private Dictionary<ConfigKey, ICollectionGenerator> configCollectionGeneratorsDictionary;

        private IFakerLoader fakerLoader = new FakerLoader();
        public Faker()
        {
        }

        //public Faker(FakerConfig fakerConfig)
        //{
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace FakerLibrary
{
    internal interface IFakerLoader
    {
        public Dictionary<Type, IGenerator> getStandardGenerators();

        public Dictionary<Type, ICollectionGenerator> getStandardCollectionGenerators();

        public Dictionary<Type, IGenerator> getPluginGenerators();

        public Dictionary<Type, ICollectionGenerator> getPluginCollectionGenerators();

    }
}

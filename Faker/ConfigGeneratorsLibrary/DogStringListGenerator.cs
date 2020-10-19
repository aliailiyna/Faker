using System;
using System.Collections.Generic;
using GeneratorsInterfacesLibrary;
using FakerInterfaceLibrary;

namespace ConfigGeneratorsLibrary
{
    public class DogStringListGenerator : RandomGenerator, ICollectionGenerator
    {
        private const int MIN_LENGTH = 0;
        private const int MAX_LENGTH = 10;
        public object GenerateCollection(Type elementType, IFaker faker)
        {
            List<string> list = new List<string>();

            int elementCount = random.Next(MIN_LENGTH, MAX_LENGTH + 1);
            for (int i = 0; i < elementCount; i++)
            {
                list.Add("dog");
            }
            return list;
        }

        public Type GetCollectionType()
        {
            return typeof(List<string>);
        }
    }
}

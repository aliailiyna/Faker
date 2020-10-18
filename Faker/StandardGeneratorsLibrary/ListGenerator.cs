using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;
using FakerInterfaceLibrary;
using System.Collections;

namespace StandardGeneratorsLibrary
{
    public class ListGenerator : RandomGenerator, ICollectionGenerator
    {
        private const int MIN_LENGTH = 0;
        private const int MAX_LENGTH = 10;
        public object GenerateCollection(Type elementType, IFaker faker)
        {
            Type genericListType = typeof(List<>).MakeGenericType(elementType);
            IList list = (IList)Activator.CreateInstance(genericListType);

            int elementCount = random.Next(MIN_LENGTH, MAX_LENGTH + 1);
            for (int i = 0; i < elementCount; i++)
                list.Add(faker.Create(elementType));
            return list;
        }

        public Type GetCollectionType()
        {
            return typeof(List<>);
        }
    }
}

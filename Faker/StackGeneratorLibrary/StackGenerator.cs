using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;
using FakerInterfaceLibrary;

namespace StackGeneratorLibrary
{
    public class StackGenerator : ICollectionGenerator
    {
        private static Random random = new Random();
        public object GenerateCollection(Type elementType, IFaker faker)
        {
            return Convert.ToByte(random.Next(Byte.MinValue, Byte.MaxValue + 1));
        }

        public Type GetCollectionType()
        {
            return typeof(List<>);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;
using RandomGeneratorLibrary;
using FakerLibrary;

namespace StandardGeneratorsLibrary
{
    public class ListGenerator : AbstractCollectionGenerator, ICollectionGenerator
    {
        ListGenerator(IFaker faker) : base(faker)
        {

        }
        public object GenetateCollection(Type elementType)
        {
            return Convert.ToByte(RandomGenerator.random.Next(Byte.MinValue, Byte.MaxValue + 1));
        }

        public Type GetCollectionType()
        {
            return typeof(List<>);
        }
    }
}

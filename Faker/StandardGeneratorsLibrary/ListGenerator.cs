﻿using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;
using RandomGeneratorLibrary;
using FakerInterfaceLibrary;

namespace StandardGeneratorsLibrary
{
    public class ListGenerator : ICollectionGenerator
    {
        public object GenetateCollection(Type elementType, IFaker faker)
        {
            return Convert.ToByte(RandomGenerator.random.Next(Byte.MinValue, Byte.MaxValue + 1));
        }

        public Type GetCollectionType()
        {
            return typeof(List<>);
        }
    }
}

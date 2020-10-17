using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;
using RandomGeneratorLibrary;

namespace StandardGeneratorsLibrary
{
    class ByteGenerator : IGenerator
    {
        public object GenetateObject()
        {
            // генерация случайного числа от Byte.MinValue до Byte.MaxValue
            return Convert.ToByte(RandomGenerator.random.Next(Byte.MinValue, Byte.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(byte);
        }
    }
}

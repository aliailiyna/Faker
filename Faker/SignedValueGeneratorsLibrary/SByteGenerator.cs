using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;
using RandomGeneratorLibrary;

namespace SignedValueGeneratorsLibrary
{
    public class SByteGenerator : IGenerator
    {
        public object GenetateObject()
        {
            // генерация случайного числа от SByte.MinValue до SByte.MaxValue
            return Convert.ToByte(RandomGenerator.random.Next(SByte.MinValue, SByte.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(sbyte);
        }
    }
}

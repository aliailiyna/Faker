using System;
using System.Collections.Generic;
using System.Text;
using GeneratorsInterfacesLibrary;

namespace SignedValueGeneratorsLibrary
{
    public class SByteGenerator : IGenerator
    {
        private static Random random = new Random();
        public object GenerateObject()
        {
            // генерация случайного числа от SByte.MinValue до SByte.MaxValue
            return Convert.ToByte(random.Next(SByte.MinValue, SByte.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(sbyte);
        }
    }
}

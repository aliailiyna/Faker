using System;
using GeneratorsInterfacesLibrary;

namespace SignedValueGeneratorsLibrary
{
    public class SByteGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от SByte.MinValue до SByte.MaxValue
            return Convert.ToSByte(random.Next(SByte.MinValue, SByte.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(sbyte);
        }
    }
}

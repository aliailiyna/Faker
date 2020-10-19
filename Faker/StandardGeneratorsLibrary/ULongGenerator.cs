using System;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class ULongGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от UInt64.MinValue до UInt64.MaxValue
            return (ulong)(((random.Next(UInt16.MinValue, UInt16.MaxValue + 1)) << 48) | 
                ((random.Next(UInt16.MinValue, UInt16.MaxValue + 1)) << 32) | 
                ((random.Next(UInt16.MinValue, UInt16.MaxValue + 1)) << 16) |
                (random.Next(UInt16.MinValue, UInt16.MaxValue + 1)));
        }

        public Type GetObjectType()
        {
            return typeof(ulong);
        }
    }
}

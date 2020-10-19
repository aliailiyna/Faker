using System;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class UIntGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от UInt32.MinValue до UInt32.MaxValue
            return (((uint)(random.Next(UInt16.MinValue, UInt16.MaxValue + 1)) << 16) | 
                (uint)(random.Next(UInt16.MinValue, UInt16.MaxValue + 1)));
        }

        public Type GetObjectType()
        {
            return typeof(uint);
        }
    }
}

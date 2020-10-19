using System;
using GeneratorsInterfacesLibrary;

namespace StandardGeneratorsLibrary
{
    public class UShortGenerator : RandomGenerator, IGenerator
    {
        public object GenerateObject()
        {
            // генерация случайного числа от UInt16.MinValue до UInt16.MaxValue
            return Convert.ToUInt16(random.Next(UInt16.MinValue, UInt16.MaxValue + 1));
        }

        public Type GetObjectType()
        {
            return typeof(ushort);
        }
    }
}
